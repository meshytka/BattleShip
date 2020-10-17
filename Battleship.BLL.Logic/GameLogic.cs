using Battleship.BLL.Contracts;
using Battleship.DAL.Contracts;
using Battleship.Entities;
using System;
using System.Linq;

namespace Battleship.BLL.Logic
{
    public class GameLogic : IGameLogic
    {
        IGameDao _gameDao;
        Board _board;

        public GameLogic(IGameDao gameDao)
        {
            _gameDao = gameDao;
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

            SaveGame();

            return newId;
        }

        public ResultsOfTurn Turn(Guid id, (int, int) shot)
        {
            var game = LoadGame(id);

            if (game == null)
                throw new ArgumentNullException();

            if (game.frstPlayerTurn && id != _board.idFirstPlayer)
                throw new ArgumentException();

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
                    return ResultsOfTurn.win;
                }
            }
            else if (point == 2 || point == 4)
            {
                return ResultsOfTurn.miss;
            }
            else
            {
                map[shot.Item1, shot.Item2] = 3;
                SaveGame();

                return ResultsOfTurn.miss;
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
            MapLogic mapLogic = new MapLogic();

            _board = LoadGame(id);

            if (_board == null || _board.statusOfGame == StatusOfGame.Started || _board.statusOfGame == StatusOfGame.Finished || !mapLogic.CheckMap(map))
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

            SaveGame();

            return true;
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

        private ResultsOfTurn KillOrHit(int[,] map, (int, int) shot)
        {
            MapLogic mapLogic = new MapLogic();
            var allShips = mapLogic.GetAllShips(map);

            var ship = allShips.Where(ship => ship.Points.Contains(shot)).FirstOrDefault();

            if (ship.Points.Any(point => point != shot && map[point.Item1, point.Item2] == 1))
            {
                return ResultsOfTurn.hit;
            }

            return ResultsOfTurn.kill;
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
