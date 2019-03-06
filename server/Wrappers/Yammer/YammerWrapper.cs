using Area.Wrappers.Models;
using Area.Wrappers.Yammer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Area.Wrappers.Yammer
{
    public class YammerWrapper : IWrapper
    {
        public IRequestStateModel GenerateYammerToken(string code)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(@"https://www.yammer.com/oauth2/access_token" +
                "?client_id=MXz2pxbqLHQtK7YWsMKxMg" +
                "&client_secret=ZM90jzbbjgCeB9bN7F1QyYkpzFFY13OvjJs4U8V4dY" +
                $"&code={code}" +
                "&grant_type=authorization_code", new StringContent(string.Empty)).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (!responseContent.Contains("access_token"))
                {
                    return new RequestFailedModel() { Error = "Impossible de récupèrer votre connexion"};
                }
                var json = JObject.Parse(responseContent);
                return JsonConvert.DeserializeObject<YammerAuthenticationModel>(responseContent).Access_Token;
            }
        }
    }
}
