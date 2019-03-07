using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction
{
    interface IDay
    {
        double Rain { get; set; }
        double Snow { get; set; }
        int TempHigh { get; set; }
        int TempLow { get; set; }

        bool CheckRain();
        bool CheckSnow();
        string CheckPrecipitation();
    }
}
