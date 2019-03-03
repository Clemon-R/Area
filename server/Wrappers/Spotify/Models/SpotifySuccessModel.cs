using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Spotify.Models
{
    public class SpotifySuccessModel : ISpotifyStateModel
    {
        public bool Success { get => true;}
    }
}
