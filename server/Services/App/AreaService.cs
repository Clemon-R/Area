using Area.Enums;
using Area.Factory;
using Area.Helpers;
using Area.Models;
using Area.ViewModels;
using Area.ViewModels.Area;
using Area.ViewModels.Area.About;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Area.Services.App
{
    public class AreaService : IService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ApplicationDbContext _context;
        private readonly TriggerFactory _triggerFactory;

        public AreaService(
            IHttpContextAccessor accessor,
            ApplicationDbContext context,
            TriggerFactory triggerFactory)
        {
            _context = context;
            _triggerFactory = triggerFactory;
            _accessor = accessor;
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

		internal AboutViewModel About()
		{
            var data = _accessor.HttpContext.Connection.RemoteIpAddress.ToString().Split(":");
            var result = new AboutViewModel() {
                Client = new AboutClientViewModel() {
                    Host =  data[data.Length > 0 ? data.Length - 1 : 0]
                },
                Server = new AboutServerViewModel() {
                    Current_time = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString(),
                    Services = new List<AboutServiceViewModel>()
                }
            };
            foreach (int i in Enum.GetValues(typeof(ServiceTypeEnum)))
            {
                var type = (ServiceTypeEnum)i;
                var service = new AboutServiceViewModel(){
                    Name = type.ToString(),
                    Actions = this.GetActionForService(type),
                    Reactions = this.GetReactionForService(type)
                };
                result.Server.Services.Add(service);
            }
			return result;
		}

        private List<AboutActionReactionViewModel> GetActionForService(ServiceTypeEnum id)
        {
            var result = new List<AboutActionReactionViewModel>();
            foreach (int i in Enum.GetValues(typeof(ActionTypeEnum)))
            {
                var type = (ActionTypeEnum)i;
                var description = type.GetAttributeOfType<DescriptionActionAttribute>();
                if (id != description.Service)
                    continue;
                var action = new AboutActionReactionViewModel()
                {
                    Name = type.ToString(),
                    Description = description.Description
                };
                result.Add(action);
            }
            return result;
        }

         private List<AboutActionReactionViewModel> GetReactionForService(ServiceTypeEnum id)
        {
            var result = new List<AboutActionReactionViewModel>();
            foreach (ReactionTypeEnum type in Enum.GetValues(typeof(ReactionTypeEnum)))
            {
                var description = type.GetAttributeOfType<DescriptionReactionAttribute>();
                if (id != description.Service)
                    continue;
                var action = new AboutActionReactionViewModel()
                {
                    Name = type.ToString(),
                    Description = description.Description
                };
                result.Add(action);
            }
            return result;
        }
    }
}
