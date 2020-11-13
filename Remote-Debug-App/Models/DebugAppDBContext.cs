using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Remote_Debug_App.Models
{
    public partial class DebugAppDBContext : DbContext
    {
        public DebugAppDBContext()
        {
        }

        public DebugAppDBContext(DbContextOptions<DebugAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DebugTable> DebugTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Debug-App-DB;Persist Security Info=True;User ID=admin;Password=neverwinter;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DebugTable>(entity =>
            {
                entity.ToTable("DebugTable");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.RandomGuid).IsRequired();

                entity.Property(e => e.SerializedObject)
                    .IsRequired()
                    .HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
