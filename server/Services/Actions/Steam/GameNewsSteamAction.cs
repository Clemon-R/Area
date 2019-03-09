using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;
using SteamWebAPI2;
using SteamWebAPI2.Models;

namespace Area.Services.Actions.Steam
{
    public class GameNewsSteamAction : IAction
    {
        TriggerCompatibilityEnum IAction.Type => throw new NotImplementedException();

        public ActionTypeEnum Id => throw new NotImplementedException();

        public void CheckAction(Account user)
        {
            var api = new SteamWebAPI2.Interfaces.PlayerService("devkey");
            var newsApi = new SteamWebAPI2.Interfaces.SteamNews("devkey");

            var ownedGamesResponse = api.GetOwnedGamesAsync(0).GetAwaiter().GetResult();
            var ownedGames = ownedGamesResponse.Data.OwnedGames;

            for (int i = 0; i < ownedGames.Count; i++)
            {
                var gameNewsResponse = newsApi.GetNewsForAppAsync(ownedGames.ElementAt(i).AppId).GetAwaiter().GetResult();
                var gameNews = gameNewsResponse.Data.NewsItems;

                for (int j = 0; j < gameNews.Count; j++)
                {
                    if (FromUnixTime(gameNews.ElementAt(j).Date) > user.LastVerificationDate)
                    {
                        //gameNews.ElementAt(j).
                    }
                }
            }
        }

        public object GetResult()
        {
            throw new NotImplementedException();
        }

        public bool IsTriggered()
        {
            throw new NotImplementedException();
        }

        public DateTime FromUnixTime(ulong unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }
}
