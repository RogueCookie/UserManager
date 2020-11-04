using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UserManager.DAL.Enum;
using UserManager.DAL.Models;

namespace UserManager.DAL
{
    public class UserManagerContext : DbContext
    {
        public UserManagerContext(DbContextOptions<UserManagerContext> option): base(option)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users").HasKey(x => x.Id);
            var users = new List<User>()
            {
                new User() {Id = 1, UserName = "tom@gmail.com", UserStatus = UserStatus.Active, Password = "123"},
                new User() {Id = 2, UserName = "alice@yahoo.com", UserStatus = UserStatus.Blocked},
                new User() {Id = 3, UserName = "sam@online.com", UserStatus = UserStatus.Deleted},
                new User() {Id = 4, UserName = "val@online.com", UserStatus = UserStatus.New}
            };

            modelBuilder.Entity<User>().HasData(users);
            base.OnModelCreating(modelBuilder);
        }
    }
}
