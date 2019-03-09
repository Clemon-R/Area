using Area.Helpers;
using Area.Models;
using Area.ViewModels.Account;
using Area.ViewModels.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area
{
    public class ViewModelFiller
    {
        public static AccountViewModel FillAccount(Account current)
        {
            var result = new AccountViewModel()
            {
                Token = current.Token,
                Username = current.UserName
            };
            return result;
        }

        public static ActionViewModel FillAction(int id, DescriptionActionAttribute description)
        {
            var result = new ActionViewModel()
            {
                Id = id,
                Compatibilitys = description.Compatibilitys,
                Description = description.Description,
                Service = description.Service
            };
            return result;
        }

        public static ReactionViewModel FillReaction(int id, DescriptionReactionAttribute description)
        {
            var result = new ReactionViewModel()
            {
                Id = id,
                Compatibility = description.Compatibility,
                Description = description.Description,
                Service = description.Service
            };
            return result;
        }
    }
}
