using System;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int[,] map { get; set; }
    }
}