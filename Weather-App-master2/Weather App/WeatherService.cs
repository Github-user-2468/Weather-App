using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherService
{
    private const string OpenWeatherMapApiKey = "005bec145c16210a8131e65a05b7391c";   // API Key (Karigan's Key)

    // Get latitude and longitude from OpenWeatherMap
    public async Task<(double lat, double lon)> GetLatLonFromOpenWeatherMap(string city, string state)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city},{state},US&limit=1&appid={OpenWeatherMapApiKey}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var locations = JArray.Parse(json);

                if (locations.Count > 0)
                {
                    double lat = locations[0]["lat"].ToObject<double>();
                    double lon = locations[0]["lon"].ToObject<double>();
                    return (lat, lon);
                }
            }
        }

        return (0, 0);
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    // Function to Get Sunrise and Sunset times from OpenWeather and convert to local time
    public async Task<(DateTime sunriseTime, DateTime sunsetTime)> GetSunriseSunsetTimes(double lat, double lon)
    {
        using (HttpClient client = new HttpClient())
        {
            // Use the Current Weather Data API to fetch sunrise and sunset times
            string url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={OpenWeatherMapApiKey}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var weatherData = JObject.Parse(json);

                if (weatherData != null)
                {
                    // Extract sunrise and sunset times (in Unix timestamp format)
                    long sunriseUnix = weatherData["sys"]["sunrise"].ToObject<long>();
                    long sunsetUnix = weatherData["sys"]["sunset"].ToObject<long>();

                    // Convert Unix timestamps to DateTime (UTC)
                    DateTime sunriseUtc = DateTimeOffset.FromUnixTimeSeconds(sunriseUnix).UtcDateTime;
                    DateTime sunsetUtc = DateTimeOffset.FromUnixTimeSeconds(sunsetUnix).UtcDateTime;

                    // Convert UTC times to local time
                    DateTime sunriseLocal = sunriseUtc.ToLocalTime();
                    DateTime sunsetLocal = sunsetUtc.ToLocalTime();

                    return (sunriseLocal, sunsetLocal);
                }
            }
        }

        return (DateTime.MinValue, DateTime.MinValue); // Return default values if the request fails
    }
    

    // This gathers and reads in weather data from OpenWeather Services (STARTED -- RN IT IS JUST FOR AN ICON BUT PAROH IF YOU WANT TO ADD FOR DAILY :) )
    public async Task<JObject> GetWeatherDataFromOpenWeather(double lat, double lon)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={OpenWeatherMapApiKey}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JObject.Parse(json);
            }
        }

        return null; // Return null if the request fails
    }

    // Fetch weather data from NWS (for the forecast) and extract timeZone
    public async Task<JObject> FetchWeatherDataFromNWS(double lat, double lon)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("YourAppName/1.0 (your@email.com)");

            // Step 1: Get the points data
            string pointsUrl = $"https://api.weather.gov/points/{lat},{lon}";
            HttpResponseMessage pointsResponse = await client.GetAsync(pointsUrl);

            if (!pointsResponse.IsSuccessStatusCode)
            {
                return null; // Return null if the points request fails
            }

            string pointsJson = await pointsResponse.Content.ReadAsStringAsync();
            var pointsData = JObject.Parse(pointsJson);

            // Fetch the hourly forecast data
            string hourlyForecastUrl = pointsData["properties"]["forecastHourly"].ToString();
            HttpResponseMessage hourlyForecastResponse = await client.GetAsync(hourlyForecastUrl);

            // Fetch the regular forecast data
            string forecastUrl = pointsData["properties"]["forecast"].ToString();
            HttpResponseMessage forecastResponse = await client.GetAsync(forecastUrl);

            if (!hourlyForecastResponse.IsSuccessStatusCode || !forecastResponse.IsSuccessStatusCode)
            {
                return null; // Return null if either forecast request fails
            }

            // Parse the hourly forecast
            string hourlyForecastJson = await hourlyForecastResponse.Content.ReadAsStringAsync();
            var hourlyForecastData = JObject.Parse(hourlyForecastJson);

            // Parse the regular forecast
            string forecastJson = await forecastResponse.Content.ReadAsStringAsync();
            var forecastData = JObject.Parse(forecastJson);

            // Extract sunrise and sunset times from the forecast data--------------------------------------
            // This is needed for the background display and the output of sunrise and sunset times
            var periods = forecastData["properties"]["periods"] as JArray;
            DateTime sunriseTime = DateTime.MinValue;
            DateTime sunsetTime = DateTime.MinValue;

            if (periods != null)
            {
                foreach (var period in periods)
                {
                    if (period["name"].ToString().Contains("Sunrise"))
                    {
                        sunriseTime = DateTime.Parse(period["startTime"].ToString());
                    }
                    else if (period["name"].ToString().Contains("Sunset"))
                    {
                        sunsetTime = DateTime.Parse(period["startTime"].ToString());
                    }
                }
            }

            // Combine all data into a single JObject
            var combinedData = new JObject
            {
                ["hourlyForecast"] = hourlyForecastData,
                ["forecast"] = forecastData,
                ["sunriseTime"] = sunriseTime,
                ["sunsetTime"] = sunsetTime
            };

            return combinedData;
        }
    }

    public List<DailyForecast> ProcessDailyForecastData(JObject forecastData)
    {
        if (forecastData == null)
        {
            Console.WriteLine("Forecast data is null.");
            return new List<DailyForecast>();
        }

        var dailyForecasts = new Dictionary<string, DailyForecast>();
        var periods = forecastData["properties"]["periods"] as JArray;

        if (periods == null)
        {
            Console.WriteLine("Periods data is null.");
            return new List<DailyForecast>();
        }

        foreach (var period in periods)
        {
            // Parse the date from the startTime field
            string startTime = period["startTime"]?.ToString();
            if (string.IsNullOrEmpty(startTime))
            {
                Console.WriteLine("startTime is null or empty.");
                continue;
            }

            DateTime forecastTime = DateTime.Parse(startTime);
            string dateKey = forecastTime.ToString("yyyy-MM-dd");

            // Extract temperature, description, and icon
            double temp = period["temperature"]?.ToObject<double>() ?? 0;
            string description = period["shortForecast"]?.ToString() ?? "N/A";
            string iconUrl = period["icon"]?.ToString() ?? "N/A";

            // If we don't have this day in our dictionary yet, create a new entry
            if (!dailyForecasts.ContainsKey(dateKey))
            {
                dailyForecasts[dateKey] = new DailyForecast
                {
                    Date = forecastTime.Date,
                    DayOfWeek = forecastTime.DayOfWeek.ToString(),
                    TempMin = temp,
                    TempMax = temp,
                    Description = description,
                    Icon = iconUrl // Set the NWS icon URL
                };
            }
            else
            {
                // Update min/max temperatures
                var daily = dailyForecasts[dateKey];
                if (temp < daily.TempMin) daily.TempMin = temp;
                if (temp > daily.TempMax) daily.TempMax = temp;

                // Use the mid-day forecast (around noon) for the main description and icon
                if (forecastTime.Hour >= 11 && forecastTime.Hour <= 14)
                {
                    daily.Description = description;
                    daily.Icon = iconUrl;
                }
            }
        }

        // Convert the dictionary to a list, sort by date, and take the first 5 days
        return new List<DailyForecast>(dailyForecasts.Values)
            .OrderBy(d => d.Date)
            .Take(7)
            .ToList();
    }
    public async Task<List<DailyForecast>> GetDailyForecast(double lat, double lon)
    {
        var weatherData = await FetchWeatherDataFromNWS(lat, lon);
        if (weatherData == null)
        {
            Console.WriteLine("Failed to fetch weather data.");
            return new List<DailyForecast>();
        }

        var forecastData = weatherData["forecast"] as JObject;
        return ProcessDailyForecastData(forecastData);
    }

    //CHANGED- Paroh if you want to add to these classes for openWeather.----------------------------------------

    // Fetch weather data from OpenWeather API
    public class WeatherInfo
    {
        public List<Weather> weather { get; set; }
    }

    public class Weather
    {
        public string icon { get; set; }  // Icons for weather conditions
    }
    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public override string ToString()
        {
            return $"{Date.ToString("ddd")}: {Math.Round(TempMin)}°F - {Math.Round(TempMax)}°F";
        }
    }

    // Retrieves the nearest radar station for a location
    public async Task<string> GetRadarStation(double lat, double lon)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherApp/1.0 (your-email@example.com)");

            // Get the points data
            string pointsUrl = $"https://api.weather.gov/points/{lat},{lon}";
            HttpResponseMessage response = await client.GetAsync(pointsUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                // Extract radar station ID
                return data["properties"]["radarStation"]?.ToString();
            }
        }
        return null; // Return null if anything fails
    }
}
    
