using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Area.Wrappers;
using Area.Wrappers.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using server.Wrappers.Reddit.Models;

namespace server.Wrappers.Reddit
{
    public class RedditWrapper : IWrapper
    {
        public IRequestStateModel GenerateRedditToken(string code)
        {
            var data = new Dictionary<string, string>();
            data.Add("code", code);
            data.Add("grant_type", "authorization_code");
            data.Add("redirect_uri", "http://127.0.0.1:8081/reddit/callback");

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(data.AsEnumerable()))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var response = httpClient.PostAsync(@"https://hfTIumnrQMLA_A:9wp2Hj1WzmAMSmS1srmgi99UdYM@www.reddit.com/api/v1/access_token", content).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseContent);
                    if (responseContent.Contains("error"))
                    {
                        var json = JObject.Parse(responseContent);
                        return new RequestFailedModel() { Error = (string)json["error_description"] };
                    }
                    return JsonConvert.DeserializeObject<RedditTokenModel>(responseContent);
                }
            }
        }
    }
}