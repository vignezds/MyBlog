using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.API.Models.Domain;
using MyBlog.API.Models.DTO;
using MyBlog.API.Repositories.Implementation;
using MyBlog.API.Repositories.Interface;
using System.Data;

namespace MyBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        public BlogPostController(IBlogPostRepository blogPostRepository, ICategoriesRepository categoriesRepository)
        {
            BlogPostRepository = blogPostRepository;
            CategoriesRepository = categoriesRepository;
        }

        public IBlogPostRepository BlogPostRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateBlogPost(blogPostRequestdto blogPost)
        {
            var BlogPost = new BlogPost
            {
                title = blogPost.title,
                short_description = blogPost.short_description,
                content = blogPost.content,
                featured_image_url = blogPost.featured_image_url,
                url_handle = blogPost.url_handle,
                author = blogPost.author,
                published_date = blogPost.published_date,
                is_visible = blogPost.is_visible,
                categories = new List<Category>()
            };

            foreach(var categoryGuid in blogPost.categories)
            {
                var existingcategory = await CategoriesRepository.getCategoryById(categoryGuid);
                if (existingcategory != null)
                {
                    BlogPost.categories.Add(existingcategory);
                }
            }

            BlogPost = await BlogPostRepository.createBlogpost(BlogPost);

            var response = new blogPostResponsedto
            {
                author = BlogPost.author,
                id = BlogPost.id,
                title = BlogPost.title,
                short_description = BlogPost.short_description,
                content = BlogPost.content,
                featured_image_url = BlogPost.featured_image_url,
                url_handle = BlogPost.url_handle,
                published_date = BlogPost.published_date,
                is_visible = BlogPost.is_visible,
                categories = BlogPost.categories.Select(x => new CategoryResponse
                {
                    id = x.id,
                    name = x.name,
                    url_handle = x.url_handle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> getAllBlogPost()
        {
            var blogPosts = await BlogPostRepository.getAllBlogPost();

            var blogPostdto = new List<blogPostResponsedto>();
            foreach (var blogPost in blogPosts)
            {
                blogPostdto.Add(new blogPostResponsedto
                {
                    author = blogPost.author,
                    id = blogPost.id,
                    title = blogPost.title,
                    short_description = blogPost.short_description,
                    content = blogPost.content,
                    featured_image_url = blogPost.featured_image_url,
                    url_handle = blogPost.url_handle,
                    published_date = blogPost.published_date,
                    is_visible = blogPost.is_visible,
                    categories = blogPost.categories.Select(x => new CategoryResponse
                    {
                        id = x.id,
                        name = x.name,
                        url_handle = x.url_handle
                    }).ToList()
                }) ;
            }

            return Ok(blogPostdto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getBlogPostbyID(Guid id)
        {
            var blogPost = await BlogPostRepository.getBlogPostbyID(id);
            if (blogPost is null)
            {
                return NotFound();
            }

            var blogPostFound = new blogPostResponsedto
            {
                author = blogPost.author,
                id = blogPost.id,
                title = blogPost.title,
                short_description = blogPost.short_description,
                content = blogPost.content,
                featured_image_url = blogPost.featured_image_url,
                url_handle = blogPost.url_handle,
                published_date = blogPost.published_date,
                is_visible = blogPost.is_visible,
                categories = blogPost.categories.Select(x => new CategoryResponse
                {
                    id = x.id,
                    name = x.name,
                    url_handle = x.url_handle
                }).ToList()
            };
            return Ok(blogPostFound);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> getBlogPostbyID(string urlHandle)
        {
            var blogPost = await BlogPostRepository.getBlogPostbyurlhandle(urlHandle);
            if (blogPost is null)
            {
                return NotFound();
            }

            var blogPostFound = new blogPostResponsedto
            {
                author = blogPost.author,
                id = blogPost.id,
                title = blogPost.title,
                short_description = blogPost.short_description,
                content = blogPost.content,
                featured_image_url = blogPost.featured_image_url,
                url_handle = blogPost.url_handle,
                published_date = blogPost.published_date,
                is_visible = blogPost.is_visible,
                categories = blogPost.categories.Select(x => new CategoryResponse
                {
                    id = x.id,
                    name = x.name,
                    url_handle = x.url_handle
                }).ToList()
            };
            return Ok(blogPostFound);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, updateBlogPostdto request)
        {
            var blogPostRequest = new BlogPost
            {
                id = id,
                title = request.title,
                short_description = request.short_description,
                content = request.content,
                featured_image_url = request.featured_image_url,
                url_handle = request.url_handle,
                author = request.author,
                published_date = request.published_date,
                is_visible = request.is_visible,
                categories = new List<Category>()
            };

            foreach (var category in request.categories)
            {
                var existingCategory = await CategoriesRepository.getCategoryById(category);
                if (existingCategory != null)
                {
                    blogPostRequest.categories.Add(existingCategory);
                }
            }

            var blogPostUpdateResponse = await BlogPostRepository.updateBlogPost(blogPostRequest);
            if(blogPostUpdateResponse != null)
            {
                var blogPostResponse = new blogPostResponsedto
                {
                    author = blogPostUpdateResponse.author,
                    id = blogPostUpdateResponse.id,
                    title = blogPostUpdateResponse.title,
                    short_description = blogPostUpdateResponse.short_description,
                    content = blogPostUpdateResponse.content,
                    featured_image_url = blogPostUpdateResponse.featured_image_url,
                    url_handle = blogPostUpdateResponse.url_handle,
                    published_date = blogPostUpdateResponse.published_date,
                    is_visible = blogPostUpdateResponse.is_visible,
                    categories = blogPostUpdateResponse.categories.Select(x => new CategoryResponse
                    {
                        id = x.id,
                        name = x.name,
                        url_handle = x.url_handle
                    }).ToList()
                };

                return Ok(blogPostResponse);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPostbyID([FromRoute] Guid id)
        {
            var BlogPost = await BlogPostRepository.DeleteBlogpostByID(id);
            if (BlogPost != null)
            {
                var deletedBlodPostDetails = new blogPostResponsedto
                {
                    author = BlogPost.author,
                    id = BlogPost.id,
                    title = BlogPost.title,
                    short_description = BlogPost.short_description,
                    content = BlogPost.content,
                    featured_image_url = BlogPost.featured_image_url,
                    url_handle = BlogPost.url_handle,
                    published_date = BlogPost.published_date,
                    is_visible = BlogPost.is_visible,
                };
                return Ok(deletedBlodPostDetails);
            }
            return NotFound();
        }
    }
}
