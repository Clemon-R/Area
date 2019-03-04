using Area.Wrappers.Models;
using Area.Wrappers.Twitch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Area.Wrappers.Twitch
{
    public class TwitchWrapper : IWrapper
    {
        public IRequestStateModel GenerateTwitchToken(string code, string url = "http://127.0.0.1:8081/twitch/callback")
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(@"https://id.twitch.tv/oauth2/token" +
                "?client_id=g2kkfu5px956qtxvzfvsi9jbqhip4n" +
                "&client_secret=a2w11c1wu4q8mb5wqjzf4q5njq41co" +
                $"&code={code}" +
                "&grant_type=authorization_code" +
                $"&redirect_uri={url}", new StringContent(string.Empty)).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseContent);
                if (responseContent.Contains("status"))
                {
                    var json = JObject.Parse(responseContent);
                    return new RequestFailedModel() { Error = (string)json["message"] };
                }
                return JsonConvert.DeserializeObject<TwitchTokenModel>(responseContent);
            }
        }
    }
}
