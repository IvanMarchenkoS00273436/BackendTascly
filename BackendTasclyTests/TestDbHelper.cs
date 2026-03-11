using BackendTascly.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendTasclyTests
{
    internal static class TestDbHelper
    {
        public static async Task<TasclyDbContext> CreateSeededContextAsync(string databaseName)
        {
            var options = new DbContextOptionsBuilder<TasclyDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            var context = new TasclyDbContext(options);
            await context.SeedDataAsync();
            return context;
        }
    }
}
