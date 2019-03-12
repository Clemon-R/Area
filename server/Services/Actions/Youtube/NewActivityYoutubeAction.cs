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
    public class NewActivityYoutubeAction : IAction
    {
        private readonly App.YoutubeService _youtubeService;
        public TriggerCompatibilityEnum Type => TriggerCompatibilityEnum.YoutubeActivity;
        List<Activity> _newActivites = new List<Activity>();

        public ActionTypeEnum Id => ActionTypeEnum.NewYoutubeActivity;

        public NewActivityYoutubeAction(IServiceProvider serviceProvider)
        {
            _youtubeService = (App.YoutubeService)serviceProvider.GetService(typeof(App.YoutubeService));
        }

        public void CheckAction(Account user, DateTime lastCheck)
        {
            var api = _youtubeService.GetApi(user);

            var request = api.Activities.List("snippet");
            request.PublishedAfter = lastCheck;
            var result = request.Execute();

            foreach(var searchRes in result.Items)
            {
                _newActivites.Add(searchRes);
            }
        }

        public object GetResult()
        {
            return _newActivites;
        }

        public bool IsTriggered()
        {
            return _newActivites.Count > 0;
        }

        private DateTime _lastTriggerDate;

        public DateTime GetDate()
        {
            return _lastTriggerDate;
        }
    }
}
