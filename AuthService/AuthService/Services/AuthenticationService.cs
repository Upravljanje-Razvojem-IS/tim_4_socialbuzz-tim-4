using AuthService.Models;
using AuthService.Options;
using AuthService.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ServicesSettings _settings;
        private readonly JwtSettings _jwtSettings;
        private readonly IAuthRepository _authRepository;

        public AuthenticationService(IConfiguration configuration, JwtSettings jwtSettings, IAuthRepository authRepository)
        {
            _settings = new ServicesSettings
            {
                UserService = configuration["Services:UserService"]
            };
            _jwtSettings = jwtSettings;
            _authRepository = authRepository;
        }

        public AuthModel GetAuthModelByToken(string token)
        {
            return _authRepository.GetAuthModelByToken(token);
        }

        public AuthModel GetAuthModelByUserId(Guid userId)
        {
            return _authRepository.GetAuthByUserId(userId);
        }

        public async Task<AuthResponse> Login(Principal principal)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(_settings.UserService + "User/check", principal).Result;
                CheckPrincipalResponse res = await response.Content.ReadAsAsync<CheckPrincipalResponse>();
                if (!response.IsSuccessStatusCode)
                {
                    return new AuthResponse
                    {
                        Token = null,
                        Success = false,
                        Error = res.Message.ToString()
                    };
                }

                Guid id = res.Account.Id;
                string role = res.Account.Role;
                DateTime dateIssued = DateTime.UtcNow;

                AuthModel authModel = _authRepository.GetAuthByUserId(id);
                if (authModel != null)
                {
                    return new AuthResponse
                    {
                        Token = authModel.Token,
                        Success = true
                    };
                }

                var token = CreateToken(id.ToString(), role);
                authModel = new AuthModel
                {
                    Id = id,
                    Role = role,
                    Token = token,
                    TimeOfIssuingToken = dateIssued
                };

                _authRepository.CreateAuthModel(authModel);
                return new AuthResponse
                {
                    Token = token,
                    Success = true
                };
            }
        }

        public void Logout(Guid userId)
        {
            _authRepository.DeleteAuthInfo(userId);
        }

        private string CreateToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", userId),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.MinutesToExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
