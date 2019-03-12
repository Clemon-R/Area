using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ReactionTypeEnum
    {
        [DescriptionReactionAttribute("Ajout de tracks à une playlist spéciale", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListSimpleTrack)]
        AddToPlaylistSpotify = 0,
        [DescriptionReactionAttribute("Upvote le commentaire", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.RedditComments)]
        UpvoteCommentReddit = 1,
        [DescriptionReactionAttribute("Upvote le post", ServiceTypeEnum.Reddit, TriggerCompatibilityEnum.RedditPosts)]
        UpvotePostReddit = 2,
        [DescriptionReactionAttribute("Ecris un message sur le dashboard de l'utilisateur", ServiceTypeEnum.Area, TriggerCompatibilityEnum.String)]
        AddDashboardMessage = 3,
        [DescriptionReactionAttribute("Suis l'utilisateur sur Twitch", ServiceTypeEnum.Twitch, TriggerCompatibilityEnum.ListTwitchUser)]
        FollowUserTwitch = 4,
    }
}
