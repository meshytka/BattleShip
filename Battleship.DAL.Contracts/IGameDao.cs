using Battleship.Entities;
using System;

namespace Battleship.DAL.Contracts
{
    public interface IGameDao
    {
        void SaveGame(Board board);

        Board LoadGame(Guid id);

        Board GetGameWhithOnePlayer();
    }
}