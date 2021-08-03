using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class NewToDoItem
    {
        [Required]
        public string Title { get; set; }
    }
}
