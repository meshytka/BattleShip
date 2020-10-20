using System;

namespace Battleship.Entities
{
    public enum ResultsOfShoot
    {
        canNotShoot,
        miss,
        hit,
        kill,
        win
    }

    public enum StatusOfGame
    {
        NotCreated,
        NotReady,
        Ready,
        Started,
        Finished
    }

    public class Game
    {
        public StatusOfGame statusOfGame { get; set; }
        public bool frstPlayerTurn { get; set; }
        public Guid idFirstPlayer { get; set; }
        public Guid idSecondPlayer { get; set; }
        public int[,] mapFirstPlayer { get; set; }
        public int[,] mapSecondPlayer { get; set; }
    }
}