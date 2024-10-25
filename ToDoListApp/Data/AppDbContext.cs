using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source = . ; Initial Catalog =  WNZDotNet ; User ID = sa ; password = waunnazaw ; TrustServerCetificate = true";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

    }
}
