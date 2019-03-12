using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Helpers;
using Area.Models;
using Area.Services.App;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users;

namespace Area.Services.Actions.Twitch
{
    public class NewSubTwitchAction : IAction
    {
        private DateTime _lastTriggerDate;
        List<User> _newFollowers = new List<User>();

        TwitchService _twitchService;

        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.NewSubTwitch; 

        public NewSubTwitchAction(IServiceProvider serviceProvider)
        {
            _twitchService = (TwitchService)serviceProvider.GetService(typeof(TwitchService));
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        public void CheckAction(Account user, DateTime lastCheck)
        {
            Console.WriteLine("Checking NewSubTwitchAction");
            TwitchAPI api = _twitchService.GetApi(user);
            var currentUser = api.Helix.Users.GetUsersAsync().Result;

            if (currentUser.Users.Length <= 0)
            {
                Console.WriteLine("Current user id not found");
                return;
            }
            GetUsersFollowsResponse userFollows = api.Helix.Users.GetUsersFollowsAsync("", "", 20, "", currentUser.Users[0].Id).Result;
            List<string> followerIds = new List<string>();

            if (userFollows.Follows.Length <= 0)
                Console.WriteLine("No followers for user " + currentUser.Users[0].DisplayName);
            for (int i = 0; i < userFollows.Follows.Length; i++)
            {
                Console.WriteLine("Follower : " + userFollows.Follows[i].ToUserId);
                if (userFollows.Follows[i].FollowedAt > lastCheck)
                {
                    if (_lastTriggerDate == null)
                        _lastTriggerDate = userFollows.Follows[i].FollowedAt;
                    else if (_lastTriggerDate < userFollows.Follows[i].FollowedAt)
                        _lastTriggerDate = userFollows.Follows[i].FollowedAt;
                    Console.WriteLine("New !");
                    followerIds.Add(userFollows.Follows[i].FromUserId);
                }
                else
                    Console.WriteLine("Not new");
            }
            if (followerIds.Count > 0)
            {
                _newFollowers = api.Helix.Users.GetUsersAsync(followerIds).Result.Users.ToList();
            }
        }

        public object GetResult()
        {
            return _newFollowers;
        }

        public bool IsTriggered()
        {
            return _newFollowers.Count > 0;
        }


        public DateTime GetDate()
        {
            return _lastTriggerDate;
        }
    }
}