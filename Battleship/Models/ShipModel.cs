using System.Collections.Generic;

namespace Battleship.Api.Models
{
    public class ShipModel
    {
        public List<(int, int)> Points { get; set; }
    }
}