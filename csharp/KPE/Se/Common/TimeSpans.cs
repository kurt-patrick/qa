using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common
{
    public static class TimeSpans
    {
        public static TimeSpan Sec2 = TimeSpan.FromSeconds(2);
        public static TimeSpan Sec5 = TimeSpan.FromSeconds(5);
        public static TimeSpan Sec10 = TimeSpan.FromSeconds(10);
        public static TimeSpan TimeOutDefault = TimeSpan.FromSeconds(Periods.TimeOutDefault);
    }

    public static class Periods
    {
        public const int Two = 2;
        public const int Five = 5;
        public const int Ten = 10;
        public const int TimeOutDefault = 10;
    }
}
