using CosmosMVCExample.Data;
using CosmosMVCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmosMVCExample.Controllers
{
    public class TodosController : Controller
    {
        // GET: Todos
        public ActionResult Index()
        {
            var repository = new TodoRepository();
            var entity = repository.All();
            var model = entity.Select(x => new TodoModel
            {
                Id = x.RowKey,
                Group = x.PartitionKey,
                Content = x.Content,
                Place = x.Place,
                Completed = x.Completed,
                Timestamp = x.Timestamp
            });
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TodoModel todomodel)
        {
            var todoRepository = new TodoRepository();
            todoRepository.CreateorUpdate(new TodoEntity
            {
                PartitionKey = todomodel.Group,
                RowKey = Guid.NewGuid().ToString(),
                Content = todomodel.Content,
                Place = todomodel.Place
            });
            return RedirectToAction("Index");
        }
        public ActionResult ConfirmDelete(string id, string group)
        {
            var repository = new TodoRepository();
            var item = repository.Get(group,id);
            return View("Delete",new TodoModel
            {
                Id = item.RowKey,
                Group = item.PartitionKey,
                Content = item.Content,
                Place = item.Place,
                Completed = item.Completed,
                Timestamp = item.Timestamp
            });
        }
        [HttpPost]
        public ActionResult Delete(string id, string group)
        {
            var todoRepository = new TodoRepository();
            var item = todoRepository.Get(group,id);
            todoRepository.Delete(item);
            return RedirectToAction("Index");
        }
       
        public ActionResult Edit(string id, string group)
        {
            var repository = new TodoRepository();
            var item = repository.Get(group, id);
            return View( new TodoModel
            {
                Id = item.RowKey,
                Group = item.PartitionKey,
                Content = item.Content,
                Place = item.Place,
                Completed = item.Completed,
                Timestamp = item.Timestamp
            });
        }
        [HttpPost]
        public ActionResult Edit(TodoModel model)
        {
            var todoRepository = new TodoRepository();
            var item = todoRepository.Get(model.Group,model.Id);
            item.Completed = model.Completed;
            item.Content = model.Content;
            item.Place = model.Place;
            todoRepository.CreateorUpdate(item);
            return RedirectToAction("Index");
        }

    }
}