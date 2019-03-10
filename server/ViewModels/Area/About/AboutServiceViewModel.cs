using System.Collections.Generic;

namespace Area.ViewModels.Area.About
{
    public class AboutServiceViewModel
    {
        public string Name {get;set;}
        public List<AboutActionReactionViewModel> Actions {get;set;}
        public List<AboutActionReactionViewModel> Reactions {get;set;}
    }
}