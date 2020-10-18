using System;

namespace Battleship.Entities
{
    public enum MapSchemeResult
    {
        BadMapScheme,
        NotNewMapScheme,
        Success
    }

    public class MapScheme
    {
        public Guid Id { get; set; }
        public int[,] map { get; set; }
    }
}