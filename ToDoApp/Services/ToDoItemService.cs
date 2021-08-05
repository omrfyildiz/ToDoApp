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

        public async Task<bool> AddItemAsync(NewToDoItem newItem, string userId)
        {
            var entity = new ToDoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                StartedAt = DateTimeOffset.Now,
                OwnerId = userId

            };

            _context.ToDoItems.Add(entity);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync(string id)
        {
            var items = await _context.ToDoItems
                .Where(x => x.IsDone == false && x.OwnerId == id)
                .ToArrayAsync();

            return items;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await _context.ToDoItems
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }

}
