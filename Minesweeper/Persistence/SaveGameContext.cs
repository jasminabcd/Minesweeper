using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efCoreTest
{
    public class MinesweeperContext : DbContext
    {
        public DbSet<PersistenceField> Fields => Set<PersistenceField>();
        public DbSet<PersistenceGame> Games => Set<PersistenceGame>();
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=.;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersistenceField>()
                .HasOne<PersistenceGame>()
                .WithMany(g => g.Fields)
                .HasForeignKey(f => f.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PersistenceField>()
                .Property(c => c.GameID)
                .IsRequired();



            base.OnModelCreating(modelBuilder);
        }


    }


}
