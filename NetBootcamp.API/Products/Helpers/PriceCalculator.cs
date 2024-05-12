namespace NetBootcamp.API.Products
{
    public class PriceCalculator
    {
        public decimal CalculateKdv(decimal price, decimal tax)
        {
            return price * tax;
        }
    }
}