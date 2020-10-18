using Battleship.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleship.DAL.BD
{
    public class MapSchemesDaoContext : DbContext
    {
        public virtual DbSet<MapScheme> Maps { get; set; }

        public MapSchemesDaoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MapScheme>().HasKey(map => new
            {
                map.Id,
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Battleship;User Id=postgres;Password=Zhjckfd02;");
        }
    }
}