﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly IUserMockRepository userMockRepository;
        private readonly ILoggerMockRepository<CategoryController> logger;

        public CategoryController(ICategoryRepository categoryRepository, LinkGenerator linkGenerator, IMapper mapper,
            IUserMockRepository userMockRepository, ILoggerMockRepository<CategoryController> logger)
        {
            this.categoryRepository = categoryRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

            this.userMockRepository = userMockRepository;
            this.logger = logger;
        }

        // GET: api/categories
        [HttpGet]
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

        // GET api/categories
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetById(Guid id)
        {
            var category = this.categoryRepository.GetCategoryById(id);
            logger.LogInformation("GET Category successful.");
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

                logger.LogInformation("POST Category successful.");
                return Created(location, mapper.Map<CategoryConfirmationDto>(confirmation));
            }
            catch(Exception e)
            {
                logger.LogError(e, "Error creating new category: ", e.Message);
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

        // DELETE api/categories/5
        [HttpDelete("{id}")]
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

        // OPTIONS
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
