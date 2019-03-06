﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;
using Area.Services.App;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Client;

namespace Area.Services.Actions.Twitch
{
    public class NewSubTwitchAction : IAction
    {
        List<Follow> _newFollowers = new List<Follow>();

        TwitchService _twitchService;
        public TriggerTypeEnum Type => TriggerTypeEnum.ListTwitchFollowers;

        public NewSubTwitchAction(TwitchService twitchService)
        {
            _twitchService = twitchService;
        }

        public void CheckAction(Account user)
        {
            TwitchAPI api = _twitchService.GetApi(null);

            GetUsersFollowsResponse userFollows = api.Helix.Users.GetUsersFollowsAsync("user_id").GetAwaiter().GetResult();

            for (int i = 0; i < userFollows.Follows.Length; i++)
            {
                if (userFollows.Follows[i].FollowedAt > user.LastVerificationDate)
                {
                    _newFollowers.Add(userFollows.Follows[i]);
                }
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