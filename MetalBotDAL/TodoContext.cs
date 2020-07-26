using MetalBotDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetalBotDAL
{
    class TodoContext: DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
