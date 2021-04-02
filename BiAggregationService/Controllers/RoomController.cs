using AggregationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace AggregationService.Controllers
{
    /// <summary>
    /// Used to retrieve room information
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomService _roomService;

        //could have added a file upload here to take the file and initialise an instance of IfcStore
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="roomService"></param>
        /// <param name="logger"></param>
        public RoomController(IRoomService roomService, ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a collection of rooms and their areas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<Room>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/getall")]
        public ActionResult GetAll()
        {
            IReadOnlyCollection<Room> results;

            try
            {
                //was trying to do something asynchronous here but couldn't see a way of doing that in IfcStore
                results = _roomService.GetRooms();
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
