using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bloggy.Data.Interfaces;
using Bloggy.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace BloggyWebAssembly.Server.Controllers
{
    [ApiController, Route("[controller]")]
    public class BloggyApiController : ControllerBase
    {
        internal readonly IBloggyApi _api;

        public BloggyApiController(IBloggyApi api) => _api = api;

        #region BlogPost Methods
        [HttpGet, Route("BlogPosts")]
        public async Task<List<BlogPost>> GetBlogPostsAsync(int numberOfPosts, int startIndex) => await _api.GetBlogPostsAsync(numberOfPosts, startIndex);
        [HttpGet, Route("BlogPostCount")]
        public async Task<int> GetBlogPostCountAsync() => await _api.GetBlogPostCountAsync();
        [HttpGet, Route("BlogPosts/{id}")]
        public async Task<BlogPost> GetBlogPostAsync(int id) => await _api.GetBlogPostAsync(id);
        [Authorize, HttpPut, Route("BlogPosts")]
        public async Task<BlogPost> SaveBlogPostAsync([FromBody] BlogPost item) => await _api.SaveBlogPostAsync(item);
        [Authorize, HttpDelete, Route("BlogPosts")]
        public async Task DeleteBlogPostAsync([FromBody] BlogPost item) => await _api.DeleteBlogPostAsync(item);
        #endregion BlogPost Methods

        #region Category Methods
        [HttpGet, Route("Categories")]
        public async Task<List<Category>> GetCategoriesAsync() => await _api.GetCategoriesAsync();
        [HttpGet, Route("Categories/{id}")]
        public async Task<Category> GetCategoryAsync(int id) => await _api.GetCategoryAsync(id);
        [Authorize, HttpPut, Route("Categories")]
        public async Task<Category> SaveCategoryAsync([FromBody] Category item) => await _api.SaveCategoryAsync(item);
        [Authorize, HttpDelete, Route("Categories")]
        public async Task DeleteCategoryAsync([FromBody] Category item) => await _api.DeleteCategoryAsync(item);
        #endregion Category Methods

        #region Tag Methods
        [HttpGet, Route("Tags")]
        public async Task<List<Tag>> GetTagsAsync() => await _api.GetTagsAsync();
        [HttpGet, Route("Tags/{id}")]
        public async Task<Tag> GetTagAsync(int id) => await _api.GetTagAsync(id);
        [Authorize, HttpPut, Route("Tags")]
        public async Task<Tag> SaveTagAsync([FromBody] Tag item) => await _api.SaveTagAsync(item);
        [Authorize, HttpDelete, Route("Tags")]
        public async Task DeleteTagAsync([FromBody] Tag item) => await _api.DeleteTagAsync(item);
        #endregion Tag Methods
    }
}
