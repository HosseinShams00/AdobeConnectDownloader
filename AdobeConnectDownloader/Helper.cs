using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdobeConnectDownloader
{
    public class Helper
    {
        public static string ConvertUintToDuration(uint milisecond)
        {
            int MiliSecond = (int)(milisecond % 1000);
            int second = (int)((milisecond / 1000) % 60);
            int min = (int)((milisecond / 1000) / 60);
            int houre = (int)(min / 60);
            min = min >= 60 ? min % 60 : min;
            string result = "";

            result += $"{((houre < 10) ? ("0" + houre) : houre)}:";
            result += $"{((min < 10) ? ("0" + min) : min)}:";
            result += $"{((second < 10) ? ("0" + second) : second)}.";

            for (int i = 0; i < 3 - MiliSecond.ToString().Length; i++)
                result += "0";
            result += MiliSecond;

            return result;
        }
    }
}
