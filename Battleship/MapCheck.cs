using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship
{
    public class MapCheck
    {
        public bool CanUserLoadNewMap(Guid id)
        {
            return true;
        }

        public bool CheckMap(int[,] board)
        {
            List<Ship> ships = new List<Ship>();

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var point = board[i, j];

                    if (point == 1)
                    {
                        if (IsAlreadyKnow(ships, i, j))
                            continue;

                        var newShip = GetShipByFirstPoint(board, i, j);

                        if (IsBadShip(ships, newShip))
                            return false;

                        ships.Add(newShip);
                    }
                }
            }

            return true;
        }

        public List<Ship> GetAllShips(int[,] board)
        {
            List<Ship> ships = new List<Ship>();

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var point = board[i, j];

                    if (point == 1 || point == 2)
                    {
                        if (IsAlreadyKnow(ships, i, j))
                            continue;

                        var newShip = GetShipByFirstPoint(board, i, j);

                        ships.Add(newShip);
                    }
                }
            }

            return ships;
        }

        private bool IsAlreadyKnow(List<Ship> ships, int i, int j)
        {
            var point = (i, j);

            if (ships.Any(p => p.Points.Contains(point)))
                return true;
            return false;
        }

        private bool IsBadPoint(List<Ship> ships, (int, int) newPoint)
        {
            foreach (var ship in ships)
            {
                foreach (var point in ship.Points)
                {
                    int x = point.Item1;
                    int y = point.Item2;

                    if (newPoint.Item1 >= x - 1 && newPoint.Item1 <= x + 1)
                    {
                        if (newPoint.Item2 >= y - 1 && newPoint.Item2 <= y + 1)
                            return true;
                    }
                }
            }

            return false;
        }

        private Ship GetShipByFirstPoint(int[,] board, int x, int y)
        {
            Ship ship = new Ship();
            ship.Points = new List<(int, int)>();

            if (y != 10 && (board[x, y + 1] == 1 || board[x, y + 1] == 2))
            {
                int g = y;

                while (g <= 10 && (board[x, g] == 1 || board[x, y + 1] == 2))
                {
                    ship.Points.Add((x, g));
                    g++;
                }
            }
            else if (x != 10 && (board[x + 1, y] == 1 || board[x, y + 1] == 2))
            {
                int k = x;

                while (k <= 10 && (board[k, y] == 1 || board[x, y + 1] == 2))
                {
                    ship.Points.Add((k, y));
                    k++;
                }
            }
            else
            {
                ship.Points.Add((x, y));
            }

            return ship;
        }

        public bool IsBadShip(List<Ship> ships, Ship newShip)
        {
            var newShipSize = newShip.Points.Count();

            var countOfSameShips = ships.Where(p => p.Points.Count == newShipSize).Count();

            if (newShipSize > 4 || newShipSize + countOfSameShips == 5)
                return true;

            foreach (var point in newShip.Points)
            {
                if (IsBadPoint(ships, point))
                    return false;
            }

            return false;
        }
    }
}