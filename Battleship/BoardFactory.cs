using Battleship.Models;
using System;
using System.Collections.Generic;

namespace Battleship
{
    public interface IBoardFactory
    {
        public int[,] GenerateBoard();
    }

    public class BoardFactory : IBoardFactory
    {
        private Random _random;

        public BoardFactory()
        {
            _random = new Random();
        }

        public int[,] GenerateBoard()
        {
            var board = new int[10, 10];
            MapCheck boardCheck = new MapCheck();

            var ships = GenerateShips(boardCheck);

            board = AddShipsToBoard(ships);

            return board;
        }

        private List<Ship> GenerateShips(MapCheck boardCheck)
        {
            var ships = new List<Ship>();

            for (var shipSize = 1; shipSize < 5; shipSize++)
            {
                int numOfShips = 0;

                while (numOfShips + shipSize < 5)
                {
                    bool sucseesGenerate = false;

                    while (!sucseesGenerate)
                    {
                        var newShip = GenerateShip(shipSize);

                        if (!boardCheck.IsBadShip(ships, newShip))
                        {
                            ships.Add(newShip);
                            sucseesGenerate = true;
                        }
                    }
                }
            }

            return ships;
        }

        private Ship GenerateShip(int size)
        {
            Ship ship = new Ship();

            bool isHorizontal = _random.Next(0, 99) < 50 ? true : false;

            (int, int) firstPoint = GenerateFirstPoint(size, isHorizontal);

            ship.Points.Add(firstPoint);

            for (int i = 0; i < size; i++)
            {
                int x = firstPoint.Item1 + (isHorizontal ? 0 : i);
                int y = firstPoint.Item2 + (isHorizontal ? i : 0);

                ship.Points.Add((x, y));
            }

            return ship;
        }

        private (int, int) GenerateFirstPoint(int size, bool isHorizontal)
        {
            int x = 0;
            int y = 0;

            x = _random.Next(0, isHorizontal ? 10 : (10 - size));
            y = _random.Next(0, isHorizontal ? (10 - size) : 10);

            return (x, y);
        }

        private int[,] AddShipsToBoard(List<Ship> ships)
        {
            var board = new int[10, 10];

            foreach (var ship in ships)
            {
                foreach (var point in ship.Points)
                {
                    board[point.Item1, point.Item2] = 1;
                }
            }

            return board;
        }
    }
}