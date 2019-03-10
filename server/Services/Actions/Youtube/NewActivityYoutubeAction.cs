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
        public TriggerCompatibilityEnum Type => throw new NotImplementedException();
        List<Activity> _newActivites = new List<Activity>();

        public ActionTypeEnum Id => throw new NotImplementedException();

        public void CheckAction(Account user)
        {
            var api = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "Area",
                ApiKey = "devkey",
            });
            var request = api.Activities.List("snippet");
            request.PublishedAfter = user.LastVerificationDate;
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
    }
}
