using System.Collections.Generic;

namespace AggregationService
{
    public interface ISummaryService
    {
        public Dictionary<string, int> GetElementCounts();
    }
}