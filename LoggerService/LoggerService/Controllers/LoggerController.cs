using AutoMapper;
using LoggerService.Data;
using LoggerService.Entities;
using LoggerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoggerService.ErrorHandler;
using LoggerService.AuthHelper;

namespace LoggerService.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LoggerController : Controller
    {
        private readonly ILogRepository repository;
        private readonly IMapper mapper;
        private readonly IAuthHelperr authHelperr;

        public LoggerController(ILogRepository repository, IMapper mapper, IAuthHelperr authHelperr)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.authHelperr = authHelperr;
        }


        /// <summary>
        /// Unos logova u bazu.
        /// </summary>
        /// <param name="oneLog">Model na osnovu koga se objekat upisuje u bazu.</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer unosa uspesnog novog loga \
        /// POST 'https://localhost:5010/api/log/' \
        ///     --data-urlencode 'Id="9af923b3-029d-4c13-87d0-98e238bbdb51"' \
        ///     --data-urlencode 'LogLevel=Information' \
        ///     --data-urlencode 'Microservice=testingMicroservice'\
        ///     --data-urlencode 'Message=Testiram samo post za logove' \
        ///     --data-urlencode 'TimeOfAction=01/01/2021 5:28:29 PM' \
        /// </remarks>
        /// <response code="201">Vraca kreirani log.</response>
        /// <response code="400">Greska prilikom upisa u bazu.</response>
        /// <response code="500">Greska pri unosa vrednosti u bazu.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<LogDto> Insert(Log oneLog) {

            try
            {
                Log inserted = repository.Insert(oneLog);

                if (inserted != null)
                {
                    Console.WriteLine($"Log level: {inserted.LogLevel}, ID: {inserted.Id} Microservice: {inserted.Microservice}, " +
                        $"Message: {inserted.Message}, Time: {inserted.TimeOfAction}");
                }
                else {

                    throw new LogException("Greska prilikom upisa u bazu!");
                }
                return Created("", mapper.Map<LogDto>(inserted));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Vraca sve trenutne logove iz baze.
        /// </summary>
        /// <param name="key">Sifra uz pomoc koje korisnik ima pristup podacima.</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer uspesnog vracanja logova 
        ///  -  GET 'https://localhost:5010/api/log/' \
        ///     Header: 'Authorization(API Key): Key -> Key, Value -> Bearer sifra123'
        /// </remarks>
        /// <response code="200">Vraca listu logova sa gore navedenim parametrima.</response>
        /// <response code="401">Greska pri autentifikaciji.</response>
        /// <response code="404">Ne postoji nijedno log.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult<List<LogDto>> GetLogs([FromHeader] string key) {

            if (!authHelperr.AuthUser(key))
                return Unauthorized();

            List<Log> logs = repository.GetLogs();

            if (logs == null || logs.Count == 0) {

                return NotFound();
            }

            return Ok(mapper.Map<List<LogDto>>(logs));

        }
    }
}
