using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace AggregationService.Controllers
{
    /// <summary>
    /// High level information about the model
    /// </summary>
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ILogger<SummaryController> _logger;
        private readonly ISummaryService _summaryService;

        //could have added a file upload here to take the file and initialise an instance of IfcStore
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="summaryService"></param>
        /// <param name="logger"></param>
        public SummaryController(ISummaryService summaryService, ILogger<SummaryController> logger)
        {
            _summaryService = summaryService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a collection of elements and the number of their occurrences
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Dictionary<string, int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("[controller]/getelementcounts")]
        public ActionResult GetElementCounts()
        {
            Dictionary<string, int> result;

            try
            {
                //was trying to do something asynchronous here but couldn't see a way of doing that in IfcStore
                result = _summaryService.GetElementCounts();
            } 
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, null, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }
    }
}
