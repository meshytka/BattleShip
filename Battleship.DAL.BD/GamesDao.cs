using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Linq;

namespace Battleship.DAL.BD
{
    public class GamesDao : IGameDao
    {
        private EntityDaoContext _entityDaoContext;

        public GamesDao()
        {
            _entityDaoContext = new EntityDaoContext();
        }

        public Game GetGameWhithOnePlayer()
        {
            var game = _entityDaoContext.Boards.FirstOrDefault(board => board.idSecondPlayer == Guid.Empty);

            return game;
        }

        public Game LoadGame(Guid id)
        {
            var game = _entityDaoContext.Boards.FirstOrDefault(board => board.idFirstPlayer == id || board.idSecondPlayer == id);

            return game;
        }

        public void SaveGame(Game board)
        {
            if (_entityDaoContext.Boards.Any(b => b.idFirstPlayer == board.idFirstPlayer))
            {
                _entityDaoContext.Boards.Update(board);
            }
            else
            {
                _entityDaoContext.Boards.Add(board);
            }

            _entityDaoContext.SaveChanges();
        }

        public StatusOfGame GetStatusOfGame(Guid id)
        {
            var status = _entityDaoContext.Boards.Where(b => b.idFirstPlayer == id || b.idSecondPlayer == id).FirstOrDefault().statusOfGame;

            return status;
        }
    }
}