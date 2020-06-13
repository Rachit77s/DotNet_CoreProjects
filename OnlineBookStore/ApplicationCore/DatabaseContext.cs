using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ApplicationCore
{
    public class DatabaseContext : IdentityDbContext<User, Role, int>
    {
        //Used for dependency Injection
        //This is the way of accepting the databse config setting either from the UI layer or Repository layer
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<User> AspNetUsers { get; set; }
        public DbSet<Role> AspNetRoles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Product> Products { get; set; }

        //We are defining primary keys and Index
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //define foreign key and disable cascade delete option
            modelBuilder.Entity<Order>().HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cart>().HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            modelBuilder.Entity<IdentityUserRole<int>>(i =>
            {
                i.HasKey(x => new { x.RoleId, x.UserId });
            });
            modelBuilder.Entity<IdentityUserLogin<int>>(i =>
            {
                i.HasIndex(x => new { x.ProviderKey, x.LoginProvider });
            });
            modelBuilder.Entity<IdentityRoleClaim<int>>(i =>
            {
                i.HasKey(x => x.Id);
            });
            modelBuilder.Entity<IdentityUserClaim<int>>(i =>
            {
                i.HasKey(x => x.Id);
            });
            modelBuilder.Entity<IdentityUserToken<int>>(i =>
            {
                i.HasKey(x => x.UserId);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
