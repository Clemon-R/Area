using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;
using Reddit.Controllers;

namespace Area.Services.Reactions.Reddit
{
    public class UpvoteCommentRedditReaction : IReaction
    {
        public ReactionTypeEnum Id => ReactionTypeEnum.UpvoteCommentReddit;

        public TriggerCompatibilityEnum Type => TriggerCompatibilityEnum.RedditComments;

        public bool Execute(Models.Account user, object result, string args)
        {
            List<Comment> comments = result as List<Comment>;

            for (int i = 0; i < comments.Count; i++)
            {
                comments[i].Upvote();
            }
            return true;
        }
    }
}
