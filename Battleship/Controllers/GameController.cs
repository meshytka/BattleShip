using Battleship.BLL.Contracts;
using Battleship.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Battleship.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : BaseController
    {
        private readonly IGameLogic _gameLogic;

        public GameController(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        [HttpGet("GetGetIdOfNewGame")]
        public JsonResult GetIdOfNewGame()
        {
            var newId = _gameLogic.NewGame();

            return MessageResult(newId.ToString());
        }

        [HttpGet("GetGeneratedMap")]
        public JsonResult GetGeneratedMap()
        {
            var map = _gameLogic.GenerateNewMap();

            return MessageResult(map.ToString());
        }

        [HttpGet("GetPlayerMap/{id}")]
        public JsonResult GetPlayerMap(Guid id)
        {
            var map = _gameLogic.GetPlayerMap(id);

            if (map == null)
            {
                return ErrorResponse("Invalid Player Id");
            }

            return MessageResult(map.ToString());
        }

        [HttpGet("GetEnemyMap/{id}")]
        public JsonResult GetEnemyMap(Guid id)
        {
            var map = _gameLogic.GetEnemyMap(id);

            if (map == null)
            {
                return ErrorResponse("Invalid Player Id");
            }

            return MessageResult(map.ToString());
        }

        [HttpPost("Turn")]
        public JsonResult Turn(Guid id, (int, int) shot)
        {
            var result = _gameLogic.Shoot(id, shot);

            if (result == ResultsOfShoot.canNotShoot)
            {
                return ErrorResponse("You can't shoot");
            }

            return MessageResult(result.ToString());
        }

        [HttpPost("AddNewUserMap")]
        public JsonResult AddNewUserMap(Guid id, int[,] map)
        {
            var result = _gameLogic.AddNewUserMap(id, map);

            if (result == false)
            {
                return ErrorResponse("You can't add new map");
            }

            return MessageResult("Map added successfully");
        }
    }
}