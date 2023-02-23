using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AspNetCoreDatatable.Entities;

#nullable disable

namespace AspNetCoreDatatable.Data
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
        public virtual DbSet<UserInfo> UserInfos { get; set; }

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
