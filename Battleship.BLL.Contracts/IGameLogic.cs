﻿using Battleship.Entities;
using System;

namespace Battleship.BLL.Contracts
{
    public interface IGameLogic
    {
        Guid NewGame();

        ResultsOfShoot Shoot(Guid id, (int, int) shot);

        int[,] GetPlayerMap(Guid id);

        int[,] GetEnemyMap(Guid id);

        bool AddNewUserMap(Guid id, int[,] map);

        int[,] GenerateNewMap();

        StatusOfGame GetStatusOfGame(Guid id);
    }
}