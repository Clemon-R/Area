using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Spotify.Models
{
    public class SpotifyTokenModel : SpotifySuccessModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}
