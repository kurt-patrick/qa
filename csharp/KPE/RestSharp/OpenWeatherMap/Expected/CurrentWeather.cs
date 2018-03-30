using KPE.RestSharp.OpenWeatherMap.Response;

namespace KPE.RestSharp.OpenWeatherMap.Expected
{
    public class CurrentWeather
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sys Sys { get; set; } = new Sys();
        public Coordinates Coordinates { get; set; } = new Coordinates();

        public static CurrentWeather ByCityID_Perth_AU() => Perth_AU(115.83, -31.93);
        public static CurrentWeather ByCityNameCountryName_Perth_AU() => Perth_AU(115.86, -31.95);

        public static CurrentWeather Perth_AU(double lon, double lat)
        {
            const int Perth_AU_CityID = 2063523;

            var retVal = new CurrentWeather() { Id = Perth_AU_CityID, Name = "Perth" };
            retVal.Sys.Country = "AU";
            retVal.Coordinates.Longitude = lon;
            retVal.Coordinates.Latitude = lat;

            return retVal;
        }

        public static CurrentWeather ByCityName_Perth_GB()
        {
            const int Perth_GB_CityID = 2640358;

            var retVal = new CurrentWeather() { Id = Perth_GB_CityID, Name = "Perth" };
            retVal.Sys.Country = "GB";
            retVal.Coordinates.Longitude = -3.43;
            retVal.Coordinates.Latitude = 56.4;

            return retVal;
        }

    }
}
