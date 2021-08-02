using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync(string id);
        Task<bool> AddItemAsync(NewToDoItem newItem, string userId);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
