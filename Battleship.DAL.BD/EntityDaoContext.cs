using Battleship.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleship.DAL.BD
{
    public class EntityDaoContext : DbContext
    {
        public virtual DbSet<Game> Boards { get; set; }
        public virtual DbSet<MapScheme> Maps { get; set; }

        public EntityDaoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Game>().HasKey(board => new
            {
                board.idFirstPlayer,
            });

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