namespace bootcamp.Service.Products.Helpers
{
    public class PriceCalculator
    {
        public decimal CalculateKdv(decimal price, decimal tax)
        {
            return price * tax;
        }
    }
}