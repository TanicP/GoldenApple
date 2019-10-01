using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GoldenAppleApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Property> Propertys { get; set; }
        public DbSet<Data> Datas { get; set; }
        public DbSet<SubData> SubDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GoldenAppledb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data>()
                .HasOne(s => s.Prop)
                .WithMany(b => b.Data)
                .IsRequired();

            modelBuilder.Entity<SubData>()
                .HasOne(s => s.data)
                .WithMany(b => b.SubData)
                .IsRequired();

        }
        public ApplicationContext()
        {
      //      Database.EnsureCreated();
        }
        
        
    }
}
