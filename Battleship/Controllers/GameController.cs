using Battleship.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameLogic _gameLogic;

        public GameController(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        [HttpGet("Get/{id}")]
        public Guid GetIdByNewGame()
        {
            var newId = _gameLogic.NewGame();

            return newId;
        }
    }
}