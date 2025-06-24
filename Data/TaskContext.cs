using Microsoft.EntityFrameworkCore;
using ToDoApplicationMVC.Models;

namespace ToDoApplicationMVC.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
    }
}
