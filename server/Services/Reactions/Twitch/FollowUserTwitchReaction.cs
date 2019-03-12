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

namespace Area.Services.Reactions.Twitch
{
    public class FollowUserTwitchReaction : IReaction
    {
        public ReactionTypeEnum Id => ReactionTypeEnum.FollowUserTwitch;

        public TriggerCompatibilityEnum Type { get; private set; }

        private readonly TwitchService _twitchService;

        public FollowUserTwitchReaction(IServiceProvider serviceProvider)
        {
            _twitchService = (TwitchService)serviceProvider.GetService(typeof(TwitchService));
            Type = Id.GetAttributeOfType<DescriptionReactionAttribute>().Compatibility;
        }

        public bool Execute(Account user, object result, string args)
        {
            TwitchAPI api = _twitchService.GetApi(user);
            List<User> users = result as List<User>;

            Console.WriteLine("Executing FollowUsers Twitch Reaction");
            if (users == null || api == null)
                return false;
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine("Following " + users[i].DisplayName);
                api.V5.Users.FollowChannelAsync(users[i].Id, users[i].Id);
            }
            return true;
        }
    }
}
