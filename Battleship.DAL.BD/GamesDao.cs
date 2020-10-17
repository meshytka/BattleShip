using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;

namespace Battleship.DAL.BD
{
    public class GamesDao : IGameDao
    {
        public Board GetGameWhithOnePlayer()
        {
            throw new NotImplementedException();
        }

        public Board LoadGame(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveGame(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
