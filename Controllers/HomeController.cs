using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApplicationMVC.Data;
using ToDoApplicationMVC.Models;


namespace ToDoApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskContext db;

        public HomeController(TaskContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Todos.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Add(todo);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var todo = await db.Todos.FindAsync(id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Todo todo)
        {
            if (id != todo.Id) return NotFound();
            if (ModelState.IsValid)
            {
                db.Update(todo);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var todo = await db.Todos.FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await db.Todos.FindAsync(id);
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MarkComplete(int id)
        {
            var todo = await db.Todos.FindAsync(id);
            if (todo == null) return NotFound();

            todo.IsCompleted = true;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
