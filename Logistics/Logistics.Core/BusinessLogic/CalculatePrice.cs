namespace Logistics.Core.BusinessLogic
{
    public static class CalculatePrice
    {
        public static double Calculate(int pieces, double price, double distancePrice, double weightcoefficient)
        {
            return (pieces * price) + (distancePrice * weightcoefficient);
        }
    }
}
