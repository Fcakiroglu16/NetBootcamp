using Microsoft.EntityFrameworkCore;

namespace LoggingAndMiddleware.API.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Error> Errors { get; set; }
    }
}