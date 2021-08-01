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


        public async Task<bool> AddItemAsync(NewToDoItem newItem)
        {
            var entity = new ToDoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                StartedAt = DateTimeOffset.Now
            };

            _context.ToDoItems.Add(entity);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
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
