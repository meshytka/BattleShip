using System;

namespace Battleship
{
    public class AddMap
    {
        private MapCheck mapCheck;

        public AddMap()
        {
            mapCheck = new MapCheck();
        }

        public bool LoadUserMap(Guid id, int[,] map)
        {
            if (CanUserLoadNewBoard(id) && mapCheck.CheckMap(map))
                return false;

            SaveMap(id, map);

            return true;
        }

        public bool CanUserLoadNewBoard(Guid id)
        {
            return true;
        }

        private void SaveMap(Guid id, int[,] board)
        {
        }
    }
}