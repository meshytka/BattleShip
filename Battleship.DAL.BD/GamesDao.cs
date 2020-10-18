using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Linq;

namespace Battleship.DAL.BD
{
    public class GamesDao : IGameDao
    {
        private GamesDaoContext _gamesDaoContext;

        public GamesDao()
        {
            _gamesDaoContext = new GamesDaoContext();
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
            if (_gamesDaoContext.Boards.Any(b => b.idFirstPlayer == board.idFirstPlayer))
            {
                _gamesDaoContext.Boards.Update(board);
            }
            else
            {
                _gamesDaoContext.Boards.Add(board);
            }

            _gamesDaoContext.SaveChanges();
        }
    }
}