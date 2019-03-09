using Area.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Enums
{
    public enum ReactionTypeEnum
    {
        [DescriptionReactionAttribute("Ajout l'album à votre playlist", ServiceTypeEnum.Spotify, TriggerCompatibilityEnum.ListSimpleAlbum)]
        AddToPlaylistSpotify = 0
    }
}
