using AggregationService.Extensions;
using AggregationService.Models;
using AggregationService.Repositories;
using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;

namespace AggregationService
{
    public class RoomService : IRoomService
    {
        private IIfcStoreRepository<IIfcSpace> _spaceRepository;

        public RoomService(IIfcStoreRepository<IIfcSpace> spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public IReadOnlyCollection<Room> GetRoomInformation()
        {
            var spaces = _spaceRepository.GetAll();
            var rooms = new List<Room>();

            foreach (var space in spaces)
            {
                var room = new Room
                {
                    Name = space.Name.GetValueOrDefault().ToString(),
                    Area = space.GetArea()
                };

                rooms.Add(room);
            };

            return rooms.ToArray();
        }
    } 
}