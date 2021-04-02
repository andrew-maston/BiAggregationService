using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Ifc;

namespace AggregationService.Repositories
{
    public class IfcStoreRepository<T> : IIfcStoreRepository<T> where T : IPersistEntity
    {
        private readonly IfcStore _ifcStore;
        private readonly ILogger _logger;

        public IfcStoreRepository(IfcStore ifcStore)//, ILogger logger)
        {
            //Ideally I'd probably intitialise this some other way, for now we'll just read the file specified in configuration on startup, is disposible so will get cleaned up
            _ifcStore = ifcStore;
            //_logger = logger;
        }

        public IReadOnlyCollection<T> GetAll()
        {
            var message = $"Get all {typeof(T).Name} elements";
            using (var tx = _ifcStore.BeginTransaction(message))
            {
                //_logger.Log(LogLevel.Information, message);
                var elements = _ifcStore.Instances.OfType<T>().ToArray();
                return elements;
            }
        }
    }
}
