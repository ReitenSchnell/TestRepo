namespace TestLibrary.Tests
{
    internal static class Comparer
    {
        public static bool Compare(CalculationResult actual, CalculationResult expected)
        {
            if ((actual == null) && (expected == null))
                return true;

            if ((actual == null) || (expected == null))
                return false;

            return actual.TotalAmount == expected.TotalAmount;
        }
    }
}
