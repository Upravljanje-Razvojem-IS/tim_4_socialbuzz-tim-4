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

namespace LoggerService.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LoggerController : Controller
    {
        private readonly ILogRepository repository;
        private readonly IMapper mapper;

        public LoggerController(ILogRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult<LogDto> Insert(Log oneLog) {

            try
            {
                Log inserted = repository.Insert(oneLog);
                Console.WriteLine($"Log level: {oneLog.LogLevel}, ID: {oneLog.Id} Microservice: {oneLog.Microservice}, " +
                    $"Message: {oneLog.Message}, Time: {oneLog.TimeOfAction}" );

                return Created("", mapper.Map<LogDto>(oneLog));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
