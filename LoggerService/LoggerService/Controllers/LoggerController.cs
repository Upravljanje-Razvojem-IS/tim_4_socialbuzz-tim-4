using AutoMapper;
using LoggerService.Data;
using LoggerService.Entities;
using LoggerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public ActionResult<LogDto> Insert([FromForm] Log oneLog) {

            try
            {
                Log inserted = repository.Insert(oneLog);
                return Created("", mapper.Map<LogDto>(oneLog));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
