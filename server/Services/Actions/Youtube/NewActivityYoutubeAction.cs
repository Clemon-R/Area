using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Area.Services.Actions.Youtube
{
    public class NewActivityYoutubeAction : IAction
    {
        public TriggerCompatibilityEnum Type => throw new NotImplementedException();

        public ActionTypeEnum Id => throw new NotImplementedException();

        public void CheckAction(Account user)
        {
            var api = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "Area",
                ApiKey = "devkey",
            });
            api.Activities.List("");
        }

        public object GetResult()
        {
            throw new NotImplementedException();
        }

        public bool IsTriggered()
        {
            throw new NotImplementedException();
        }
    }
}
