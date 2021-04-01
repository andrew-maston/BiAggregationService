using AggregationService.Models;
using System.Collections.Generic;

namespace AggregationService
{
    public interface IRoomService
    {
        IReadOnlyCollection<Room> GetRoomInformation();
    }
}