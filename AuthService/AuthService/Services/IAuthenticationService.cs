using AuthService.Models;
using System;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> Login(Principal principal);
        void Logout(Guid userId);
        AuthModel GetAuthModelByUserId(Guid userId);
        AuthModel GetAuthModelByToken(string token);
    }
}
