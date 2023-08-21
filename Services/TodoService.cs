using Microsoft.AspNetCore.Mvc;
using TodoApplication.Data;
using TodoApplication.Models;
using TodoApplication.Services.Interfaces;

namespace TodoApplication.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _context;

        public TodoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewTodoAsync(Todo todo)
        {
            _context.Add(todo);
            await _context.SaveChangesAsync();
        }

        public Task ArchiveNewTodoAsync(Todo todo)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteNewTodoAsync(Todo todo)
        {
            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
