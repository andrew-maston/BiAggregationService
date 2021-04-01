using AggregationService.Models;
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
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly IRoomService _roomService;

        //could have added a file upload here to take the file and initialise an instance of IfcStore
        public RoomsController(IRoomService roomService, ILogger<RoomsController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetRoomInformation()
        {
            IReadOnlyCollection<Room> results;

            try
            {
                //was trying to do something asynchronous here but couldn't see a way of doing that in IfcStore
                results = _roomService.GetRoomInformation();
            } 
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, null, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(results);
        }
    }
}
