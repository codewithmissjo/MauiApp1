using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    namespace WeatherApp.Models
    {
        class WeatherInfo
        {
            public string name { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public float temp_min { get; set; }
            public float temp_max { get; set; }
        }
        class Main
        {
            public float temp { get; set; }
            public float humidity { get; set; }

            public float pressure { get; set; }
        }
        class Wind
        {
            public float speed { get; set; }
            public int deg { get; set; }
            public float gust { get; set; }
        }
        class Clouds
        {
            public int all { get; set; }
        }

        class ForecastInfo
        {
            public List[] list { get; set; }
        }
        class List
        {
            public Main main { get; set; }
            public string dt { get; set; }
            public string dt_txt { get; set; }
        }
    }

}
