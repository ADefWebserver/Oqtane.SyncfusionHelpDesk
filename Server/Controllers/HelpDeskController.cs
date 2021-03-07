using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Syncfusion.HelpDesk.Models;
using Syncfusion.HelpDesk.Repository;

namespace Syncfusion.HelpDesk.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class HelpDeskController : Controller
    {
        private readonly IHelpDeskRepository _HelpDeskRepository;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public HelpDeskController(IHelpDeskRepository HelpDeskRepository, ILogManager logger, IHttpContextAccessor accessor)
        {
            _HelpDeskRepository = HelpDeskRepository;
            _logger = logger;

            if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
            {
                _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
            }
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.HelpDesk> Get(string moduleid)
        {
            return _HelpDeskRepository.GetHelpDesks(int.Parse(moduleid));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.HelpDesk Get(int id)
        {
            Models.HelpDesk HelpDesk = _HelpDeskRepository.GetHelpDesk(id);
            if (HelpDesk != null && HelpDesk.ModuleId != _entityId)
            {
                HelpDesk = null;
            }
            return HelpDesk;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.HelpDesk Post([FromBody] Models.HelpDesk HelpDesk)
        {
            if (ModelState.IsValid && HelpDesk.ModuleId == _entityId)
            {
                HelpDesk = _HelpDeskRepository.AddHelpDesk(HelpDesk);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "HelpDesk Added {HelpDesk}", HelpDesk);
            }
            return HelpDesk;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.HelpDesk Put(int id, [FromBody] Models.HelpDesk HelpDesk)
        {
            if (ModelState.IsValid && HelpDesk.ModuleId == _entityId)
            {
                HelpDesk = _HelpDeskRepository.UpdateHelpDesk(HelpDesk);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "HelpDesk Updated {HelpDesk}", HelpDesk);
            }
            return HelpDesk;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.HelpDesk HelpDesk = _HelpDeskRepository.GetHelpDesk(id);
            if (HelpDesk != null && HelpDesk.ModuleId == _entityId)
            {
                _HelpDeskRepository.DeleteHelpDesk(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "HelpDesk Deleted {HelpDeskId}", id);
            }
        }
    }
}
