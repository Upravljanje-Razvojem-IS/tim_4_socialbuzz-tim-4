using AuthService.Models;
using System;
using System.Linq;

namespace AuthService.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _authDbContext;
        public AuthRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public AuthModel CreateAuthModel(AuthModel authModel)
        {
            var createdAuthModel = _authDbContext.AuthModel.Add(authModel);
            _authDbContext.SaveChanges();
            return createdAuthModel.Entity;
        }

        public void DeleteAuthInfo(Guid userId)
        {
            var authModel = _authDbContext.AuthModel.FirstOrDefault(i => i.Id == userId);
            _authDbContext.Remove(authModel);
            _authDbContext.SaveChanges();
        }

        public AuthModel GetAuthByUserId(Guid userId)
        {
            return _authDbContext.AuthModel.FirstOrDefault(i => i.Id == userId);
        }

        public AuthModel GetAuthModelByToken(string token)
        {
            return _authDbContext.AuthModel.FirstOrDefault(i => i.Token.Equals(token.ToString()));
        }
    }
}
