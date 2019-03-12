using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Helpers;
using Area.Models;
using Steam.Models;
using SteamWebAPI2;
using SteamWebAPI2.Models;

namespace Area.Services.Actions.Steam
{
    public class GameNewsSteamAction : IAction
    {
        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.FollowedArtistNewReleaseSpotify;
        List<NewsItemModel> _news = new List<NewsItemModel>();

        public GameNewsSteamAction()
        {
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        public void CheckAction(Account user, DateTime lastCheck)
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
                    if (FromUnixTime(gameNews.ElementAt(j).Date) > lastCheck)
                    {
                        _news.Add(gameNews.ElementAt(j));
                    }
                }
            }
        }

        public object GetResult()
        {
            return _news;
        }

        public bool IsTriggered()
        {
            return _news.Count > 0;
        }

        public DateTime FromUnixTime(ulong unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        private DateTime _lastTriggerDate;

        public DateTime GetDate()
        {
            return _lastTriggerDate;
        }
    }
}
