namespace UserService.Models.Auth
{
    public class CheckPrincipalResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AccountDTO Account { get; set; }
    }
}
