using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Area.Enums;
using Area.Helpers;
using Area.Models;

namespace Area.Services.Actions.Steam
{
    public class NewObjectInInventorySteamAction : IAction
    {
        public TriggerCompatibilityEnum Type { get; private set; }

        public ActionTypeEnum Id => ActionTypeEnum.FollowedArtistNewReleaseSpotify;

        public NewObjectInInventorySteamAction()
        {
            Type = Id.GetAttributeOfType<DescriptionActionAttribute>().Compatibilitys[0];
        }

        public void CheckAction(Account user)
        {
            var api = new SteamWebAPI2.Interfaces.PlayerService("devkey");
            //var iventoryApi = new SteamWebAPI2.Interfaces.I

            throw new NotImplementedException();
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
