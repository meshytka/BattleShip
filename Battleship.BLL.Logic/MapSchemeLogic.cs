using Battleship.BLL.Contracts;
using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Collections.Generic;

namespace Battleship.BLL.Logic
{
    public class MapSchemeLogic : IMapSchemeLogic
    {
        private IMapLogic _mapLogic;
        private IMapSchemeDao _mapSchemeDao;

        public MapSchemeLogic(IMapSchemeDao mapSchemeDao, IMapLogic mapLogic)
        {
            _mapLogic = mapLogic;
            _mapSchemeDao = mapSchemeDao;
        }

        public IEnumerable<MapScheme> GetMapSchemes()
        {
            return _mapSchemeDao.GetMapSchemes();
        }

        public int[,] GetMapSchemes(Guid id)
        {
            return _mapSchemeDao.GetMapSchemes(id);
        }

        public MapSchemeResult SaveMapSchemes(int[,] newMap)
        {
            if (!_mapLogic.CheckMap(newMap) || !_mapLogic.IsNewMap(newMap))
            {
                return MapSchemeResult.BadMapScheme;
            }

            var id = Guid.NewGuid();

            return _mapSchemeDao.SaveMapSchemes(id, newMap);
        }
    }
}