using Area.Enums;
using Area.Helpers;
using Area.ViewModels;
using Area.ViewModels.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public class AreaService : IService
    {
        private readonly ApplicationDbContext _context;

        public AreaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ActionReactionViewModel> GetActions()
        {
            var result = new List<ActionReactionViewModel>();
            foreach (int i in Enum.GetValues(typeof(ActionTypeEnum)))
            {
                ActionTypeEnum type = (ActionTypeEnum)i;
                var description = type.GetAttributeOfType<DescriptionAttribut>();
                var tmp = new ActionReactionViewModel()
                {
                    Id = (int)type,
                    Compatibility = description.Compatibility,
                    Description = description.Description
                };
                result.Add(tmp);
            }
            return result;
        }

        public List<ActionReactionViewModel> GetReactions()
        {
            var result = new List<ActionReactionViewModel>();
            foreach (int i in Enum.GetValues(typeof(ReactionTypeEnum)))
            {
                var type = (ReactionTypeEnum)i;
                var description = type.GetAttributeOfType<DescriptionAttribut>();
                var tmp = new ActionReactionViewModel()
                {
                    Id = (int)type,
                    Compatibility = description.Compatibility,
                    Description = description.Description
                };
                result.Add(tmp);
            }
            return result;
        }

        public IViewModel NewArea(NewAreaViewModel model)
        {

            return null;
        }
    }
}
