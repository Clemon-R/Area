using Area.Wrappers.Models;

namespace Area.Wrappers.Youtube.Models
{
    public class YoutubeTokenModel : RequestSuccessModel
    {
        public string Access_Token {get;set;}
        public string Rrefresh_Token {get;set;}
        public int Expires_In {get;set;}
        public string Token_Type {get;set;}
    }
}