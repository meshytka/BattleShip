using Battleship.Entities;
using System;
using System.Collections.Generic;

namespace Battleship.BLL.Contracts
{
    public interface IMapLogic
    {
        bool CheckMap(int[,] map);

        List<Ship> GetAllShips(int[,] map);

        int[,] GenerateMap();

        bool IsNewMap(int[,] map);

        IEnumerable<MapScheme> GetMapSchemes();

        int[,] GetMapSchemes(Guid id);

        MapSchemeResult SaveMapSchemes(int[,] newMap);
    }
}