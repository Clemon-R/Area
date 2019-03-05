using Area.Services.App;
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

        public AreaController(
            AreaService areaService)
        {
            _areaService = areaService;
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
    }
}
