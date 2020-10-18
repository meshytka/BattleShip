using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.DAL.BD
{
    public class MapSchemesDao : IMapSchemeDao
    {
        private MapSchemesDaoContext _mapSchemesDaoContext;

        public MapSchemesDao()
        {
            _mapSchemesDaoContext = new MapSchemesDaoContext();
        }

        public IEnumerable<MapScheme> GetMapSchemes()
        {
            var mapSchemes = _mapSchemesDaoContext.Maps;

            return mapSchemes.ToList();
        }

        public int[,] GetMapSchemes(Guid id)
        {
            var mapScheme = _mapSchemesDaoContext.Maps.FirstOrDefault(map => map.Id == id);

            if (mapScheme == null)
            {
                return null;
            }

            return mapScheme.map;
        }

        public MapSchemeResult SaveMapSchemes(Guid id, int[,] newMap)
        {
            if (_mapSchemesDaoContext.Maps.Any(m => m.map == newMap))
            {
                return MapSchemeResult.NotNewMapScheme;
            }

            MapScheme newMapScheme = new MapScheme()
            {
                Id = id,
                map = newMap
            };

            _mapSchemesDaoContext.Maps.Add(newMapScheme);
            _mapSchemesDaoContext.SaveChanges();

            return MapSchemeResult.Success;
        }
    }
}