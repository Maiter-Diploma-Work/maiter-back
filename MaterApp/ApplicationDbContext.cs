using MaterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MaterApp
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<BlockedUsers> BlockedUsers { get; set; } // Добавьте DbSet для BlockedUsers

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserInterest>()
                .HasKey(ui => new { ui.UserId, ui.InterestId });

            modelBuilder.Entity<UserInterest>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInterests)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<UserInterest>()
                .HasOne(ui => ui.Interest)
                .WithMany(i => i.UserInterests)
                .HasForeignKey(ui => ui.InterestId);

            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.LikerId, l.LikeeId });

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Liker)
                .WithMany()
                .HasForeignKey(l => l.LikerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Likee)
                .WithMany()
                .HasForeignKey(l => l.LikeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BlockedUsers>()
                .HasKey(bu => new { bu.UserId, bu.BlockedUserId });

            modelBuilder.Entity<BlockedUsers>()
                .HasOne(bu => bu.User)
                .WithMany(u => u.BlockedUsers)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BlockedUsers>()
                .HasOne(bu => bu.BlockedUser)
                .WithMany()
                .HasForeignKey(bu => bu.BlockedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
