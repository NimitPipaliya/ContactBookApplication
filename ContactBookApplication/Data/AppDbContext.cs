using ContactBookApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        public DbSet<ContactBook> ContactBook { get; set; }
        public DbSet<User> User { get; set; }
    }
}
