Thank you for your interest in my Weather Prediction application. 

In order to run this program, you will need to download the repository and open the solution file in Visual Studio. From there, you should simply be able to hit Start to compile and run the code. 

This application uses a .csv file of 10 years of historical weather data from the Louisville area (furnished by the National Centers for Environmental Information). It is a fairly simple app that allows user to search the database for weather from a specific day, to get a prediction for weather on a specific day (using month and date) based on that day's historical averages for weather, and to add a date to the database for inclusion in the data moving forward. The first two options allow users to save their historical information or prediction to a WeatherData.txt file that will write to the WeatherData folder. The third option will format and write the data provided by the user to the WeatherData.csv file in the WeatherData.csv folder and will also update the Dictionary in use so that the provided day will be immediately searchable. 

Some known limitations: 

1. The predictions for precipitation are off simply because it is very rare for a singular date (i.e. February 1st) to have had precipitation over 50% of the time in a 10 year period. This is more for fun than for meteorological accuracy, but it does appear that very few (if any) days predict rain or snow.

2. There are a few instances of code that I would have liked to make into a method to be more DRY, but in at least one instance it did not appear to work. I needed to use "return" in a module of code in order to return a user to the main menu of the application, but using that in a method did not produce the correct result. Where possible, I did try to condense things to methods, but I'm sure there are a few instances of repetitive code. 

3. Some of the validations may still have a couple holes. I tried to be as exhaustive as possible to ensure invalid dates would loop properly, returning an error and prompting the user for an accurately formatted response. However, there may be some loopholes.

I would love any feedback you have to give on this! Thank you for taking to time to look at it!

Joel
