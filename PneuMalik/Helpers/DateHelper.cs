using System;

namespace ExtensionMethods
{
    public static class DateHelper
    {

        public static bool Between(this DateTime date, DateTime from, DateTime to)
        {

            if (from.Month > to.Month)
            {

                if (((from.Month < date.Month) || (from.Month == date.Month && from.Day <= date.Day)) ||
                    ((to.Month > date.Month) || (to.Month == date.Month) && to.Day >= date.Day))
                {
                    return true;
                }
            }
            else
            {

                if (((from.Month < date.Month) || (from.Month == date.Month && from.Day <= date.Day)) &&
                    ((to.Month > date.Month) || (to.Month == date.Month) && to.Day >= date.Day))
                {
                    return true;
                }
            }

            return false;
        }
    }
}