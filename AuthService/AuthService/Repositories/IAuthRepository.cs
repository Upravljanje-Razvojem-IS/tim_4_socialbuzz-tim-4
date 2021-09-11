using AuthService.Models;
using System;

namespace AuthService.Repositories
{
    public interface IAuthRepository
    {
        AuthModel GetAuthByUserId(Guid userId);
        AuthModel GetAuthModelByToken(string token);
        AuthModel CreateAuthModel(AuthModel authModel);
        void DeleteAuthInfo(Guid userId);
    }
}
