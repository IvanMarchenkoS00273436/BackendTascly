using BackendTascly.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Data
{
    public class TasclyDbContext(DbContextOptions<TasclyDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
