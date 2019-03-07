using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WeatherPrediction
{
    class Program
    {
        static void Main(string[] args)
        {
            //This builds a Dictionary to store data from WeatherData.csv, storing a key value pair of <date, Day object>.
            var WeatherData = new Dictionary<string, Day>();

            using (StreamReader sr = new StreamReader("../../../WeatherData/WeatherData.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var row = sr.ReadLine().Split(',');

                    WeatherData.Add(row[0], new Day
                    {
                        Rain = Convert.ToDouble(row[1]),
                        Snow = Convert.ToDouble(row[2]),
                        TempHigh = Convert.ToInt32(row[3]),
                        TempLow = Convert.ToInt32(row[4])
                    });
                }
            }

            //Code below is menu loop. If data is appended to WeatherData.csv, it will append at end of each loop. 
            while (true)
            {
                Console.WriteLine("Welcome to my Weather Almanac!");
                Console.WriteLine();
                Console.WriteLine("To locate historical weather information for a specific day, please press 1.");
                Console.WriteLine();
                Console.WriteLine("To get your weather prediction for a specific day of the year, please press 2.");
                Console.WriteLine();
                Console.WriteLine("To input new weather data for a specific date, please press 3.");
                Console.WriteLine();
                Console.WriteLine("To exit this program, please type 'quit'.");
                Console.WriteLine();
                var response = Console.ReadLine();
                Console.WriteLine();

                if (response.ToUpper() == "QUIT")
                {
                    break;
                }
                //Option one prints temperature data to console and checks for rain/snow fall, printing data if there was rain/snow.
                else if (response == "1")
                {
                    HelperMethods.MenuOptionOne(WeatherData);
                }

                //Option two retrieves a prediction based on historical averages for a specific month and day.
                else if (response == "2")
                {
                    HelperMethods.MenuOptionTwo(WeatherData);

                }

                else if (response == "3")
                {

                }
            }

        }

        
    }
}
