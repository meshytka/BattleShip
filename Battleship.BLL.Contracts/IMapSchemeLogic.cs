using Battleship.Entities;
using System;
using System.Collections.Generic;

namespace Battleship.BLL.Contracts
{
    public interface IMapSchemeLogic
    {
        IEnumerable<MapScheme> GetMapSchemes();

        int[,] GetMapSchemes(Guid id);

        MapSchemeResult SaveMapSchemes(int[,] newMap);
    }
}