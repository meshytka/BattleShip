using Battleship.BLL.Contracts;
using Battleship.BLL.Logic;
using Battleship.Models;
using BattleShipTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipTests
{
    public class MapLogicTests
    {
        private IMapLogic mapLogic;

        [SetUp]
        public void Setup()
        {
            mapLogic = new MapLogic();
        }

        [Test]
        public void CheckMapReturnTrueWhenWeCheckGoodMap()
        {
            var map = MapHelper.goodMap();

            var checkResult = mapLogic.CheckMap(map);

            Assert.IsTrue(checkResult);
        }

        [Test]
        public void CheckMapReturnFalseWhenWeCheckBadMap()
        {
            var mapWhithBadPoint = MapHelper.mapWhithBadPoint();

            var mapLessThatNeedShip = MapHelper.mapLessThatNeedShip();

            var mapMoreThatNeedShip = MapHelper.mapMoreThatNeedShip();

            var mapMoreSizeThatNeed = MapHelper.mapMoreSizeThatNeed();

            var mapLessSizeThatNeed = MapHelper.mapLessSizeThatNeed();

            Assert.IsFalse(mapLogic.CheckMap(mapMoreThatNeedShip));
            Assert.IsFalse(mapLogic.CheckMap(mapLessThatNeedShip));
            Assert.IsFalse(mapLogic.CheckMap(mapWhithBadPoint));
            Assert.IsFalse(mapLogic.CheckMap(mapMoreSizeThatNeed));
            Assert.IsFalse(mapLogic.CheckMap(mapLessSizeThatNeed));
        }

        [Test]
        public void GetAllShipsReturnAllShipsThatWeInter()
        {
            var map = new int[10, 10]
            {
                { 1,1,0,1,0,0,1,1,1,1 },
                { 0,0,0,1,0,0,0,0,0,0 },
                { 0,0,0,1,0,0,0,0,0,1 },
                { 0,0,0,0,0,0,0,0,0,1 },
                { 0,1,0,0,0,0,0,0,0,0 },
                { 0,1,0,0,0,0,0,1,0,0 },
                { 0,1,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,1,0,1,0,0,1,0 },
                { 1,0,0,0,0,0,0,0,1,0 }
            };

            List<ShipModel> ships = new List<ShipModel>()
            {
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (0, 0), (0,1)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (0, 3), (1,3), (2,3)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (0, 6), (0,7), (0,8), (0,9)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (2, 9), (3,9)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (4, 1), (5,1), (6,1)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (5, 7)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (8, 3)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (8, 5)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (8, 8), (9,8)
                    },
                },
                new ShipModel()
                {
                    Points = new List<(int, int)>()
                    {
                        (9, 0)
                    },
                }
            };

            var shipsFromMethod = mapLogic.GetAllShips(map);

            var result = true;

            foreach (var ship in ships)
            {
                var shipFromMethod = shipsFromMethod.Where(s => s.Points.Contains(ship.Points.First())).First();

                foreach (var point in ship.Points)
                {
                    if (!shipFromMethod.Points.Contains(point))
                    {
                        result = false;
                    }
                }
            }

            Assert.IsTrue(result);
        }
    }
}