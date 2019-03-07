using Area.Services.App;
using Area.ViewModels;
using Area.ViewModels.Account;
using Area.ViewModels.Area;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _areaService;
        private readonly AccountService _accountService;

        public AreaController(
            AreaService areaService,
            AccountService accountService)
        {
            _areaService = areaService;
            _accountService = accountService;
        }

        [HttpGet("actions")]
        public List<ActionReactionViewModel> GetActions()
        {
            return _areaService.GetActions();
        }

        [HttpGet("reactions")]
        public List<ActionReactionViewModel> GetReactions()
        {
            return _areaService.GetReactions();
        }

        [HttpPost("new")]
        public IViewModel NewArea([FromBody] NewAreaViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _areaService.NewArea(account, model);
        }

        [HttpPost("triggers")]
        public List<TriggerViewModel> GetTriggers([FromBody] ConnectedViewModel model)
        {
            var account = _accountService.GetAccount(model);
            return _areaService.GetTriggers(account);
        }

        [HttpPost("delete/trigger/{id}")]
        public IViewModel DeleteTrigger([FromBody] ConnectedViewModel model, int id)
        {
            var account = _accountService.GetAccount(model);
            return _areaService.DeleteTrigger(account, id);
        }
    }
}
