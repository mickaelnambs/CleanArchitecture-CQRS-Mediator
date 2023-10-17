using System.Text.Json;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (await context.Todos.AnyAsync()) return;

            var todosData = await File.ReadAllTextAsync("../Persistence/todos.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var todos = JsonSerializer.Deserialize<List<Todo>>(todosData, options);

            foreach (var todo in todos)
            {
                context.Add(todo);
            }

            await context.SaveChangesAsync();
        }
    }
}