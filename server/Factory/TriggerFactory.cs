﻿using Area.Enums;
using Area.Factory;
using Area.Models;
using Area.Services.Actions.Spotify;
using Area.Services.Reactions.Spotify;
using Area.Services.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Factory
{
    public class TriggerFactory : IFactory
    {
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

                trigger.Template = new TriggerTemplate(action, reaction);
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
                default:
                    return null;
            }
        }
    }
}
