using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TodoApplication.Models;

namespace TodoApplication.Services.Interfaces
{
    public interface ITodoService
    {
        public Task AddNewTodoAsync(Todo todo);
        public Task ArchiveTodoAsync(Todo todo);
        public Task DeleteNewTodoAsync(Todo todo);
    }
}
