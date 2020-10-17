using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Linq;

namespace Battleship.DAL.BD
{
    public class GamesDao : IGameDao
    {
        GamesDaoContext _gamesDaoContext = new GamesDaoContext();

        public GamesDao()
        {

        }

        public Board GetGameWhithOnePlayer()
        {
            var game = _gamesDaoContext.Boards.FirstOrDefault(board => board.idSecondPlayer == Guid.Empty);

            return game;
        }

        public Board LoadGame(Guid id)
        {
            var game = _gamesDaoContext.Boards.FirstOrDefault(board => board.idFirstPlayer == id || board.idSecondPlayer == id);

            return game;
        }

        public void SaveGame(Board board)
        {
            _gamesDaoContext.Boards.Add(board);
            _gamesDaoContext.SaveChanges();
        }
    }
}
