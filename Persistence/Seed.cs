using System.Text.Json;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            await SeedTodos(context);
            await SeedUsers(context, userManager);
        }

        private static async Task SeedTodos(DataContext context)
        {
            if (await context.Todos.AnyAsync()) return;

            var todos = await DeserializeJsonFile<List<Todo>>("../Persistence/todos.json");

            context.Todos.AddRange(todos);

            await context.SaveChangesAsync();
        }

        private static async Task SeedUsers(DataContext context, UserManager<AppUser> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var users = await DeserializeJsonFile<List<AppUser>>("../Persistence/users.json");

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }

            await context.SaveChangesAsync();
        }

        private static async Task<T> DeserializeJsonFile<T>(string filePath)
        {
            var jsonData = await File.ReadAllTextAsync(filePath);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<T>(jsonData, options);
        }
    }
}