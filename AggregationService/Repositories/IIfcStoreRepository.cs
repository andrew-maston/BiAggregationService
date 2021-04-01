using System.Collections.Generic;
using Xbim.Common;

namespace AggregationService.Repositories
{
    public interface IIfcStoreRepository<T> where T : IPersistEntity
    {
        IReadOnlyCollection<T> GetAll();
    }
}