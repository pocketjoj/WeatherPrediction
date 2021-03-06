﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WeatherPrediction
{
    public static class MenuMethods
    {
        //Menu option one prints temperature data to console and checks for rain/snow fall, printing data if there was rain/snow.
        public static void MenuOptionOne(Dictionary<string, Day> data, ref int Counter)
        {
            Console.WriteLine("Type the date for which you wanter weather info in MM/DD/YY format Data is available from 01/01/09 - 01/29/19.");
            Console.WriteLine();
            var date = Console.ReadLine();
            Console.WriteLine();

            if (date.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            if ((date.Length != 8) || (date[2] != '/') || (date[5] != '/'))
            {
                Console.WriteLine("Date formatted incorrectly. Must use MM/DD/YY format. Press any key to try again.");
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();
                MenuOptionOne(data, ref Counter);
            }

            else if (!data.ContainsKey(date))
            {
                Console.WriteLine("No data was found for that day.");
                Console.WriteLine("Please enter a date in MM/DD/YY format between 01/01/09 and 01/29/19.");
                Console.WriteLine("Press any key to try again.");
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();
                MenuOptionOne(data, ref Counter);
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("The high for " + date + " was " + data[date].TempHigh + " degrees, and the low was " + data[date].TempLow + " degrees.");
                Console.WriteLine(HelperMethods.GetHistoricalText(data[date], date));
                Console.WriteLine();

                while (true)
                {
                    Console.WriteLine("Would you like to save this data to the \"WeatherPrediction.txt\" file within the WeatherData folder? (Type \"Yes\" or \"No\").");
                    Console.WriteLine();
                    var response = Console.ReadLine();

                    if (response.ToUpper() == "YES")
                    {

                        HelperMethods.WriteData("../../../WeatherData/WeatherInfo.txt", HelperMethods.GetHistoricalText(data[date], date, ref Counter));
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                        
                    }

                    else if (response.ToUpper() == "NO")
                    {
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Please enter either \"Yes\" or \"No\". Press any key to try again.");
                        Console.WriteLine();
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                Console.Clear();
            }
        }

        //Menu option two takes a day (in MM/DD format) and returns a prediction for that day based on historical averages in the WeatherData Dictionary.
        public static void MenuOptionTwo(Dictionary<string, Day> data, ref int Counter)
        {
            Console.WriteLine("Please provide the month and day in MM/DD format to receive a weather estimate for that day.");
            Console.WriteLine();
            var date = Console.ReadLine();

            if (date.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            //This rather long if statement checks that the date is formatted correctly by checking for length of string, / mark and that day and month are not too high of values.
            if ((date.Length != 5) || (date[2] != '/') || (int.TryParse(date[0].ToString() + date[1].ToString(), out int month) && month > 13) || (int.TryParse(date[3].ToString() + date[4].ToString(), out int day) && day > 32))
            {
                Console.WriteLine("Date formatted incorrectly. Must be in MM/DD format only. Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                MenuOptionTwo(data, ref Counter);
            }

            else
            {
                //This list will contain each date (from MM/DD selected by user) in each year from 2009-2020. This will be used as keys to obtain values from WeatherData Dictionary.
                List<string> dates = new List<string>(12)
                    {   HelperMethods.ToKey(date, "09"),
                        HelperMethods.ToKey(date, "10"),
                        HelperMethods.ToKey(date, "11"),
                        HelperMethods.ToKey(date, "12"),
                        HelperMethods.ToKey(date, "13"),
                        HelperMethods.ToKey(date, "14"),
                        HelperMethods.ToKey(date, "15"),
                        HelperMethods.ToKey(date, "16"),
                        HelperMethods.ToKey(date, "17"),
                        HelperMethods.ToKey(date, "18"),
                        HelperMethods.ToKey(date, "19"),
                        HelperMethods.ToKey(date, "20"), };

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
                Console.WriteLine(HelperMethods.GetPredictionText(Prediction, date));
                Console.WriteLine();

                while (true)
                {
                    Console.WriteLine("Would you like to save this data to the \"WeatherPrediction.txt\" file within the WeatherData folder? (Type \"Yes\" or \"No\").");
                    Console.WriteLine();
                    var response = Console.ReadLine();

                    if (response.ToUpper() == "YES")
                    {
                        HelperMethods.WriteData("../../../WeatherData/WeatherInfo.txt", HelperMethods.GetPredictionText(Prediction, date, ref Counter));
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                    }

                    else if (response.ToUpper() == "NO")
                    {
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Please enter either \"Yes\" or \"No\". Press any key to try again.");
                        Console.WriteLine();
                        Console.ReadKey();
                        Console.Clear();
                    }
                    
                }
                Console.Clear();
            }
        }

        //Menu options three has the user input data to the data set by putting in month, day and year then answering questions about the weather on that day.
        //This should append the information to my .csv file and add it to the WeatherData Dictionary.
        public static void MenuOptionThree(Dictionary<string, Day> data)
        {
            Console.WriteLine("Thank you for adding more data to my data set! To start, please enter the month as 2 numbers (e.g. \"01\").");
            Console.WriteLine();
            var month = Console.ReadLine();
            int Month;
            if (month.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            if (month.Length == 2 && int.TryParse(month, out Month) && Month > 0 && Month < 13)
            {
                Console.WriteLine();
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid month: a two digit number between 01 and 12. Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                MenuOptionThree(data);

            }

            Console.WriteLine("Please enter the day as 2 numbers (e.g. \"01\").");
            Console.WriteLine();
            var day = Console.ReadLine();
            int Day;
            if (day.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            if (day.Length == 2 && int.TryParse(day, out Day) && Day > 0 && Day < 32)
            {
                Console.WriteLine();
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid day: a two digit number between 01 and 31. Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                MenuOptionThree(data);

            }

            Console.WriteLine("Please enter the year as 2 numbers (e.g. \"19\").");
            Console.WriteLine();
            var year = Console.ReadLine();
            int Year;
            if (year.ToUpper() == "MENU")
            {
                Console.Clear();
                return;
            }

            if (year.Length == 2 && int.TryParse(year, out Year) && Year >= 0 && Year < 100)
            {
                Console.WriteLine();
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid year: a two digit number between 00 and 99. Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                MenuOptionThree(data);

            }

            var newKey = month + "/" + day + "/" + year;

            Console.Clear();

            if (!data.ContainsKey(newKey))
            {
                Console.WriteLine("Please enter the following data for each category.");
                Console.WriteLine("Rainfall (in inches) on " + newKey + ": ");
                var rainText = Console.ReadLine();

                try
                {
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

                    HelperMethods.WriteData("../../../WeatherData/WeatherData.csv", newDayData);
                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey();
                    Console.Clear();
                }

                catch (System.Exception)
                {
                    Console.WriteLine("For rainfall, snowfall and temperatures, please only use numbers. Press any key to start over.");
                    Console.ReadKey();
                    Console.Clear();
                    MenuOptionThree(data);
                }
            }
            else
            {
                Console.WriteLine("We already have data for that date. Press any key to return to the main menu.");
                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }
}
