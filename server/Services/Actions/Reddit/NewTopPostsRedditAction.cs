using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Helpers;
using Area.Models;
using Area.Services.App;
using Reddit;
using Reddit.Controllers;

namespace Area.Services.Actions
{
    public class NewTopPostsRedditAction : IAction
    {
        private readonly RedditService _redditService;

        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.NewTopPostsReddit;

        public NewTopPostsRedditAction(IServiceProvider serviceProvider)
        {
            _redditService = (RedditService)serviceProvider.GetService(typeof(RedditService));
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        List<Post> _newTopPosts = new List<Post>();

        public void CheckAction(Models.Account user)
        {
            RedditAPI api = _redditService.GetApi(_redditService.GetToken(user));

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
