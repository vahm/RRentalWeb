using RRental.Web.Api.Models;

namespace RRental.Web.Api.Helper
{
    public static class CalculatePoints
    {
        public static int Calculate(EquipmentType type)
        {
            int points;
            switch (type)
            {
                case EquipmentType.HeavyEquipment:
                    points = 2;
                    break;
                default:
                    points = 1;
                    break;
            }
            return points;
        }
    }
}