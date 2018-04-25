﻿namespace BankSystem.Data.Database
{
    using BankSystem.Models.Models;
    using BankSystem.Models.Models.Accounts;
    using Microsoft.EntityFrameworkCore;
    public class BankSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<SavingsAccount> SavingAccounts { get; set; }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-924HS8U\SQLEXPRESS;Database=BankSystem;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.SavingAccounts)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CheckingAccounts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<SavingsAccount>()
                .HasOne(s => s.User)
                .WithMany(u => u.SavingAccounts);

            modelBuilder.Entity<CheckingAccount>()
                .HasOne(c => c.User)
                .WithMany(u => u.CheckingAccounts);
        }
    }
}