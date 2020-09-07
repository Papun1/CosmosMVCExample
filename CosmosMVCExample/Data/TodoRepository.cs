using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CosmosMVCExample.Models;
using Microsoft.Azure.Cosmos.Table;

namespace CosmosMVCExample.Data
{
    public class TodoRepository
    {
        private CloudTable todoTable = null;
        public TodoRepository()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=cstb108;AccountKey=tjlxzUgzAadOlWiSyu8zAe98m30tPBrdlRl13kgUruytVU979ZYHZZ40mgEvigK4AsousOL1l7PEBTkAWgGTjw==;TableEndpoint=https://cstb108.table.cosmos.azure.com:443/;";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            todoTable = tableClient.GetTableReference("Todo");
        }
        public IEnumerable<TodoEntity> All()
        {
           // var query = new TableQuery<TodoEntity>();
            var query = new TableQuery<TodoEntity>().
                Where(TableQuery.GenerateFilterConditionForBool(nameof(TodoEntity.Completed),
                QueryComparisons.Equal,false));
            var entities = todoTable.ExecuteQuery(query);  
            return entities;
        }
        public void CreateorUpdate(TodoEntity todoEntity)
        {
            var operation = TableOperation.InsertOrReplace(todoEntity);
            todoTable.Execute(operation);

        }
        public void Delete(TodoEntity todoEntity)
        {
            var operation = TableOperation.Delete(todoEntity);
            todoTable.Execute(operation);
        }
        public TodoEntity Get(string partitionKey,string rowKey)
        {
            var operation = TableOperation.Retrieve<TodoEntity>(partitionKey, rowKey);
            var result= todoTable.Execute(operation);
            return result.Result as TodoEntity;
        }
    }
    public class TodoEntity:TableEntity
    {
        public string Content { get; set; }
        public bool Completed { get; set; }
        public string Place { get; set; }
    }
}