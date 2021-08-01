using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> Index()
        {
            var toDoItems = await _toDoItemService.GetIncompleteItemsAsync();

            var model = new ToDoViewModel()
            {
                ToDoItems = toDoItems
            };

            return View(model);

        }

        public async Task<IActionResult> AddItem(NewToDoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var successful = await _toDoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest(new { error = "Could not be added item" });
            }

            return Ok();
        }



    }
}
