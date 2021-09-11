using AuthService.Models;
using AuthService.Models.Requests;
using AuthService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthService.Controllers
{
    /// <summary>
    /// Kontroler sa endpoint-ima za autentifikaciju korisnika
    /// </summary>
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Prijava korisnika
        /// </summary>
        /// <returns>Token, ukoliko je autentifikacija uspešna</returns>
        /// <response code="200">Token</response>
        /// <response code="400">Pogrešna lozinka ili email adresa</response>
        /// <response code="500">Greška na serveru</response>
        [Route("/login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] Principal principal)
        {
            try
            {
                var authResponse = _authenticationService.Login(principal);
                if (authResponse.Result.Success)
                {
                    return Ok(new LoginSuccessResponse
                    {
                        Token = authResponse.Result.Token
                    });
                }
                return BadRequest(new AuthResponse
                {
                    Token = null,
                    Success = false,
                    Error = authResponse.Result.Error
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        /// <summary>
        /// Odjavljivanje korisnika
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Korisnik je odjavljen</response>
        /// <response code="400">Pogrešne vrednosti u zahtevu</response>
        /// <response code="500">Greška na serveru</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/logout")]
        [HttpPost]
        public IActionResult Logout(LogoutRequest body)
        {
            try
            {
                AuthModel authModel = _authenticationService.GetAuthModelByToken(body.Token);
                if (authModel is null)
                {
                    return BadRequest("Korisnik sa tim id-om ne postoji ili je već odjavljen");
                }
                _authenticationService.Logout(authModel.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
