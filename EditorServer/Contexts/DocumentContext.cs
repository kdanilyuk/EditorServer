using EditorServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EditorServer.Contexts
{
    public class DocumentContext : DbContext
    {
        private readonly string _connectionString;

        public DocumentContext() { }

        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options) { }

        public DocumentContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<DocumentDTO> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentContext).Assembly);

            modelBuilder.Entity<DocumentDTO>()
                        .HasOne(x => x.Parent)
                        .WithMany(x => x.Childrens)
                        .HasForeignKey(x => x.ParentId);
        }
    }
}
