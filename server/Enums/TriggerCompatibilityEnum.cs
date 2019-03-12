using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum TriggerCompatibilityEnum
    {
        None = -1,
        ListSimpleAlbum = 0,
        ListSimpleTrack,
        ListPlaylistTrack,
        ListTwitchUser,
        String,
        RedditComments,
        RedditPosts,
        SteamNews,
        SteamInventoryItem,
        YoutubeActivity
    }
}
