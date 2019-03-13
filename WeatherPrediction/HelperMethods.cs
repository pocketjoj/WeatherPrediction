using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WeatherPrediction
{
    public static class HelperMethods
    {
        //Method to take MM/DD input and convert it to MM/DD/YY input for WeatherData Dictionary.
        public static string ToKey(string input, string year)
        {
            var Key = input + "/" + year;
            return Key;
        }

        //Method to format data for a historical day that will be written to Console or external file.
        public static string GetHistoricalText(Day day, string date)
        {
            var text = "There was " + day.CheckPrecipitation() + " on " + date + ".";
            return text;
        }

        //Method to format data for a future day that will be written to Console or external file.
        public static string GetPredictionText(Day day, string date)
        {
            var text = "There most likely be " + day.CheckPrecipitation() + " on " + date + ".";
            return text;
        }

        //Menu option one prints temperature data to console and checks for rain/snow fall, printing data if there was rain/snow.
        public static void MenuOptionOne(Dictionary<string, Day> data)
        {
            Console.WriteLine("Type the date for which you wanter weather info in MM/DD/YY format Data is available from 01/01/09 - 01/29/19.");
            Console.WriteLine();
            var date = Console.ReadLine();

            if (date.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            if (!data.ContainsKey(date))
            {
                Console.WriteLine("No data was found for that day.");
                Console.WriteLine("Please enter a date in MM/DD/YY format between 01/01/09 and 01/29/19.");
                Console.WriteLine("Press any key to try again.");
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();
                MenuOptionOne(data);

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The high for " + date + " was " + data[date].TempHigh + " degrees, and the low was " + data[date].TempLow + " degrees.");
                Console.WriteLine(GetHistoricalText(data[date], date));
                Console.WriteLine();
                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        //Menu option two takes a day (in MM/DD format) and returns a prediction for that day based on historical averages in the WeatherData Dictionary.
        public static void MenuOptionTwo(Dictionary<string, Day> data)
        {
            Console.WriteLine("Please provide the month and day in MM/DD format to receive a weather estimate for that day.");
            Console.WriteLine();
            var date = Console.ReadLine();

            if (date.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            //This list will contain each date (from MM/DD selected by user) in each year from 2009-2020. This will be used as keys to obtain values from WeatherData Dictionary.
            List<string> dates = new List<string>(12)
                    {   ToKey(date, "09"),
                        ToKey(date, "10"),
                        ToKey(date, "11"),
                        ToKey(date, "12"),
                        ToKey(date, "13"),
                        ToKey(date, "14"),
                        ToKey(date, "15"),
                        ToKey(date, "16"),
                        ToKey(date, "17"),
                        ToKey(date, "18"),
                        ToKey(date, "19"),
                        ToKey(date, "20"), };

            //Container variables for each value that the Day object possesses.
            int counter = 0;
            double rain = 0;
            int rainCount = 0;
            double snow = 0;
            int snowCount = 0;
            int tempHigh = 0;
            int tempLow = 0;

            //Adds values of each historical date into a container variable for each category.
            foreach (string d in dates)
            {
                if (data.ContainsKey(d))
                {
                    rain += data[d].Rain;
                    snow += data[d].Snow;
                    tempHigh += data[d].TempHigh;
                    tempLow += data[d].TempLow;
                    counter++;

                    if (data[d].CheckRain())
                    {
                        rainCount++;
                    }

                    if (data[d].CheckSnow())
                    {
                        snowCount++;
                    }
                }

            }

            // Pulls in average of each variable and creates new Day object from averages. Note: rain and snow variables only populate if their probability of occurrence is > 40%.
            if (rainCount / counter >= .4)
            {
                rain = rain / rainCount;
            }
            else
            {
                rain = 0;
            }

            if (snowCount / counter >= .5)
            {
                snow = snow / snowCount;
            }
            else
            {
                snow = 0;
            }
            tempHigh = tempHigh / counter;
            tempLow = tempLow / counter;

            Day Prediction = new Day
            {
                Rain = rain,
                Snow = snow,
                TempHigh = tempHigh,
                TempLow = tempLow,
            };

            Console.WriteLine();
            Console.WriteLine("Your weather prediction for " + date + ":");
            Console.WriteLine("A high of " + Prediction.TempHigh + " degrees and a low of " + Prediction.TempLow + " are predicted.");
            Console.WriteLine(GetPredictionText(Prediction, date));
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //Menu options three has the user input data to the data set by putting in month, day and year then answering questions about the weather on that day.
        //This should append the information to my .csv file and add it to the WeatherData Dictionary.
        public static void MenuOptionThree(Dictionary<string, Day> data)
        {
            Console.WriteLine("Thank you for adding more data to my data set! To start, please enter the month as 2 numbers (e.g. \"01\").");
            Console.WriteLine();
            var month = Console.ReadLine();
            if (month.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }
            Console.WriteLine("Please enter the day as 2 numbers (e.g. \"01\").");
            Console.WriteLine();
            var day = Console.ReadLine();
            if (day.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }
            Console.WriteLine("Please enter the year as 2 numbers (e.g. \"19\").");
            Console.WriteLine();
            var year = Console.ReadLine();
            if (year.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            var newKey = month + "/" + day + "/" + year;

            Console.Clear();

            if (!data.ContainsKey(newKey))
            {
                Console.WriteLine("Please enter the following data for each category.");
                Console.WriteLine("Rainfall (in inches) on " + newKey + ": ");
                var rainText = Console.ReadLine();
                if (rainText.ToUpper() == "MENU")
                {
                    Console.Clear();
                    return;
                }
                double rain = Convert.ToDouble(rainText);
                Console.WriteLine();

                Console.WriteLine("Snowfall (in inches) on " + newKey + ": ");
                var snowText = Console.ReadLine();
                if (snowText.ToUpper() == "MENU")
                {
                    Console.Clear();
                    return;
                }
                double snow = Convert.ToDouble(snowText);
                Console.WriteLine();

                Console.WriteLine("The high temperature (in degrees F) on " + newKey + ": ");
                var tempHighText = Console.ReadLine();
                if (tempHighText.ToUpper() == "MENU")
                {
                    Console.Clear();
                    return;
                }
                int tempHigh = Convert.ToInt32(tempHighText);
                Console.WriteLine();

                Console.WriteLine("The low temperature (in degress F) on " + newKey + ": ");
                var tempLowText = Console.ReadLine();
                if (tempLowText.ToUpper() == "MENU")
                {
                    Console.Clear();
                    return;
                }
                int tempLow = Convert.ToInt32(tempLowText);
                Console.WriteLine();

                Day newDay = new Day
                {
                    Rain = rain,
                    Snow = snow,
                    TempHigh = tempHigh,
                    TempLow = tempLow
                };

                data[newKey] = newDay;

                string newDayData = newKey + "," + newDay.Rain + "," + newDay.Snow + "," + newDay.TempHigh + "," + newDay.TempLow;

                using (StreamWriter sw = new StreamWriter("../../../WeatherData/WeatherData.csv", true))
                {
                    sw.WriteLine(newDayData);
                }
                Console.WriteLine();
                Console.WriteLine("This data has been written to the .csv file. Thanks!");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("We already have data for that date. To return to the main menu, please press 1. To put in a different date, please press 2.");
                Console.WriteLine();
                var response = Console.ReadLine();

                if (response == "2")
                {
                    Console.Clear();
                    MenuOptionThree(data);
                }

                else
                {
                    return;
                }
            }
        }   
    }
}
