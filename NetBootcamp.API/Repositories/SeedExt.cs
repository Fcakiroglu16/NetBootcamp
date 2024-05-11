namespace NetBootcamp.API.Repositories
{
    public static class SeedExt
    {
        public static void SeedDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                SeedData.SeedDatabase(dbContext);
            }
        }


        //public static decimal CalculateTax(this decimal price)
        //{
        //    return price * 1.20m;


        //}
    }


    //public class Calculate
    //{
    //    public void CalculateTax()
    //    {
    //        decimal price = 100;
    //        price.CalculateTax();
    //    }
    //}
}