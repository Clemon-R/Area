using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Area.Models;
using Area.Wrappers.Models;
using Area.Wrappers.Youtube.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Area.Wrappers.Youtube
{
    public class YoutubeWrapper : IWrapper
    {
        public IRequestStateModel GenerateYoutubeToken(string code)
        {
            var data = new Dictionary<string, string>();
            data.Add("code", code);
            data.Add("grant_type", "authorization_code");
            data.Add("redirect_uri", "http://127.0.0.1:8081/youtube/callback");
            data.Add("client_id", "473291186491-5tiqjh1gb7uoiv717utbf8ibjgcvfbj0.apps.googleusercontent.com");
            data.Add("client_secret", "yEQlGF5mlNqW_zCrm4fkT2kB");

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(data.AsEnumerable()))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var response = httpClient.PostAsync("https://www.googleapis.com/oauth2/v4/token", content).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseContent);
                    if (responseContent.Contains("error"))
                    {
                        var json = JObject.Parse(responseContent);
                        return new RequestFailedModel() { Error = (string)json["error_description"] };
                    }
                    return JsonConvert.DeserializeObject<YoutubeTokenModel>(responseContent);
                }
            }
        }

        public IRequestStateModel RefreshYoutubeToken(Token code)
        {
            var data = new Dictionary<string, string>();
            data.Add("grant_type", "refresh_token");
            data.Add("refresh_token", code.RefreshToken);
            data.Add("client_id", "473291186491-5tiqjh1gb7uoiv717utbf8ibjgcvfbj0.apps.googleusercontent.com");
            data.Add("client_secret", "yEQlGF5mlNqW_zCrm4fkT2kB");

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(data.AsEnumerable()))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var response = httpClient.PostAsync("https://www.googleapis.com/oauth2/v4/token", content).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseContent);
                    if (responseContent.Contains("error"))
                    {
                        var json = JObject.Parse(responseContent);
                        return new RequestFailedModel() { Error = (string)json["error_description"] };
                    }
                    return JsonConvert.DeserializeObject<YoutubeRefreshTokenModel>(responseContent);
                }
            }
        }
    }
}