using Battleship.Entities;
using System;

namespace Battleship.DAL.Contracts
{
    public interface IGameDao
    {
        void SaveGame(Game board);

        Game LoadGame(Guid id);

        Game GetGameWhithOnePlayer();

        StatusOfGame GetStatusOfGame(Guid id);
    }
}