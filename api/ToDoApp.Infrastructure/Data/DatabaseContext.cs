using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Core.Data.Entities;

namespace ToDoApp.Infrastructure.Data
{
    public class DatabaseContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public DbSet<TodoEntity> Todos { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TodoEntity>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Title).HasMaxLength(300);
                b.Property(e => e.IsCompleted).HasDefaultValue(false);
                b.HasOne(e => e.Creator)
                    .WithMany(u => u.Todos)
                    .HasForeignKey(e => e.CreatorId);
                b.HasOne(e => e.Priority)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(e => e.PriorityLevel)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(e => e.AssignedUser)
                    .WithMany()
                    .HasForeignKey(e => e.AssignedUserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<TodoPriorityEntity>(b =>
            {
                b.HasKey(e => e.Level);
                b.ToTable("TodoPriorities");
            });
        }
    }
}
