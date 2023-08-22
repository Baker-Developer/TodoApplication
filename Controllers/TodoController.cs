using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Data;
using TodoApplication.Models;
using TodoApplication.Services;
using TodoApplication.Services.Interfaces;

namespace TodoApplication.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITodoService _todoService;

        public TodoController(ApplicationDbContext context, ITodoService todoService)
        {
            _context = context;
            _todoService = todoService;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            return _context.Todo != null ?
                        View(await _context.Todo.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Todo'  is null.");
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Todo == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Note,DeadLineDate,IsComplete")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddNewTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Todo == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Note,DeadLineDate,IsComplete")] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }


        // GET: Todo/Archive/5
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null || _context.Todo == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }



        // POST: Todo/Delete/5
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(int id)
        {
            if (_context.Todo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Todo'  is null.");
            }
            var todo = await _context.Todo.FindAsync(id);
            await _todoService.ArchiveTodoAsync(todo);

            return RedirectToAction(nameof(Index));
        }



        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Todo == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Todo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Todo'  is null.");
            }
            var todo = await _context.Todo.FindAsync(id);
            if (todo != null)
            {
                await _todoService.DeleteNewTodoAsync(todo);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return (_context.Todo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
