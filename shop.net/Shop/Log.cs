using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop
{
    public class Log
    {
        public static string leadTime = "Время выполнения метода ";
        public static string StrToLog(long milliSeconds)
        {
            return $"completed in {milliSeconds} ms";
        }
        public static string StrToLog(long milliSeconds, string message)
        {
            var resultStr = $"completed in {milliSeconds} ms | {message}";
            return resultStr;
        }
    }
}