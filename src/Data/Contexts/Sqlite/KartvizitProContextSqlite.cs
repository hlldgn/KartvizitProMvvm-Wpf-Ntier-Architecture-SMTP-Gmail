using Entites.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts.Sqlite
{
    public class KartvizitProContextSqlite:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=KartvizitPro.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(c =>
            {
                c.ToTable("Company").HasKey(k => k.Id);
                c.Property(p=>p.Name).IsRequired();
            });
            modelBuilder.UseCollation("Turkish_CI_AS");
        }
        public DbSet<Company> Company { get; set; }
    }
}
