using Battleship.Api.Helpers;
using Battleship.BLL.Contracts;
using Battleship.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Battleship.Api.Controllers
{
    public class MapSchemeController : BaseController
    {
        private readonly IMapLogic _mapLogic;

        public MapSchemeController(IMapLogic mapLogic)
        {
            _mapLogic = mapLogic;
        }

        [HttpGet("GetEnemyMap")]
        public JsonResult GetMapSchemes()
        {
            var mapSchemes = _mapLogic.GetMapSchemes();

            return MultipleResult(mapSchemes.Select(map => map.ToString()));
        }

        [HttpGet("GetEnemyMap/{id}")]
        public JsonResult GetMapSchemes(Guid id)
        {
            var map = _mapLogic.GetMapSchemes(id);

            return MessageResult(map.ToString());
        }

        [HttpPost("Turn")]
        public JsonResult SaveMapShemes(int[,] map)
        {
            var result = _mapLogic.SaveMapSchemes(map);

            if (result == MapSchemeResult.BadMapScheme || result == MapSchemeResult.NotNewMapScheme)
                return ErrorResponse(MapSchemeControllerHelper.ConvertToStringMapSchemeResult(result));

            return MessageResult(MapSchemeControllerHelper.ConvertToStringMapSchemeResult(result));
        }
    }
}