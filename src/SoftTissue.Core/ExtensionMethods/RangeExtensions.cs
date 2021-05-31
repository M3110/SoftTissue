using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains extension methods for <see cref="Range"/>
    /// </summary>
    public static class RangeExtensions
    {
        /// <summary>
        /// This method checks if <see cref="Range"/> is valid.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="range"></param>
        /// <param name="name"></param>
        /// <param name="response"></param>
        public static void Validate<TResponse>(this Range range, string name, TResponse response)
            where TResponse : OperationResponseBase
        {
            response.AddErrorIfNegativeOrZero(range.InitialPoint, $"{name} initial point");

            if (range.Step.HasValue)
                response.AddErrorIfNegativeOrZero(range.Step.Value, $"{name} step");

            if (range.FinalPoint.HasValue)
            {
                response
                    .AddErrorIfNegativeOrZero(range.FinalPoint.Value, $"{name} final point")
                    .AddErrorIf(() => range.InitialPoint > range.FinalPoint.Value, "The initial point must be less than final point.");
            }
        }

        public static IEnumerable<double> ToEnumerable(this Range value)
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
