using Newtonsoft.Json;
using RestSharp;
using System;

namespace KPE.RestSharp.OpenWeatherMap.Request
{
    /// <summary>
    ///Endpoint for any API calls
    ///api.openweathermap.org
    ///Example of API call
    ///api.openweathermap.org/data/2.5/weather? q = London, uk&APPID= 4dc010843c2aff638c1a34dc5c1f3059
    ///API documentation
    ///http://openweathermap.org/api
    ///Please, always use your API keys as &APPID= 4dc010843c2aff638c1a34dc5c1f3059 in any queries.
    ///https://openweathermap.org/appid
    ///bulk.openweathermap.org/sample/city.list.json.gz
    /// </summary>
    public class RequestBase
    {
        protected static T ExecuteRequest<T>(string resource)
        {
            RestClient restClient = new RestClient("http://api.openweathermap.org/data/2.5");
            var request = new RestRequest($"{resource}&APPID={Helper.GetAppId()}");

            var executeResponse = restClient.Execute(request);
            Helper.ThrowIfNull(executeResponse);
            Helper.ThrowIfNull(executeResponse.Content);

            var jsonObj = JsonConvert.DeserializeObject<T>(executeResponse.Content);
            Helper.ThrowIfNull(jsonObj);

            return jsonObj;
        }

    }
}
