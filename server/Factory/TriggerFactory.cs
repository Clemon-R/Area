using Area.Enums;
using Area.Factory;
using Area.Models;
using Area.Services.Actions;
using Area.Services.Actions.Reddit;
using Area.Services.Actions.Spotify;
using Area.Services.Actions.Steam;
using Area.Services.Actions.Twitch;
using Area.Services.Reactions.Area;
using Area.Services.Reactions.Reddit;
using Area.Services.Reactions.Spotify;
using Area.Services.Reactions.Twitch;
using Area.Services.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Factory
{
    public class TriggerFactory : IFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TriggerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateTriggerTemplates(Account owner)
        {
            foreach (Trigger trigger in owner.Triggers)
            {
                CreateTriggerTemplate(trigger);
            }
        }

        public void CreateTriggerTemplate(Trigger trigger)
        {
            if (trigger.Template == null)
            {
                Type action = GetAction(trigger.ActionType);
                Type reaction = GetReaction(trigger.ReactionType);

                trigger.Template = new TriggerTemplate(action, reaction, _serviceProvider);
            }
        }

        private Type GetAction(ActionTypeEnum actionType)
        {
            switch (actionType)
            {
                case ActionTypeEnum.FollowedArtistNewReleaseSpotify:
                    return typeof(FollowedArtistNewReleaseSpotifyAction);
                case ActionTypeEnum.FollowedPlaylistUpdatedSpotify:
                    return typeof(FollowedPlaylistUpdatedSpotifyAction);
                case ActionTypeEnum.NewTopPostsReddit:
                    return typeof(NewTopPostsRedditAction);
                case ActionTypeEnum.NewReplyToCommentReddit:
                    return typeof(NewReplyToCommentRedditAction);
                case ActionTypeEnum.NewSmashClipTwitch:
                    return typeof(NewSmashClipTwitchAction);
                case ActionTypeEnum.NewSubTwitch:
                    return typeof(NewSubTwitchAction);
                case ActionTypeEnum.GameNewsSteam:
                    return typeof(GameNewsSteamAction);
                case ActionTypeEnum.NewInventoryItemSteam:
                    return typeof(NewObjectInInventorySteamAction);
                case ActionTypeEnum.NewYoutubeActivity:
                default:
                    return null;
            }
        }

        private Type GetReaction(ReactionTypeEnum reactionType)
        {
            switch (reactionType)
            {
                case ReactionTypeEnum.AddToPlaylistSpotify:
                    return typeof(AddToPlaylistSpotifyReaction);
                case ReactionTypeEnum.AddDashboardMessage:
                    return typeof(AddDashboardMessageAreaReaction);
                case ReactionTypeEnum.UpvoteCommentReddit:
                    return typeof(UpvoteCommentRedditReaction);
                case ReactionTypeEnum.UpvotePostReddit:
                    return typeof(UpvotePostRedditReaction);
                case ReactionTypeEnum.FollowUserTwitch:
                    return typeof(FollowUserTwitchReaction);
                default:
                    return null;
            }
        }
    }
}
