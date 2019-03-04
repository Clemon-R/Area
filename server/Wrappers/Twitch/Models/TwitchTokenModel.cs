using Area.Wrappers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Wrappers.Twitch.Models
{
    public class TwitchTokenModel : RequestSuccessModel
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public int Expires_In { get; set; }
        public string[] Scope { get; set; }
        public string Token_Type { get; set; }
    }
}
