using System.Linq;
using Xbim.Ifc4.Interfaces;

namespace AggregationService.Extensions
{
    public static class IIfcSpaceExtensions
    {

        public static double GetArea(this IIfcSpace space)
        {
            return space.IsDefinedBy
                .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                .OfType<IIfcElementQuantity>()
                .SelectMany(qset => qset.Quantities)
                .OfType<IIfcQuantityArea>()
                //Something makes me think I'm not doing it right here
                .Select(a => {
                    double.TryParse(a.AreaValue.ToString(), out var result);
                    return result;
                })
                .Sum();
        }
    }
}
