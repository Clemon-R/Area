using Area.Wrappers;
using Area.Wrappers.Spotify.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Area.Graphs.Spotify
{
    public class SpotifyWrapper : IWrapper
    {
        public ISpotifyStateModel GetSpotifyToken(string code)
        {
            var data = new Dictionary<string, string>();
            data.Add("code", code);
            data.Add("grant_type", "authorization_code");
            data.Add("redirect_uri", "http://127.0.0.1:8081/spotify/callback");
            data.Add("client_id", "a42c9625d0534a27911d6d708531b4b9");
            data.Add("client_secret", "94618a312bf14e478c0e7c77d29312be");
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(data.AsEnumerable()))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var response = httpClient.PostAsync("https://accounts.spotify.com/api/token", content).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseContent);
                    if (responseContent.Contains("error"))
                    {
                        var json = JObject.Parse(responseContent);
                        return new SpotifyFailedModel() { Error = (string)json["error_description"] };
                    }
                    return JsonConvert.DeserializeObject<SpotifyTokenModel>(responseContent);
                }
            }
        }
    }
}
