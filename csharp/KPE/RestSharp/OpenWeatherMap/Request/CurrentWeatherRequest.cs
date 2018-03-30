using KPE.RestSharp.OpenWeatherMap.Response;

namespace KPE.RestSharp.OpenWeatherMap.Request
{
    /// <summary>
    /// https://openweathermap.org/current
    /// </summary>
    public class CurrentWeatherRequest : RequestBase
    {
        /// <summary>
        /// Returns the specific city based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CurrentWeatherResponse ByCityID(int id)
        {
            return ExecuteRequest<CurrentWeatherResponse>($"weather?id={id}");
        }

        /// <summary>
        /// Returns the first match based on the city name
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static CurrentWeatherResponse ByCityName(string cityName)
        {
            return ExecuteRequest<CurrentWeatherResponse>($"weather?q={cityName}");
        }

        /// <summary>
        /// Returns specific to the country and city name
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static CurrentWeatherResponse ByCityName(string cityName, string countryCode)
        {
            return ExecuteRequest<CurrentWeatherResponse>($"weather?q={cityName},{countryCode}");
        }
    }
}
