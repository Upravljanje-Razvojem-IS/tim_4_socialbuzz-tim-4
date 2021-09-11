namespace AuthService.Models
{
    public class CheckPrincipalResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Account Account { get; set; }
    }
}
