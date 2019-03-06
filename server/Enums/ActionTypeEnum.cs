using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ActionTypeEnum
    {
        [DescriptionAttribut("Test1", TriggerCompatibilityEnum.ListSimpleAlbum)]
        FollowedArtistNewReleaseSpotify = 0,
        [DescriptionAttribut("Test2", TriggerCompatibilityEnum.ListPlaylistTrack)]
        FollowedPlaylistUpdatedSpotify = 1,
        [DescriptionAttribut("Test3", TriggerCompatibilityEnum.ListTwitchFollowers)]
        NewSubTwitch = 2
    }
}
