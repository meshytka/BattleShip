using Battleship.BLL.Contracts;
using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Linq;

namespace Battleship.BLL.Logic
{
    public class GameLogic : IGameLogic
    {
        private IMapLogic _mapLogic;
        private IGameDao _gameDao;
        private Board _board;

        public GameLogic(IMapLogic mapLogic, IGameDao gameDao)
        {
            _gameDao = gameDao;
            _mapLogic = mapLogic;
            _board = new Board();
        }

        public Guid NewGame()
        {
            Guid newId = Guid.NewGuid();

            var gameWhithOnePlayer = GetGameWhithOnePlayer();

            if (gameWhithOnePlayer == null)
            {
                _board.idFirstPlayer = newId;
                _board.statusOfGame = StatusOfGame.NotReady;
            }
            else
            {
                _board.idSecondPlayer = newId;
            }

            _board.statusOfGame = StatusOfGame.NotReady;

            SaveGame();

            return newId;
        }

        public ResultsOfShoot Shoot(Guid id, (int, int) shot)
        {
            var game = LoadGame(id);

            if (game == null)
                return ResultsOfShoot.canNotShoot;

            if (game.frstPlayerTurn && id != _board.idFirstPlayer)
                return ResultsOfShoot.canNotShoot;

            if (game.statusOfGame == StatusOfGame.Ready)
            {
                game.statusOfGame = StatusOfGame.Started;
            }

            int[,] map = new int[10, 10];

            if (game.frstPlayerTurn)
            {
                map = _board.mapSecondPlayer;
            }
            else
            {
                map = _board.mapFirstPlayer;
            }

            var point = map[shot.Item1, shot.Item2];

            if (point == 1)
            {
                map[shot.Item1, shot.Item2] = 2;

                SaveGame();

                int countOf = 0;

                foreach (var item in map)
                {
                    if (item == 1)
                    {
                        countOf++;
                    }
                }

                if (countOf > 0)
                {
                    return KillOrHit(map, shot);
                }
                else
                {
                    _board.statusOfGame = StatusOfGame.Finished;
                    SaveGame();
                    return ResultsOfShoot.win;
                }
            }
            else if (point == 2 || point == 4)
            {
                return ResultsOfShoot.miss;
            }
            else
            {
                map[shot.Item1, shot.Item2] = 3;
                SaveGame();

                return ResultsOfShoot.miss;
            }
        }

        public int[,] GetPlayerMap(Guid id)
        {
            var board = LoadGame(id);

            if (board == null)
            {
                throw new ArgumentException();
            }

            var map = id == board.idFirstPlayer ? board.mapFirstPlayer : board.mapSecondPlayer;
            return map;
        }

        public int[,] GetEnemyMap(Guid id)
        {
            var board = LoadGame(id);

            if (board == null)
            {
                throw new ArgumentException();
            }

            var map = id == board.idFirstPlayer ? board.mapFirstPlayer : board.mapSecondPlayer;
            var anonimizeMap = AnonymizeMap(map);
            return anonimizeMap;
        }

        public bool AddNewUserMap(Guid id, int[,] map)
        {
            _board = LoadGame(id);

            if (_board == null || _board.statusOfGame == StatusOfGame.Started || _board.statusOfGame == StatusOfGame.Finished || !_mapLogic.CheckMap(map))
            {
                return false;
            }

            if (id == _board.idFirstPlayer)
            {
                _board.mapFirstPlayer = map;
            }
            else
            {
                _board.mapSecondPlayer = map;
            }

            if (_board.mapFirstPlayer != null && _board.mapSecondPlayer != null)
            {
                _board.statusOfGame = StatusOfGame.Ready;
            }

            SaveGame();

            return true;
        }

        public int[,] GenerateNewMap()
        {
            return _mapLogic.GenerateMap();
        }

        private int[,] AnonymizeMap(int[,] map)
        {
            for (var i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == 1)
                    {
                        map[i, j] = 0;
                    }
                }
            }

            return map;
        }

        private void SaveGame()
        {
            _gameDao.SaveGame(_board);
        }

        private ResultsOfShoot KillOrHit(int[,] map, (int, int) shot)
        {
            var allShips = _mapLogic.GetAllShips(map);

            var ship = allShips.Where(ship => ship.Points.Contains(shot)).FirstOrDefault();

            if (ship.Points.Any(point => point != shot && map[point.Item1, point.Item2] == 1))
            {
                return ResultsOfShoot.hit;
            }

            return ResultsOfShoot.kill;
        }

        private Board LoadGame(Guid id)
        {
            return _gameDao.LoadGame(id);
        }

        private Board GetGameWhithOnePlayer()
        {
            return _gameDao.GetGameWhithOnePlayer();
        }
    }
}