using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.DAL.BD
{
    public class MapDao : IMapDao
    {
        private EntityDaoContext _entityDaoContext;

        public MapDao()
        {
            _entityDaoContext = new EntityDaoContext();
        }

        public IEnumerable<MapScheme> GetMapSchemes()
        {
            var mapSchemes = _entityDaoContext.Maps;

            return mapSchemes.ToList();
        }

        public int[,] GetMapSchemes(Guid id)
        {
            var mapScheme = _entityDaoContext.Maps.FirstOrDefault(map => map.Id == id);

            if (mapScheme == null)
            {
                return null;
            }

            return mapScheme.map;
        }

        public MapSchemeResult SaveMapSchemes(Guid id, int[,] newMap)
        {
            if (_entityDaoContext.Maps.Any(m => m.map == newMap))
            {
                return MapSchemeResult.NotNewMapScheme;
            }

            MapScheme newMapScheme = new MapScheme()
            {
                Id = id,
                map = newMap
            };

            _entityDaoContext.Maps.Add(newMapScheme);
            _entityDaoContext.SaveChanges();

            return MapSchemeResult.Success;
        }
    }
}