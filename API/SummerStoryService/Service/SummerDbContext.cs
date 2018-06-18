using Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Text;

namespace Service
{
    public class SummerDbContext : DbContext
    {
        public SummerDbContext() : base("name=connStr")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RecordEntity> Records { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<TextEntity> Texts { get; set; }
    }
}
