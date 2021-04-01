using AggregationService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc4.Interfaces;

namespace AggregationService
{
    internal class SummaryService : ISummaryService
    {
        private readonly IIfcStoreRepository<IIfcElement> _elementRepository;

        public SummaryService(IIfcStoreRepository<IIfcElement> elementRepository)
        {
            _elementRepository = elementRepository;
        }

        public Dictionary<string, int> GetElementCounts()
        {
            var results = _elementRepository.GetAll();
            var modelElementDictionary = new Dictionary<string, int>();

            //not sure how to get labels, could do something where I've got a switch case with my own enum Door, Wall, Window etc, looked into the enums but can't see one
            var elementGroups = results.GroupBy(r => r.GetType().Name);

            foreach (var elementType in elementGroups)
            {
                modelElementDictionary.Add(elementType.Key, elementType.Count());
            }

            return modelElementDictionary;
        }
    }
}