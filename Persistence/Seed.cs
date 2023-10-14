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

            var booksData = await File.ReadAllTextAsync("../Persistence/todos.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var books = JsonSerializer.Deserialize<List<Todo>>(booksData, options);

            foreach (var book in books)
            {
                context.Add(book);
            }

            await context.SaveChangesAsync();
        }
    }
}