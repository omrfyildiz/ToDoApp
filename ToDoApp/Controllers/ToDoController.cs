using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Services;


namespace ToDoApp.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly IToDoItemService _toDoItemService;
        private readonly UserManager<IdentityUser> _userManager;

        public ToDoController(IToDoItemService toDoItemService, UserManager<IdentityUser> userManager)
        {
            _toDoItemService = toDoItemService;
            _userManager = userManager;
        }

        public IdentityUser GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User;
            var user = _userManager.GetUserAsync(User).Result;
            return user;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var toDoItems = await _toDoItemService.GetIncompleteItemsAsync(GetCurrentUser().Id);

            var model = new ToDoViewModel()
            {
                ToDoItems = toDoItems
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> AddItem(NewToDoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var successful = await _toDoItemService.AddItemAsync(newItem, GetCurrentUser().Id);
            if (!successful)
            {
                return BadRequest(new { error = "Could not add item" });
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> MarkDoneAsync(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var successful = await _toDoItemService.MarkDoneAsync(id);

            if (!successful) return BadRequest();

            return Ok();
        }



    }
}
