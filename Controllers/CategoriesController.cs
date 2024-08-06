using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.API.Data;
using MyBlog.API.Models.Domain;
using MyBlog.API.Models.DTO;
using MyBlog.API.Repositories.Interface;

namespace MyBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(ICategoriesRepository repository)
        {
            Repository = repository;
        }

        public ICategoriesRepository Repository { get; }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory(CategoryRequest request)
        {
            var category = new Category
            {
                name = request.name,
                url_handle = request.url_handle
            };
            await Repository.CreateAsync(category);

            var response = new CategoryResponse
            {
                id = category.id,
                name = category.name,
                url_handle = category.url_handle
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> getAllCategories()
        {
            var catergories = await Repository.getCategoriesAsync();
            var respone = new List<CategoryResponse>();
            foreach(var category in catergories)
            {
                respone.Add(new CategoryResponse { 
                    id = category.id, 
                    name = category.name, 
                    url_handle = category.url_handle 
                });
            }
            return Ok(respone);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getCategoryByID([FromRoute] Guid id)
        {
           var category = await Repository.getCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            else
            {
                var categ = new CategoryResponse{ id = category.id, name = category.name, url_handle = category.url_handle };   
                return Ok(categ);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> updateCategory(Guid id, UpdateCategorydto request)
        {
            var category = new Category { id = id, name = request.name, url_handle = request.url_handle };
            category = await Repository.updatecategory(category);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> deleteCategory(Guid id)
        {
            var category = await Repository.deletecategory(id);
            if(category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }
     }
}
