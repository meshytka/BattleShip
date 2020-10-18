using Battleship.Entities;
using System;
using System.Collections.Generic;

namespace Battleship.DAL.Contracts
{
    public interface IMapSchemeDao
    {
        IEnumerable<MapScheme> GetMapSchemes();

        int[,] GetMapSchemes(Guid id);

        MapSchemeResult SaveMapSchemes(Guid id, int[,] newMap);
    }
}