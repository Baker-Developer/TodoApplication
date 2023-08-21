using TodoApplication.Data;
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
    }
}
