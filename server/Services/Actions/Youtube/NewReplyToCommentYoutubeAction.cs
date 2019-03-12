using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Area.Services.Actions.Youtube
{
    public class NewReplyToCommentYoutubeAction : IAction
    {
        private DateTime _lastTriggerDate;
        private readonly App.YoutubeService _youtubeService;

        public TriggerCompatibilityEnum Type => throw new NotImplementedException();
        List<Activity> _newActivites = new List<Activity>();

        public ActionTypeEnum Id => throw new NotImplementedException();

        public NewReplyToCommentYoutubeAction(IServiceProvider serviceProvider)
        {
            _youtubeService = (App.YoutubeService)serviceProvider.GetService(typeof(App.YoutubeService));
        }

        public void CheckAction(Account user, DateTime lastCheck)
        {
            var api = _youtubeService.GetApi(user);

            var request = api.Comments.List("snippet");

        }

        public object GetResult()
        {
            return _newActivites;
        }

        public bool IsTriggered()
        {
            return _newActivites.Count > 0;
        }

        public DateTime GetDate()
        {
            return _lastTriggerDate;
        }
    }
}
