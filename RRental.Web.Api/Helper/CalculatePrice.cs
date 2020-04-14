using RRental.Web.Api.Models;

namespace RRental.Web.Api.Helper
{
    public static class CalculatePrice
    {
        public static int Price(EquipmentType type, int duration)
        {
            int oneTimeFee = 100;
            int dailyFeePremium = 60;
            int dailyFeeRegular = 40;
            int price = 0;
            switch (type)
            {
                case EquipmentType.HeavyEquipment:
                    price = dailyFeePremium * duration + oneTimeFee;
                    break;
                case EquipmentType.RegularEquipment:
                    if (duration > 2)
                    {
                        price = dailyFeePremium * 2 + dailyFeeRegular * (duration - 2) + oneTimeFee;
                    }
                    else
                    {
                        price = dailyFeePremium * duration + oneTimeFee;
                    }
                    break;
                case EquipmentType.SpecializedEquipment:
                    if (duration > 3)
                    {
                        price = dailyFeePremium * 3 + dailyFeeRegular * (duration - 3);
                    }
                    else
                    {
                        price = dailyFeePremium * duration;
                    }
                    break;
            }
            return price;
        }
    }
}