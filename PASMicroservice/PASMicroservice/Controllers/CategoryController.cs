using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PASMicroservice.Entities;
using PASMicroservice.Models.Category;
using PASMicroservice.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PASMicroservice.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepository categoryRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        // GET: api/categories
        [HttpGet]
        public ActionResult<List<CategoryDto>> Get()
        {
            var categories = this.categoryRepository.GetCategories();

            if (categories == null || categories.Count() == 0)
                return NoContent();

            return Ok(mapper.Map<List<CategoryDto>>(categories));
        }

        // GET api/categories
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetById(Guid id)
        {
            var category = this.categoryRepository.GetCategoryById(id);
            return Ok(mapper.Map<CategoryDto>(category));
        }

        // POST api/categories
        [HttpPost]
        public ActionResult<CategoryConfirmationDto> Post([FromBody] CategoryCreationDto category)
        {
            try
            {
                var categoryEntity = mapper.Map<Category>(category);
                var confirmation = this.categoryRepository.CreateCategory(categoryEntity);

                string location = linkGenerator.GetPathByAction("GetById", "Category", new { id = confirmation.Id });

                return Created(location, mapper.Map<CategoryConfirmationDto>(confirmation));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        // PUT api/categories
        [HttpPut()]
        public ActionResult<CategoryConfirmationDto> Put([FromBody] CategoryUpdateDto category)
        {
            try
            {
                if (this.categoryRepository.GetCategoryById(category.Id) == null)
                    return NotFound();

                Category categoryEntity = mapper.Map<Category>(category);
                CategoryConfirmation confirmation = this.categoryRepository.UpdateCategory(categoryEntity);
                return Ok(mapper.Map<CategoryConfirmationDto>(confirmation));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        // DELETE api/categories/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var category = this.categoryRepository.GetCategoryById(id);

                if (category == null)
                    return NotFound();

                this.categoryRepository.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        // OPTIONS
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
