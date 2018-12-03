using System;

namespace PneuMalik.Models.Dto
{
    public enum Season
    {

        Summer = 1,
        Winter,
        Allyear,
        Unknown
    }

    public static class SeasonHelper
    {
        public static Season Parse(string input)
        {

            switch (input.ToLower())
            {
                case "summer":
                    return Season.Summer;
                case "winter":
                    return Season.Winter;
                case "allyear":
                    return Season.Allyear;
                default:
                    return Season.Unknown;
            }
        }
    }
}