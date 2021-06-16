using Microsoft.EntityFrameworkCore;
using ApiFinance.Models;

namespace ApiFinance.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ativo>().Property(t => t.Id).UseIdentityColumn();
          
            modelBuilder.Entity<Pregao>()
            .Property(t => t.Id)
            .UseIdentityColumn();

            modelBuilder.Entity<Pregao>()
            .Property(t => t.Low);
            modelBuilder.Entity<Pregao>()
            .Property(t => t.High);
            modelBuilder.Entity<Pregao>()
            .Property(t => t.Open);
            modelBuilder.Entity<Pregao>()
            .Property(t => t.Close);
            modelBuilder.Entity<Pregao>()
            .Property(t => t.Volume);
            modelBuilder.Entity<Pregao>()
            .Property(t => t.VariacaoD1);
            modelBuilder.Entity<Pregao>()
            .Property(t => t.VariacaoDAnt);
        }

        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Pregao> Pregaos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}