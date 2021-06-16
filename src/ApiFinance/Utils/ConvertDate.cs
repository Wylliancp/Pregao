using System;
using System.Collections.Generic;

namespace ApiFinance.Utils
{
    public static class ConvertDate
    {
        public static DateTime TimeStampToDateTime(this long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime(); 
            return dtDateTime;
        }
        public static List<DateTime> TimeToDatesTimes(this List<long> unixtimes)
        {
            var listaDateTime = new List<DateTime>();
            foreach(var itemUnix in unixtimes)
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(itemUnix).ToUniversalTime(); 
                listaDateTime.Add(dtDateTime);
            }
            
            return listaDateTime;
        }
    }
}