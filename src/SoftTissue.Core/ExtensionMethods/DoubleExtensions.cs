namespace SoftTissue.Core.ExtensionMethods
{
    public static class DoubleExtensions
    {
		public static double Factorial(this double value)
		{
			if (value == 1 || value == 0)
			{
				return 1;
			}

			double previousFactorial = (value - 1).Factorial();

			return value * previousFactorial;
		}
	}
}
