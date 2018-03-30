using KPE.RestSharp.OpenWeatherMap.Request;
using NUnit.Framework;

namespace KPE.RestSharp.OpenWeatherMap.Test
{
    [TestFixture(Category = "current-weather")]
    public class CurrentWeatherTests
    {
        [Test()]
        public void ByCityName()
        {
            var expected = Expected.CurrentWeather.ByCityName_Perth_GB();
            var response = CurrentWeatherRequest.ByCityName(expected.Name);
            Assert_Response(expected, response);
        }

        [Test()]
        public void ByCityNameCountryName()
        {
            var expected = Expected.CurrentWeather.ByCityNameCountryName_Perth_AU();
            var response = CurrentWeatherRequest.ByCityName(expected.Name, expected.Sys.Country);
            Assert_Response(expected, response);
        }

        [Test()]
        public void ByCityID()
        {
            var expected = Expected.CurrentWeather.ByCityID_Perth_AU();
            var response = CurrentWeatherRequest.ByCityID(expected.Id);
            Assert_Response(expected, response);
        }

        void Assert_Response(Expected.CurrentWeather expected, Response.CurrentWeatherResponse actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);

            Assert.IsNotNull(actual.Weather);
            Assert.AreNotEqual(0, actual.Weather.Count);

            Assert.IsNotNull(actual.Sys);
            Assert.AreEqual(expected.Sys.Country, actual.Sys.Country);

            Assert.IsNotNull(actual.Coordinates);
            Assert.AreEqual(expected.Coordinates.Longitude, actual.Coordinates.Longitude);
            Assert.AreEqual(expected.Coordinates.Latitude, actual.Coordinates.Latitude);
        }

    }
}
