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

        public bool CheckMap(int[,] map)
        {
            List<Ship> ships = new List<Ship>();

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var point = map[i, j];

                    if (point == 1)
                    {
                        if (IsAlreadyKnow(ships, i, j))
                            continue;

                        var newShip = GetShipByFirstPoint(map, i, j);

                        if (IsBadShip(ships, newShip))
                            return false;

                        ships.Add(newShip);
                    }
                }
            }

            if (ships.Count() != 10)
            {
                return false;
            }

            return true;
        }

        public List<Ship> GetAllShips(int[,] map)
        {
            List<Ship> ships = new List<Ship>();

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var point = map[i, j];

                    if (point == 1 || point == 2)
                    {
                        if (IsAlreadyKnow(ships, i, j))
                            continue;

                        var newShip = GetShipByFirstPoint(map, i, j);

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

        private Ship GetShipByFirstPoint(int[,] map, int x, int y)
        {
            Ship ship = new Ship();
            ship.Points = new List<(int, int)>();

            if (y != 9 && (map[x, y + 1] == 1 || map[x, y + 1] == 2))
            {
                int g = y;

                while (g < 10 && (map[x, g] == 1 || map[x, g] == 2))
                {
                    ship.Points.Add((x, g));
                    g++;
                }
            }
            else if (x != 9 && (map[x + 1, y] == 1 || map[x, y + 1] == 2))
            {
                int k = x;

                while (k < 10 && (map[k, y] == 1 || map[k, y] == 2))
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

            foreach (var point in newShip.Points)
            {
                if (IsBadPoint(ships, point))
                    return false;
            }

            if (newShipSize > 4 || newShipSize + countOfSameShips == 5)
                return true;

            return false;
        }
    }
}