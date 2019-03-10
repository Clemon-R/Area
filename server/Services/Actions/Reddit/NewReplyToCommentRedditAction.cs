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

namespace Area.Services.Actions.Reddit
{
    public class NewReplyToCommentRedditAction : IAction
    {
        private readonly RedditService _redditService;

        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.NewReplyToCommentReddit;

        public NewReplyToCommentRedditAction(IServiceProvider serviceProvider)
        {
            _redditService = (RedditService)serviceProvider.GetService(typeof(RedditService));
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        List<Comment> _newReplies = new List<Comment>();

        public void CheckAction(Models.Account user)
        {
            RedditAPI api = _redditService.GetApi(user);

            var comments = api.Account.Me.CommentHistory();

            for (int i = 0; i < comments.Count; i++)
            {
                var replies = comments[i].Replies;

                for (int j = 0; j < replies.Count; j++)
                {
                    if (replies[j].Created > user.LastVerificationDate)
                    {
                        _newReplies.Add(replies[j]);
                    }
                }
            }
        }

        public object GetResult()
        {
            return _newReplies;
        }

        public bool IsTriggered()
        {
            return _newReplies.Count > 0;
        }
    }
}
