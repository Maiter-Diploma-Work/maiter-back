using MaterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MaterApp
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Interest> Interests { get; set; }  

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, Username = "Tom", Email = "tom@example.com", Password = "password1", FirstName = "Tom", LastName = "Smith", DateOfBirth = new DateTime(1990, 1, 1), Gender = Gender.Male },
            //    new User { Id = 2, Username = "Bob", Email = "bob@example.com", Password = "password2", FirstName = "Bob", LastName = "Johnson", DateOfBirth = new DateTime(1985, 5, 10), Gender = Gender.Male },
            //    new User { Id = 3, Username = "Sam", Email = "sam@example.com", Password = "password3", FirstName = "Sam", LastName = "Brown", DateOfBirth = new DateTime(1995, 9, 15), Gender = Gender.Female }
            //);

        }
    }
}
