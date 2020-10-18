using System;

namespace Battleship.Models
{
    public enum ResultsOfTurnModels
    {
        miss,
        hit,
        kill,
        win
    }

    public enum StatusOfGameModels
    {
        NotReady,
        Ready,
        Started,
        Finished
    }

    public class BoardModel
    {
        public StatusOfGameModels statusOfGame { get; set; }
        public bool frstPlayerTurn { get; set; }
        public Guid idFirstPlayer { get; set; }
        public Guid idSecondPlayer { get; set; }
        public int[,] mapFirstPlayer { get; set; }
        public int[,] mapSecondPlayer { get; set; }
    }
}