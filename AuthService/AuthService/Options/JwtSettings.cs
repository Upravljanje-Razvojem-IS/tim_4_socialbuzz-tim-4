namespace AuthService.Options
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int MinutesToExpiration { get; set; }
    }
}
