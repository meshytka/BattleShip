using Battleship.BLL.Contracts;
using Battleship.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.BLL.Logic
{
    public class MapLogic : IMapLogic
    {
        public bool CheckMap(int[,] map)
        {
            List<Ship> ships = new List<Ship>();

            if (!CheckMapSize(map))
            {
                return false;
            }

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

                        if (!IsGoodShip(ships, newShip))
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

        public int[,] GenerateMap()
        {
            var map = new int[10, 10];

            var ships = GenerateShips();

            map = AddShipsToMap(ships);

            return map;
        }

        private bool CheckMapSize(int[,] map)
        {
            if (map.Rank != 2 || map.Length != 100 || map.GetUpperBound(0) + 1 != 10)
            {
                return false;
            }

            return true;
        }

        private bool IsGoodShip(List<Ship> ships, Ship newShip)
        {
            var newShipSize = newShip.Points.Count();

            var countOfSameShips = ships.Where(p => p.Points.Count == newShipSize).Count();

            foreach (var point in newShip.Points)
            {
                if (!IsGoodPoint(ships, point))
                    return false;
            }

            if (newShipSize + countOfSameShips >= 5)
                return false;

            return true;
        }

        private bool IsAlreadyKnow(List<Ship> ships, int i, int j)
        {
            var point = (i, j);

            if (ships.Any(p => p.Points.Contains(point)))
                return true;
            return false;
        }

        private bool IsGoodPoint(List<Ship> ships, (int, int) newPoint)
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
                            return false;
                    }
                }
            }

            return true;
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
            else if (x != 9 && (map[x + 1, y] == 1 || map[x + 1, y] == 2))
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

        private List<Ship> GenerateShips()
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

                        if (IsGoodShip(ships, newShip))
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
            Random random = new Random();

            bool isHorizontal = random.Next(0, 99) < 50 ? true : false;

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
            Random random = new Random();

            int x = 0;
            int y = 0;

            x = random.Next(0, isHorizontal ? 10 : (10 - size));
            y = random.Next(0, isHorizontal ? (10 - size) : 10);

            return (x, y);
        }

        private int[,] AddShipsToMap(List<Ship> ships)
        {
            var map = new int[10, 10];

            foreach (var ship in ships)
            {
                foreach (var point in ship.Points)
                {
                    map[point.Item1, point.Item2] = 1;
                }
            }

            return map;
        }
    }
}