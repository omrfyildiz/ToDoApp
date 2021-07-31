using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class ToDoItemService : IToDoItemService
    {
         private readonly ToDoDbContext _context;

        public ToDoItemService(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync()
        {
            var items = await _context.ToDoItems
                .Where(x => x.IsDone == false)
                .ToArrayAsync();

            return items;
        }

    }
    
}
