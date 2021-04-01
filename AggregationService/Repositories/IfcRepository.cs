using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Ifc;

namespace AggregationService.Repositories
{
    public class IfcStoreRepository<T> : IIfcStoreRepository<T> where T : IPersistEntity
    {
        private readonly IfcStore _ifcStore;

        public IfcStoreRepository(IfcStore ifcStore)
        {
            //Ideally I'd probably intitialise this some other way, for now we'll just read the file specified in configuration on startup, is disposible so will get cleaned up
            _ifcStore = ifcStore;
        }

        public IReadOnlyCollection<T> GetAll()
        {
            using (var tx = _ifcStore.BeginTransaction($"Get all {typeof(T).Name} elements"))
            {
                var elements = _ifcStore.Instances.OfType<T>().ToArray();
                return elements;
            }
        }
    }
}
