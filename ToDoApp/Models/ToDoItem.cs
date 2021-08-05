using System;

namespace ToDoApp.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public bool IsDone { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public string OwnerId { get; set; }
    }
}
