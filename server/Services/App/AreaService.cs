using Area.Enums;
using Area.Factory;
using Area.Helpers;
using Area.Models;
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
        private readonly TriggerFactory _triggerFactory;

        public AreaService(ApplicationDbContext context,
            TriggerFactory triggerFactory)
        {
            _context = context;
            _triggerFactory = triggerFactory;
        }

        public List<ActionViewModel> GetActions()
        {
            var result = new List<ActionViewModel>();
            foreach (int i in Enum.GetValues(typeof(ActionTypeEnum)))
            {
                ActionTypeEnum type = (ActionTypeEnum)i;
                var description = type.GetAttributeOfType<DescriptionActionAttribute>();
                result.Add(ViewModelFiller.FillAction(i, description));
            }
            return result;
        }

        public List<ReactionViewModel> GetReactions()
        {
            var result = new List<ReactionViewModel>();
            foreach (int i in Enum.GetValues(typeof(ReactionTypeEnum)))
            {
                var type = (ReactionTypeEnum)i;
                var description = type.GetAttributeOfType<DescriptionReactionAttribute>();
                result.Add(ViewModelFiller.FillReaction(i, description));
            }
            return result;
        }

        public List<TriggerViewModel> GetTriggers(Account owner)
        {
            var result = new List<TriggerViewModel>();
            foreach (var trigger in owner.Triggers)
            {
                var tmp = new TriggerViewModel()
                {
                    Id = trigger.Id,
                    ActionId = trigger.ActionType,
                    ReactionId = trigger.ReactionType
                };
                result.Add(tmp);
            }
            return result;
        }

        public IViewModel DeleteTrigger(Account owner, int id)
        {
            var trigger = owner.Triggers.FirstOrDefault(t => t.Id == id);
            if (trigger == null)
                return new ErrorViewModel() { Error="AREA non trouvé"};
            owner.Triggers.Remove(trigger);
            _context.Remove(trigger);
            _context.Update(owner);
            _context.SaveChanges();
            return new SuccessViewModel();
        }

        public IViewModel NewArea(Account account, NewAreaViewModel model)
        {
            var action = (ActionTypeEnum)model.ActionId;
            var reaction = (ReactionTypeEnum)model.ReactionId;
            if (account.Triggers.Where(t => t.ActionType == action && t.ReactionType == reaction).Any())
                return new ErrorViewModel() {Error="AREA déjà existant, impossible de le créé" };
            Console.WriteLine("AreaService(NewArea): Creating new AREA...");
            var trigger = new Trigger()
            {
                ActionType = action,
                ReactionType = reaction
            };
            Console.WriteLine("AreaService(NewArea): Saving new AREA...");
            _context.Add(trigger);
            account.Triggers.Add(trigger);
            _context.Update(account);
            _context.SaveChanges();
            Console.WriteLine("AreaService(NewArea): Creating the AREA template...");
            _triggerFactory.CreateTriggerTemplate(trigger);
            Console.WriteLine("AreaService(NewArea): AREA created");
            return new SuccessViewModel();
        }
    }
}
