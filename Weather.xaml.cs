using MauiApp1.Helper;
using MauiApp1.Models.WeatherApp.Models;
using Newtonsoft.Json;

namespace MauiApp1;

public partial class Weather : ContentPage
{
	public Weather()
	{
		InitializeComponent();
		entry.Text = "Austin, Texas";
        cityTxt.Text = entry.Text;
        DateTime today = DateTime.Now;
        dateTxt.Text = $"{today.ToString("MMMM")} {today.Day}, {today.Year}";
        GetWeatherInfo();
	}

    string key = "4924a627c6289e89d83860c3475ce8c2";

    private void CityButton_Clicked(object sender, EventArgs args)
    {
        string city = entry.Text;
        cityTxt.Text = city.ToString();
        GetWeatherInfo();
    }

    private async void GetWeatherInfo()
    {
        string placeName = entry.Text;
        var url1 = $"https://api.openweathermap.org/data/2.5/weather?q={placeName}&appid={key}&units=imperial";

        var result = await ApiCaller.Get(url1);
        if (result.Successful)
        {
            try
            {
                //Details.Text = result.Response;
                var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                temperatureTxt.Text = $" {weatherInfo.main.temp.ToString("0")}°";
                humidityTxt.Text = $"{weatherInfo.main.humidity.ToString("0")}%";
                windTxt.Text = $"{weatherInfo.wind.speed.ToString()} m/s";
                pressureTxt.Text = $"{weatherInfo.main.pressure.ToString()} hpa";
                cloudinessTxt.Text = $"{weatherInfo.clouds.all.ToString()}%";

                GetWeatherForecast();
            }
            catch (Exception ex)
            {
                Details.IsVisible = true;
                Details.Text = ex.ToString();
            }
        }
        else
        {
            Details.IsVisible = true;
            Details.Text = result.ErrorMessage;
        }
    }

    private async void GetWeatherForecast()
    {
        string placeName = entry.Text;
        var url2 = $"https://api.openweathermap.org/data/2.5/forecast?q={placeName}&appid={key}&units=imperial";

        var result2 = await ApiCaller.Get(url2);
        if (result2.Successful)
        {
            try
            {
                //Details.Text = result2.Response;
                var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result2.Response);

                List<List> allList = new List<List>();

                foreach (var list in forcastInfo.list)
                {
                    var date = DateTime.Parse(list.dt_txt);

                    // date.hour == 21 b'se the forecast updates every 3 hours, so we want to check the last hour check
                    if (date > DateTime.Now && date.Hour == 21 && date.Minute == 0 && date.Second == 0)
                        allList.Add(list);
                }

                //Day1.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd");
                Day1.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                Day1temp.Text = $"{allList[0].main.temp.ToString("0")}° F";

                //Day2.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                Day2.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                Day2temp.Text = $"{allList[1].main.temp.ToString("0")}° F";

                //Day3.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                Day3.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                Day3temp.Text = $"{allList[2].main.temp.ToString("0")}° F";

                //Day4.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                Day4.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                Day4temp.Text = $"{allList[3].main.temp.ToString("0")}° F";
            }
            catch (Exception ex)
            {
                Details.IsVisible = true;
                Details.Text = ex.ToString();
            }
        }
        else
        {
            Details.IsVisible = true;
            Details.Text = result2.ErrorMessage;
        }
    }
}