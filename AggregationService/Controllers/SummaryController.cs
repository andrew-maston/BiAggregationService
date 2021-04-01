using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SummaryController : ControllerBase
    {
        private readonly ILogger<SummaryController> _logger;
        private readonly ISummaryService _summaryService;

        //could have added a file upload here to take the file and initialise an instance of IfcStore
        public SummaryController(ISummaryService summaryService, ILogger<SummaryController> logger)
        {
            _summaryService = summaryService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetElementCounts()
        {
            Dictionary<string, int> result;

            try
            {
                //was trying to do something asynchronous here but couldn't see a way of doing that in IfcStore
                result = _summaryService.GetElementCounts();
            } 
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }
    }
}
