namespace Bootcamp.Web.TokenServices
{
    public record TokenOption()
    {
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
    }
}