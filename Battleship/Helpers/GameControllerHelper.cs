using Battleship.Entities;

namespace Battleship.Api.Helpers
{
    public static class GameControllerHelper
    {
        public static string ConvertToStringStatusOfGame(StatusOfGame statusOfGame)
        {
            if (statusOfGame == StatusOfGame.NotReady)
            {
                return "Game is not ready";
            }
            else if (statusOfGame == StatusOfGame.Ready)
            {
                return "Game is ready";
            }
            else if (statusOfGame == StatusOfGame.Started)
            {
                return "The game started";
            }
            else if (statusOfGame == StatusOfGame.Finished)
            {
                return "Game over";
            }
            else
            {
                return "Game not created";
            }
        }

        public static string ConvertToStringResultsOfShoot(ResultsOfShoot resultsOfShoot)
        {
            if (resultsOfShoot == ResultsOfShoot.miss)
            {
                return "Miss";
            }
            else if (resultsOfShoot == ResultsOfShoot.hit)
            {
                return "Hit";
            }
            else if (resultsOfShoot == ResultsOfShoot.kill)
            {
                return "Kill ship";
            }
            else if (resultsOfShoot == ResultsOfShoot.win)
            {
                return "You win!";
            }
            else
            {
                return "You can't shoot";
            }
        }
    }
}