using Battleship.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.BLL.Contracts
{
    public interface IMapLogic
    {
        bool CheckMap(int[,] map);
        List<Ship> GetAllShips(int[,] map);

        int[,] GenerateMap();
    }
}
