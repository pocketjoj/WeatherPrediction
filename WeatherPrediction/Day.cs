using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction
{
    public class Day : IDay
    {
        public double Rain { get; set; }
        public double Snow { get; set; }
        public int TempHigh { get; set; }
        public int TempLow { get; set; }

        public bool CheckRain()
        {
            if (Rain > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool CheckSnow()
        {
            if (Snow > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        public string CheckPrecipitation()
        {
            string text = null;

            if (Rain == 0 && Snow == 0)
            {
                text = "no rain or snow";
            }

            else if (Rain > 0 && Snow == 0)
            {
                if (Rain < .5)
                {
                    text = "light rain";
                }

                else if (Rain >= .5 && Rain < 1)
                {
                    text = "moderate rain";
                }

                else if (Rain > 1)
                {
                    text = "heavy rain";
                }
            }

            else if (Rain == 0 && Snow > 0)
            {
                if (Snow < 2)
                {
                    text = "light snow";
                }

                else if (Snow >= 1 && Snow < 6)
                {
                    text = "moderate snow";
                }

                else if (Snow > 6)
                {
                    text = "heavy snow";
                }
            }

            else if (Rain > 0 && Snow > 0)
            {
                if (Rain < .5)
                {
                    text = "light rain";
                }

                else if (Rain >= .5 && Rain < 1)
                {
                    text = "moderate rain";
                }

                else if (Rain > 1)
                {
                    text = "heavy rain";
                }

                if (Snow < 2)
                {
                    text += " and light snow";
                }

                else if (Snow >= 1 && Snow < 6)
                {
                    text += " and moderate snow";
                }

                else if (Snow > 6)
                {
                    text += " and heavy snow";
                }
            }

            return text;
        }
    }
}
