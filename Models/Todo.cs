using System.ComponentModel.DataAnnotations;

namespace ToDoApplicationMVC.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        public string Task { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
