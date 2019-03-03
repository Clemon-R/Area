using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Spotify.Models
{
    public class SpotifyFailedModel : ISpotifyStateModel
    {
        public bool Success => false;
        public string Error { get; set; }
    }
}
