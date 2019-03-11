using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Helpers;
using Area.Models;
using Area.Services.App;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Clips.GetClip;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Client;

namespace Area.Services.Actions.Twitch
{
    public class NewSmashClipTwitchAction : IAction
    {
        List<Clip> _newClips = new List<Clip>();

        TwitchService _twitchService;

        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.NewSmashClipTwitch; 

        public NewSmashClipTwitchAction(TwitchService twitchService)
        {
            _twitchService = twitchService;
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        public void CheckAction(Account user)
        {
            TwitchAPI api = _twitchService.GetApi(_twitchService.GetToken(user));

            var game = api.Helix.Games.GetGamesAsync(null, new List<string>() { "Super Smash Bros. Ultimate" }).GetAwaiter().GetResult();
            if (game.Games.Length > 0)
            {
                var clips = api.Helix.Clips.GetClipAsync(null, game.Games[0].Id).GetAwaiter().GetResult();
                for (int i = 0; i < clips.Clips.Length; i++)
                {
                    if (Convert.ToDateTime(clips.Clips[i].CreatedAt) > user.LastVerificationDate)
                    {
                        _newClips.Add(clips.Clips[i]);
                    }
                }
            }
        }

        public object GetResult()
        {
            return _newClips;
        }

        public bool IsTriggered()
        {
            return _newClips.Count > 0;
        }
    }
}