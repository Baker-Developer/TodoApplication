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
            try
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
  
        }

        public async Task ArchiveTodoAsync(Todo todo)
        {
            try
            {
                todo.IsComplete = true;
                _context.Update(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteNewTodoAsync(Todo todo)
        {
            try
            {
                _context.Todo.Remove(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
