using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ReactionTypeEnum
    {
        [DescriptionAttribut("Ajout l'album à votre playlist", TriggerCompatibilityEnum.ListSimpleAlbum, ServiceTypeEnum.Spotify)]
        AddToPlaylistSpotify = 0
    }
}
