﻿using DocaSub.Models;
using Microsoft.EntityFrameworkCore;

namespace DocaSub.Data
{
    public class DocaDbContext : DbContext
    {
        public DocaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SubRequest> SubRequests { get; set; }

        public DbSet<Subvention> Subventions { get; set; }


        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("doca");

        }
    }
}
