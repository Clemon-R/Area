using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api;
namespace Area.Services.APIs
{
    public class TwitchService
    {
        static string clientId = "g2kkfu5px956qtxvzfvsi9jbqhip4n";

        public static async void lol()
        {
            TwitchAPI api = new TwitchAPI();
            //api.Settings.ClientId = "?";
            //api.Settings.AccessToken = "?";

            var lol = await api.V5.Users.GetUserFollowsAsync("?");
        }
    }
}
