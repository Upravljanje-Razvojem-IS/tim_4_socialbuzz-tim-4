using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PASMicroservice.Entities;
using PASMicroservice.Mocks;
using PASMicroservice.Models.Category;
using PASMicroservice.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PASMicroservice.Controllers
{
    /// <summary>
     /// CategoryController izvršava CRUD operacije nad podacima o kategorijama.
     /// </summary>
    [Route("api/categories")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly ILoggerMockRepository<CategoryController> logger;
        private readonly IAuthenticationMock authenticationMock;

        public CategoryController(ICategoryRepository categoryRepository, LinkGenerator linkGenerator, IMapper mapper,
            ILoggerMockRepository<CategoryController> logger, IAuthenticationMock authenticationMock)
        {
            this.categoryRepository = categoryRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.logger = logger;
            this.authenticationMock = authenticationMock;
        }

        /// <summary>
        /// Vraća sve kategorije listinga
        /// </summary>
        /// <returns>Lista kategorija</returns>
        /// <remarks>
        /// Primer zahteva za vraćanje svih kategorija \
        /// GET /api/categories
        /// </remarks>
        /// <response code="200">Uspešno su vraćene sve kategorije.</response>
        /// <response code="204">Ne postoji nijedna kategorija i vraća se prazan odgovor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CategoryDto>> Get()
        {
            var categories = this.categoryRepository.GetCategories();

            if (categories == null || categories.Count() == 0)
            {
                logger.LogInformation("GET Category no content.");
                return NoContent();
            }

            logger.LogInformation("GET Category successful.");
            return Ok(mapper.Map<List<CategoryDto>>(categories));
        }

        /// <summary>
        /// Vraća jednu kategoriju
        /// </summary>
        /// <param name="id">id kategorije</param>
        /// <returns>Jedna kategorija</returns>
        /// <remarks>
        /// Primer zahteva za vraćanje kategorije sa traženim id-jem \
        /// GET /api/categories/329f5f35-9ae7-4bd7-89ff-480cfa938804
        /// </remarks>
        /// <response code="200">Uspešno je vraćena kategorija.</response>
        /// <response code="404">Ne postoji kategorija sa traženim id-jem.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDto> GetById(Guid id)
        {
            var category = this.categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                logger.LogInformation("GET Category not found");
                return NotFound();
            }

            logger.LogInformation("GET Category successful.");
            return Ok(mapper.Map<CategoryDto>(category));
        }

        /// <summary>
        /// Kreiranje nove kategorije
        /// </summary>
        /// <returns>Kreirana kategorija</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove kategorije \
        /// POST /api/categories \
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MzE0MDA4MjUsImlzcyI6IlVSSVMudW5zLmFjLnJzIiwiYXVkIjoiVVJJUy51bnMuYWMucnMifQ.BnjGu6iJW3oSY_PzvS3iDEd3uY_oZJmtJFhGgdS37SQ
        /// { \
        ///     "Name": "Kompjuterske komponente", \
        /// } \
        /// Primer zahteva za kreiranje kategorije sa opcionim obeležjima 
        /// POST /api/categories \
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MzE0MDA4MjUsImlzcyI6IlVSSVMudW5zLmFjLnJzIiwiYXVkIjoiVVJJUy51bnMuYWMucnMifQ.BnjGu6iJW3oSY_PzvS3iDEd3uY_oZJmtJFhGgdS37SQ
        /// { \
        ///     "Name": "Grafičke kartice", \
        ///     "ParentCategoryId": "329f5f35-9ae7-4bd7-89ff-480cfa938804" \
        /// } \
        /// </remarks>
        /// <response code="201">Uspešno je kreirana kategorija.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpPost]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<CategoryConfirmationDto> Post([FromBody] CategoryCreationDto category)
        {
            try
            {
                var categoryEntity = mapper.Map<Category>(category);
                var confirmation = this.categoryRepository.CreateCategory(categoryEntity);

                string location = linkGenerator.GetPathByAction("GetById", "Category", new { id = confirmation.CategoryId });

                logger.LogInformation("POST Category successful.");
                return Created(location, mapper.Map<CategoryConfirmationDto>(confirmation));
            }
            catch(Exception e)
            {
                logger.LogError(e, "Error creating new category: ", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Izmena postojeće kategorije
        /// </summary>
        /// <returns>Izmenjena kategorija</returns>
        /// <remarks>
        /// Primer zahteva za izmenu kategorije \
        /// PUT /api/listings \
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MzE0MDA4MjUsImlzcyI6IlVSSVMudW5zLmFjLnJzIiwiYXVkIjoiVVJJUy51bnMuYWMucnMifQ.BnjGu6iJW3oSY_PzvS3iDEd3uY_oZJmtJFhGgdS37SQ
        /// { \
        ///     "CategoryId": "329f5f35-9ae7-4bd7-89ff-480cfa938804", \
        ///     "Name": "Računarske komponente i delovi", \
        /// } \
        /// Primer zahteva za izmenu kategorije sa opcionim obeležjima \
        /// PUT /api/listings \
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MzE0MDA4MjUsImlzcyI6IlVSSVMudW5zLmFjLnJzIiwiYXVkIjoiVVJJUy51bnMuYWMucnMifQ.BnjGu6iJW3oSY_PzvS3iDEd3uY_oZJmtJFhGgdS37SQ
        /// { \
        ///     "CategoryId": "dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa" \
        ///     "Name": "Grafičke kartice", \
        ///     "ParentCategoryId": "329f5f35-9ae7-4bd7-89ff-480cfa938804", \
        /// } \
        /// </remarks>
        /// <response code="200">Uspešno je izmenjena kategorija.</response>
        /// <response code="404">Ne postoji kategorija sa datim id-jem.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpPut]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<CategoryConfirmationDto> Put([FromBody] CategoryUpdateDto category)
        {
            try
            {
                if (this.categoryRepository.GetCategoryById(category.CategoryId) == null)
                {
                    logger.LogInformation("PUT Category not found.");
                    return NotFound();
                }
                Category categoryEntity = mapper.Map<Category>(category);
                CategoryConfirmation confirmation = this.categoryRepository.UpdateCategory(categoryEntity);

                logger.LogInformation("PUT Category successful.");
                return Ok(mapper.Map<CategoryConfirmationDto>(confirmation));
            }
            catch(Exception e)
            {
                logger.LogError(e, "Error updating existing category: ", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Brisanje jedne kategorije
        /// </summary>
        /// <param name="id">id kategorije za brisanje</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za brisanje kategorije \
        /// DELETE /api/categories/7f3bc508-5b2e-4dfe-abdd-08d974ea8872
        /// Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MzE0MDA4MjUsImlzcyI6IlVSSVMudW5zLmFjLnJzIiwiYXVkIjoiVVJJUy51bnMuYWMucnMifQ.BnjGu6iJW3oSY_PzvS3iDEd3uY_oZJmtJFhGgdS37SQ
        /// </remarks>
        /// <response code="204">Uspešno je obrisana kategorija i vraća odgovor bez sadržaja.</response>
        /// <response code="404">Ne postoji kategorija sa datim id-jem.</response>
        /// <response code="500">Greška na backend-u.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var category = this.categoryRepository.GetCategoryById(id);

                if (category == null)
                {
                    logger.LogInformation("DELETE Category not found.");
                    return NotFound();
                }
                this.categoryRepository.DeleteCategory(id);

                logger.LogInformation("DELETE Category successful.");
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting category: ", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Vraća dozvoljene HTTP metode na endpoint-u
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za pregled dostupnih metoda \
        /// OPTIONS /api/categories
        /// </remarks>
        /// <response code="200">Uspešno vraćene metode.</response>
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
