using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;
using Reddit.Controllers;

namespace Area.Services.Reactions.Reddit
{
    public class UpvotePostRedditReaction : IReaction
    {
        public ReactionTypeEnum Id => ReactionTypeEnum.UpvotePostReddit;

        public TriggerCompatibilityEnum Type => TriggerCompatibilityEnum.RedditPosts;

        public bool Execute(Models.Account user, object result, string args)
        {
            List<Post> posts = result as List<Post>;

            for (int i = 0; i < posts.Count; i++)
            {
                posts[i].Upvote();
            }
            return true;
        }
    }
}
