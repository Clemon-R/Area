using Area.Wrappers.Models;

namespace server.Wrappers.Reddit.Models
{
    public class RedditTokenModel : RequestSuccessModel
    {
        public string Access_Token {get;set;}
        public string Rrefresh_Token {get;set;}
        public int Expires_In {get;set;}
        public string Token_Type {get;set;} 
        public string Scope {get;set;}       
    }
}