using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains extension methods for <see cref="Range"/>
    /// </summary>
    public static class RangeExtensions
    {
        public static IEnumerable<double> ToList(this Range value)
        {
            if (value == null)
                return null;

            if (value.Step.HasValue == false || value.FinalPoint.HasValue == false)
                return new List<double> { value.InitialPoint };

            var list = new List<double>();

            double point = value.InitialPoint;

            while (point < value.FinalPoint)
            {
                list.Add(point);
                point += value.Step.Value;
            }

            list.Add(value.FinalPoint.Value);

            return list;
        }
    }
}
