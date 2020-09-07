using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmosMVCExample.Models
{
    public class TodoModel
    {
        public string Id { get; set; }
        public string Group { get; set; }
        public string Content { get; set; }
        public bool Completed { get; set; }
        public string Place { get; set; }
        public DateTimeOffset Timestamp { get; set; }

    }
}