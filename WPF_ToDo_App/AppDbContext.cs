using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WPF_ToDo_App
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> UserDetails { get; set; }
        public DbSet<Task> TaskDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-SSE1R9SI\\PALITSQLSERVER;Database=PalitDB;User Id=sa;Password=Palit@2505;TrustServerCertificate=True;");
            }
            catch (Exception ex) 
            {
               Console.WriteLine($"Error configuring database: {ex.Message}");
            }
        }
    }
}
