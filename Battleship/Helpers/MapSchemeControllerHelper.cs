using Battleship.Entities;

namespace Battleship.Api.Helpers
{
    public static class MapSchemeControllerHelper
    {
        public static string ConvertToStringMapSchemeResult(MapSchemeResult mapSchemeResult)
        {
            if (mapSchemeResult == MapSchemeResult.BadMapScheme)
            {
                return "Bad map scheme";
            }
            else if (mapSchemeResult == MapSchemeResult.NotNewMapScheme)
            {
                return "This map schema is not new";
            }
            else
            {
                return "Map scheme added successfully";
            }
        }
    }
}