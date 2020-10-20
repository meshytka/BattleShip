using Battleship.BLL.Contracts;
using Battleship.BLL.Logic;
using Battleship.DAL.Contracts;
using Battleship.Entities;
using BattleShipTests.Helpers;
using Moq;
using NUnit.Framework;
using System;
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
            Mock<IMapDao> mock = new Mock<IMapDao>();
            mock.Setup(s => s.SaveMapSchemes(It.IsAny<Guid>(), It.IsAny<int[,]>())).Returns(MapSchemeResult.Success);
            mock.Setup(s => s.GetMapSchemes()).Returns(new List<MapScheme>());
            mock.Setup(s => s.GetMapSchemes(It.IsAny<Guid>())).Returns(new int[10,10]);

            mapLogic = new MapLogic(mock.Object);
        }

        [Test]
        public void CheckMapReturnTrueWhenWeCheckGoodMap()
        {
            var map = MapTestsHelper.goodMap();

            var checkResult = mapLogic.CheckMap(map);

            Assert.IsTrue(checkResult);
        }

        [Test]
        public void CheckMapReturnFalseWhenWeCheckBadMap()
        {
            var mapWhithBadPoint = MapTestsHelper.mapWhithBadPoint();

            var mapLessThatNeedShip = MapTestsHelper.mapLessThatNeedShip();

            var mapMoreThatNeedShip = MapTestsHelper.mapMoreThatNeedShip();

            var mapMoreSizeThatNeed = MapTestsHelper.mapMoreSizeThatNeed();

            var mapLessSizeThatNeed = MapTestsHelper.mapLessSizeThatNeed();

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

            List<Ship> ships = new List<Ship>()
            {
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (0, 0), (0,1)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (0, 3), (1,3), (2,3)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (0, 6), (0,7), (0,8), (0,9)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (2, 9), (3,9)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (4, 1), (5,1), (6,1)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (5, 7)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (8, 3)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (8, 5)
                    },
                },
                new Ship()
                {
                    Points = new List<(int, int)>()
                    {
                        (8, 8), (9,8)
                    },
                },
                new Ship()
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

        [Test]
        public void IsNewMapReturnTrueWhenSentNewMap()
        {
            var map = MapTestsHelper.goodMap();

            var result = mapLogic.IsNewMap(map);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsNewMapReturnFalseWhenSentNotNewMap()
        {
            var map = MapTestsHelper.notNewMap();

            var result = mapLogic.IsNewMap(map);

            Assert.IsFalse(result);
        }
    }
}