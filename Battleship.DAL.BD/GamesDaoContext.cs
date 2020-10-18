using Battleship.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleship.DAL.BD
{
    public class GamesDaoContext : DbContext
    {
        public virtual DbSet<Board> Boards { get; set; }

        public GamesDaoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Board>().HasKey(board => new
            {
                board.idFirstPlayer,
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Battleship;User Id=postgres;Password=Zhjckfd02;");
        }
    }
}