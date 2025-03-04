using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http; // Add this for HttpClient
using System.Threading.Tasks; // Add this for Task
using Newtonsoft.Json.Linq; // Add this for JObject and JArray
using System.IO;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using static WeatherService;
using System.Collections.Generic;

namespace Weather_App
{
    public partial class Form1 : Form
    {

       
      

        private Color initialColor = ColorTranslator.FromHtml("#173253");
        private Color intialEndColor = ColorTranslator.FromHtml("#6889a8");
        private string currentTemperatureUnit = "Fahrenheit";


        public Form1()
        {
            InitializeComponent();
            PopulateStatesComboBox();
            this.BackColor = Color.White;


            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;


        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a gradient brush
            Rectangle gradientRect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(
                gradientRect,

                initialColor, // Start color
                intialEndColor, // End color
                LinearGradientMode.Vertical)) // Gradient direction
            {
                ColorBox.BackColor = Color.FromArgb(180, ColorTranslator.FromHtml("#1a1d3c"));
                Citylabel.BackColor = Color.FromArgb(180, ColorTranslator.FromHtml("#1a1d3c"));
                Citylabel.ForeColor = Color.White;
                Statelabel.BackColor = Color.FromArgb(30, ColorTranslator.FromHtml("#1a1d3c"));
                Statelabel.ForeColor = Color.White;
                // Fill the form with the gradient
                e.Graphics.FillRectangle(brush, gradientRect);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void PopulateStatesComboBox()
        {
            string[] states = {
                "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware",
                "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky",
                "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi",
                "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
                "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania",
                "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont",
                "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"
            };

            StateComboBox.Items.AddRange(states);
            StateComboBox.SelectedIndex = 0;
        }


        private async void Searchbutton_Click(object sender, EventArgs e)
        {
            string city = CityTextBox.Text;
            string state = StateComboBox.SelectedItem.ToString();

            WeatherService weatherService = new WeatherService();

            // Fetch latitude and longitude
            var (lat, lon) = await weatherService.GetLatLonFromOpenWeatherMap(city, state);

            //Gets icon beside temperture label for display
            var weatherData = await weatherService.GetWeatherDataFromOpenWeather(lat, lon); // Just for icon FOR NOW
           

            if (lat != 0 && lon != 0)
            {
               
                //Icon for beside temp
                var Info = weatherData.ToObject<WeatherInfo>();
                WeatherIcon.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png"; // Displays the icon

                // Fetch sunrise and sunset times
                var (sunriseTime, sunsetTime) = await weatherService.GetSunriseSunsetTimes(lat, lon);

                // Fetch weather data
                var combinedForecastData = await weatherService.FetchWeatherDataFromNWS(lat, lon);

                if (combinedForecastData != null)
                {
                    // Access the hourly forecast (ensure it's a JObject)
                    var hourlyForecast = combinedForecastData["hourlyForecast"] as JObject;
                    if (hourlyForecast != null)
                    {
                        DisplayWeatherData(hourlyForecast); // Display the hourly forecast
                    }

                    // Access the regular forecast (ensure it's a JObject)
                    var forecast = combinedForecastData["forecast"] as JObject;
                    if (forecast != null)
                    {
                        DisplayForecast(forecast);
                        // Process and display the daily forecast
                        var dailyForecasts = weatherService.ProcessDailyForecastData(forecast);
                        DisplayDailyForecast(dailyForecasts);

                    }

                    // Update the background image based on the current time
                    UpdateBackground(sunriseTime, sunsetTime);

                    // Display sunrise and sunset times in the UI
                    DisplaySunriseSunsetTimes(sunriseTime, sunsetTime);

                }
                else
                {
                    MessageBox.Show("Failed to fetch weather data.");
                }
            }
            else
            {
                MessageBox.Show("City not found.");
            }
        }
        private async void DisplayForecast(JObject forecast)
        {
            var periods = forecast["properties"]["periods"] as JArray;
            if (periods == null || periods.Count == 0)
            {
                MessageBox.Show("No forecast data available.");
                return;
            }

            // Get the first period
            var period = periods[0];

            // Extract detailed forecast
            string detailForecast = period["detailedForecast"].ToString();

            // Extract precipitation probability
            string precipitation = "0"; 
            var probabilityOfPrecipitation = period["probabilityOfPrecipitation"];
            if (probabilityOfPrecipitation != null)
            {
                var precipitationValue = probabilityOfPrecipitation["value"];
                if (precipitationValue != null && precipitationValue.Type != JTokenType.Null)
                {
                    precipitation = precipitationValue.ToString();
                }
            }

            // Update UI labels
            pertilabel.Text = "Precipitation: " + precipitation + "%";
            detailForecastlbl.Text = detailForecast;
        }
        private async void DisplayWeatherData(JObject hourlyForecast)
        {
           // listBox1.Items.Clear();

            var periods = hourlyForecast["properties"]["periods"] as JArray;

            if (periods != null && periods.Count > 0)
            {
                // Clear any existing controls in the flowLayoutPanel1
                flowLayoutPanel1.Controls.Clear();

                
                // Display data for the next 24 periods (or all available periods if fewer than 24)
                int maxPeriods = Math.Min(24, periods.Count); // Ensure we don't exceed 24 periods
                for (int i = 0; i < maxPeriods; i++)
                {
                    
                    // Create new labels and PictureBox for each period
                    Label timeLabel = new Label();
                    Label tempLabel = new Label();
                    PictureBox pictureBox = new PictureBox();

                    // Display data for the current period
                    await DisplayPeriodData(periods[i], timeLabel, tempLabel, pictureBox, i);

                    // Add the current period's data to the flowLayoutPanel1
                    AddPeriodToFlowLayoutPanel(periods[i], pictureBox.Image, timeLabel.Text, tempLabel.Text, i);
                }
            }
            else
            {
                MessageBox.Show("No weather data available.");
            }
        }
        // Helper method to add a period's data to the flowLayoutPanel1
        private void AddPeriodToFlowLayoutPanel(JToken period, Image icon, string time, string temperature, int periodIndex)
        {
            // Create a new Panel to hold the controls for this period
            Panel periodPanel = new Panel();
            periodPanel.Size = new Size(100, 150);


            Label timeLabel = new Label();
            timeLabel.TextAlign = ContentAlignment.MiddleCenter; // Center-align the text
            timeLabel.AutoSize = false; // Disable auto-sizing
            timeLabel.Size = new Size(80, 20); // Set a fixed size
            timeLabel.Location = new Point((periodPanel.Width - timeLabel.Width) / 2, 10); // Center horizontally
            timeLabel.Text = time; // Set the time text
            timeLabel.BackColor = Color.Transparent; // Set background to transparent
            timeLabel.ForeColor = Color.White; // Set text color (adjust as needed)

            // Create and configure the PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(60, 60); // Adjust size as needed
            pictureBox.Location = new Point((periodPanel.Width - pictureBox.Width) / 2, timeLabel.Bottom + 5); // Center horizontally and position below the time label
            pictureBox.Image = icon; // Set the image from the period
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Stretch the image to fit

            // Create and configure the Temperature Label (below the PictureBox)
            Label tempLabel = new Label();
            tempLabel.TextAlign = ContentAlignment.MiddleCenter; // Center-align the text
            tempLabel.AutoSize = false; // Disable auto-sizing
            tempLabel.Size = new Size(80, 20); // Set a fixed size
            tempLabel.Location = new Point((periodPanel.Width - tempLabel.Width) / 2, pictureBox.Bottom + 5); // Center horizontally and position below the PictureBox
            tempLabel.Text = temperature; // Set the temperature text
            tempLabel.BackColor = Color.Transparent; // Set background to transparent
            tempLabel.ForeColor = Color.White; // Set text color (adjust as needed)

            // Add the controls to the periodPanel
            periodPanel.Controls.Add(timeLabel);
            periodPanel.Controls.Add(pictureBox);
            periodPanel.Controls.Add(tempLabel);

            // Add the periodPanel to the flowLayoutPanel1
            flowLayoutPanel1.Controls.Add(periodPanel);
        }
        private async Task DisplayPeriodData(JToken period, Label timeLabel, Label tempLabel, PictureBox pictureBox, int periodIndex)
        {
            string iconUrl = period["icon"].ToString();
            await LoadImageAsync(iconUrl, pictureBox); // Pass the specific PictureBox

            string temperature = period["temperature"].ToString();
            string shortForecast = period["shortForecast"].ToString();
            string windSpeed = period["windSpeed"].ToString();
            string humidity = period["relativeHumidity"]["value"].ToString();
       

            // Parse the start time with the time zone (DateTimeOffset will handle the offset)
            DateTimeOffset periodStartTime = DateTimeOffset.Parse(period["startTime"].ToString());


            // Special handling for period 0
            if (periodIndex == 0)
                {
                    
                    timeLabel.Text = "Now"; // Display "Now" for period 0
                    tempLabel.Text = temperature + "\u00B0"; // Display temperature
                    ShortForcastLabel.Text = shortForecast; // Display short forecast outside of the panel
                    templabel1.Text = temperature + "\u00B0"; // Display temperature outside of the panel
                    windlabel1.Text = "windSpeed: " + windSpeed;
                    humlabel.Text = "Humidity: " + humidity + "%";
                }
                else
                {
                    timeLabel.Text = periodStartTime.ToString("h:mm tt"); // Format the time
                    tempLabel.Text = temperature + "\u00B0"; // Display temperature
                }
            }
        

        private async Task LoadImageAsync(string url, PictureBox pictureBox)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Add a User-Agent header to the request
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("YourAppName/1.0 (your-email@example.com)");

                    // Fetch the image data from the URL
                    byte[] imageData = await client.GetByteArrayAsync(url);

                    // Convert the byte array to an Image object
                    using (var ms = new System.IO.MemoryStream(imageData))
                    {
                        var image = Image.FromStream(ms);

                        // Assign the image to the specific PictureBox
                        pictureBox.Image = image;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load image: " + ex.Message);
            }
        }



        // This function updates the background color based on the current time
        private void UpdateBackground(DateTime sunriseTime, DateTime sunsetTime)
        {
            try
            {
                DateTime now = DateTime.Now;

                if (now >= sunriseTime && now < sunsetTime)
                {
                    // Daytime: Set the gradient colors for daytime 
                    initialColor = ColorTranslator.FromHtml("#4184f0");
                    intialEndColor = ColorTranslator.FromHtml("#9ecbff");

                    // Changes the fonts 
                    groupBox1.ForeColor = Color.White;
                    templabel1.ForeColor = Color.White;
                    ShortForcastLabel.ForeColor = Color.White;
                    detailForecastlbl.ForeColor = Color.White;
                    // Search bar label adjustments
                    flowLayoutPanel1.BackColor = Color.FromArgb(30, ColorTranslator.FromHtml("#41718c"));


                }
                else
                {
                    // Nighttime: Set the gradient colors for nighttime
                    initialColor = ColorTranslator.FromHtml("#14213a");
                    intialEndColor = ColorTranslator.FromHtml("#4c5c6b");
                    // Changes the fonts 
                    groupBox1.ForeColor = Color.White;
                    templabel1.ForeColor = Color.White;
                    ShortForcastLabel.ForeColor = Color.White;
                    detailForecastlbl.ForeColor = Color.White;
                    // Search bar label adjustments
                    flowLayoutPanel1.BackColor = Color.FromArgb(60, Color.Black);

                }

                // Force the form to redraw the background
                this.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating background: " + ex.Message);
            }
        }


        //This function displays the sunrise and sunset times in the UI
        private void DisplaySunriseSunsetTimes(DateTime sunriseTime, DateTime sunsetTime)
        {
            // Display the sunrise and sunset times in the UI with a 12-hour clock format
            SunriseLabel.Text = "Sunrise: " + sunriseTime.ToString("h:mm tt");
            SunsetLabel.Text = "Sunset: " + sunsetTime.ToString("h:mm tt");

        }
        


        //-----------------Temperature Conversion-----------------//

        // Converts Fahrenheit to Celcuis 
        private double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        // Converts Celsius to Fahrenheit

        private double CelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }
        
        
        private void Celsiusbtn_Click(object sender, EventArgs e)
        {
            // Only update if the current unit is not Celsius
            if (currentTemperatureUnit != "Celsius")
            {
                UpdateTemperatures(toCelsius: true);
                currentTemperatureUnit = "Celsius"; // Update the current unit
            }
        }

        private void Fahrenheitbtn_Click(object sender, EventArgs e)
        {
            // Only update if the current unit is not Fahrenheit
            if (currentTemperatureUnit != "Fahrenheit")
            {
                UpdateTemperatures(toCelsius: false);
                currentTemperatureUnit = "Fahrenheit"; // Update the current unit
            }
            
        }

        // Updates the current Weather Displays acoording to the button that clicked it 
        private void UpdateTemperatures(bool toCelsius)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Panel periodPanel)
                {
                    foreach (Control panelControl in periodPanel.Controls)
                    {
                        if (panelControl is Label tempLabel)
                        {
                            string text = tempLabel.Text.Replace("°", "").Trim();
                            if (double.TryParse(text, out double tempValue))
                            {
                                // Convert the temperature
                                double convertedTemp = toCelsius ? FahrenheitToCelsius(tempValue) : CelsiusToFahrenheit(tempValue);
                                tempLabel.Text = $"{Math.Round(convertedTemp, 1)}°";
                            }
                        }
                    }
                }
            }
            // Update daily forecast temperatures
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control is Panel dayPanel)
                {
                    foreach (Control panelControl in dayPanel.Controls)
                    {
                        if (panelControl is Label tempLabel && tempLabel.Tag is DailyForecast forecast)
                        {
                            double minTemp = toCelsius ? FahrenheitToCelsius(forecast.TempMin) : forecast.TempMin;
                            double maxTemp = toCelsius ? FahrenheitToCelsius(forecast.TempMax) : forecast.TempMax;
                            tempLabel.Text = $"H:{Math.Round(maxTemp)}° L:{Math.Round(minTemp)}°";
                        }
                    }
                }
            }


            // Update the main temperature display
            string mainTempText = templabel1.Text.Replace("°", "").Trim();
            if (double.TryParse(mainTempText, out double mainTempValue))
            {
                double convertedMainTemp = toCelsius ? FahrenheitToCelsius(mainTempValue) : CelsiusToFahrenheit(mainTempValue);
                templabel1.Text = $"{Math.Round(convertedMainTemp, 1)}°";
            }
        }

        //----------------------------------DAILY FORECAST---------------------------------------------//
        private async void DisplayDailyForecast(List<DailyForecast> dailyForecasts)
        {


            foreach (var forecast in dailyForecasts)
            {

                // Create a panel for each day's forecast
                Panel dayPanel = new Panel
                {
                    Size = new Size(120, 150),
                    BorderStyle = BorderStyle.None,
                    BackColor = Color.FromArgb(100, 255, 255, 255),
                    Padding = new Padding(5),
                    Margin = new Padding(5)
                };
                // Add gradient background
                dayPanel.Paint += (sender, e) =>
                {
                    using (var brush = new LinearGradientBrush(dayPanel.ClientRectangle, Color.FromArgb(100, 173, 216, 230), Color.FromArgb(100, 135, 206, 250), LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(brush, dayPanel.ClientRectangle);
                    }
                };

                // Add day of the week label
                Label dayLabel = new Label
                {
                    Text = forecast.DayOfWeek,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = false,
                    Size = new Size(110, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };

                // Add weather icon
                PictureBox iconBox = new PictureBox
                {
                    Size = new Size(50, 50),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };

                // Load the NWS icon
                await LoadImageAsync(forecast.Icon, iconBox);

                // Add temperature range label
                Label tempLabel = new Label
                {
                    Text = $"H:{Math.Round(forecast.TempMax)}° L: {Math.Round(forecast.TempMin)}°",
                    Font = new Font("Arial", 9),
                    ForeColor = Color.White,
                    AutoSize = false,
                    Size = new Size(110, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent,
                    Tag = forecast //store forecast data for temp conversion
                };

                // Center elements dynamically
                dayLabel.Location = new Point((dayPanel.Width - dayLabel.Width) / 2, 10);
                iconBox.Location = new Point((dayPanel.Width - iconBox.Width) / 2, 40);
                tempLabel.Location = new Point((dayPanel.Width - tempLabel.Width) / 2, 100);

                // Add controls to the day panel
                dayPanel.Controls.Add(dayLabel);
                dayPanel.Controls.Add(iconBox);
                dayPanel.Controls.Add(tempLabel);

                // Add the day panel to the daily forecast panel
                flowLayoutPanel2.Controls.Add(dayPanel);
            }
            // Adjust flowLayoutPanel2 width to fit all panels
            int totalPanelWidth = (120 + 10) * dailyForecasts.Count; // Panel width + margin
            int containerWidth = flowLayoutPanel2.Parent.ClientSize.Width;

            flowLayoutPanel2.Width = totalPanelWidth;
            flowLayoutPanel2.Left = (containerWidth - totalPanelWidth) / 2;
        }
    }
}