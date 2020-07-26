using System;
using System.Collections.Generic;
using System.Text;

namespace MetalBotDAL.Models
{
    public class TodoItem:Entity
    {
        public string TaskTodo { get; set; }
        public string UserId { get; set; }

    }
}
