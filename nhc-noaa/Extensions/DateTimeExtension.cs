using System;

namespace nhc_noaa
{
    public static class DateTimeExtension
    {
        public static double Diff(this DateTime value)
        {
            return Math.Round((DateTime.Now - value).TotalMilliseconds);
        }
    }
}