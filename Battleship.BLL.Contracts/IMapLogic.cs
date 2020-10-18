using Battleship.Entities;
using System.Collections.Generic;

namespace Battleship.BLL.Contracts
{
    public interface IMapLogic
    {
        bool CheckMap(int[,] map);

        List<Ship> GetAllShips(int[,] map);

        int[,] GenerateMap();
    }
}