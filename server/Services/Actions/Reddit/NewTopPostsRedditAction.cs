using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Helpers;
using Area.Models;
using Reddit;
using Reddit.Controllers;

namespace Area.Services.Actions
{
    public class NewTopPostsRedditAction : IAction
    {
        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.FollowedArtistNewReleaseSpotify;

        public NewTopPostsRedditAction()
        {
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        List<Post> _newTopPosts = new List<Post>();

        public void CheckAction(Models.Account user)
        {
            RedditAPI api = new RedditAPI("");

            var subreddits = api.Account.MySubscribedSubreddits();

            for (int i = 0; i < subreddits.Count; i++)
            {
                var risingPosts = subreddits[i].Posts.Top;
                
                for (int j = 0; j < risingPosts.Count; j++)
                {
                    if (risingPosts[j].Created > user.LastVerificationDate)
                    {
                        _newTopPosts.Add(risingPosts[j]);
                    }
                }
            }
        }

        public object GetResult()
        {
            return _newTopPosts;
        }

        public bool IsTriggered()
        {
            return _newTopPosts.Count > 0;
        }
    }
}
