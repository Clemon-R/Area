using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Spotify.Models
{
    public class SpotifyProfileModel : SpotifySuccessModel
    {
        public string Birthdate { get; set; }
        public string Country { get; set; }
        public string Display_Name { get; set; }
        public string Email { get; set; }
        public string Href { get; set; }
        public string Id{ get; set; }
        public string Product { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}
