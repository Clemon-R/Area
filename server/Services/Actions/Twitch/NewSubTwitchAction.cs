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
        List<User> _newFollowers = new List<User>();

        TwitchService _twitchService;

        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.NewSubTwitch; 

        public NewSubTwitchAction(IServiceProvider serviceProvider)
        {
            _twitchService = (TwitchService)serviceProvider.GetService(typeof(TwitchService));
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        public void CheckAction(Account user)
        {
            TwitchAPI api = _twitchService.GetApi(user);

            GetUsersFollowsResponse userFollows = api.Helix.Users.GetUsersFollowsAsync("", "", 20, "", "user_id").Result;
            List<string> followerIds = new List<string>();
            for (int i = 0; i < userFollows.Follows.Length; i++)
            {
                if (userFollows.Follows[i].FollowedAt > user.LastVerificationDate)
                {
                    followerIds.Add(userFollows.Follows[i].FromUserId);
                }
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
    }
}