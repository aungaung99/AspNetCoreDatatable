using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AspNetCoreDatatablePagination.Entities;

#nullable disable

namespace AspNetCoreDatatablePagination.Data
{
    public partial class AspNetCoreDatatableContext : DbContext
    {
        public AspNetCoreDatatableContext()
        {
        }

        public AspNetCoreDatatableContext(DbContextOptions<AspNetCoreDatatableContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Street> Streets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Street>(entity =>
            {
                entity.Property(e => e.StreetId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
