using Microsoft.EntityFrameworkCore;

namespace Remote_Debug_App.Models
{
    public partial class DebugAppDbContext : DbContext
    {
        public DebugAppDbContext()
        {
        }

        public DebugAppDbContext(DbContextOptions<DebugAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DebugTable> DebugTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
