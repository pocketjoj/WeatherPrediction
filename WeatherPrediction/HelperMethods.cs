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

        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString();

        }

        //Method to format data for a historical day that will be written to Console.
        public static string GetHistoricalText(Day day, string date)
        {
            var text = "There was " + day.CheckPrecipitation() + " on " + date + ".";
            return text;
        }

        public static string GetHistoricalText(Day day, string date, int count)
        {
            count++;
            var Text = "Entry " + count + ": Historical Data" + Environment.NewLine;
            Text += "Info obtained at " + GetTimeStamp() + "." + Environment.NewLine + Environment.NewLine;
            Text += GetHistoricalText(day, date) + Environment.NewLine + Environment.NewLine;
            return Text;
        }

        public static string GetPredictionText(Day day, string date)
        {
            var text = "There will most likely be " + day.CheckPrecipitation() + " on " + date + ".";
            return text;
        }

        public static string GetPredictionText(Day day, string date, int count)
        {
            count++;
            var Text = "Entry " + count + ": Prediction Data" + Environment.NewLine;
            Text += "Info obtained at " + GetTimeStamp() + "." + Environment.NewLine + Environment.NewLine;
            Text += GetHistoricalText(day, date) + Environment.NewLine + Environment.NewLine;
            return Text;
        }

        //This method appends data to the file.
        public static void AppendData(string fileName, string text)
        {
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(text);
            }
            Console.WriteLine();
            Console.WriteLine("This data has been written to the file at" + fileName + ". Thanks!");
            Console.WriteLine();
        }
    }
}
