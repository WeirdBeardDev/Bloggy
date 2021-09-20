using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggy.Data.Interfaces;
using System.Net.Http;
using Bloggy.Data.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Bloggy.Data.Extensions;
using Newtonsoft.Json;

namespace Bloggy.Data
{
    public class BloggyApiClientSide : IBloggyApi
    {
        private readonly IHttpClientFactory _factory;

        public BloggyApiClientSide(IHttpClientFactory factory) => _factory = factory;

        #region BlogPost Methods
        public async Task<BlogPost> GetBlogPostAsync(int id)
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<BlogPost>($"BloggyAPI/BlogPosts/{id}");
        }
        public async Task<int> GetBlogPostCountAsync()
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<int>("BloggyAPI/BlogPostCount");
        }
        public async Task<List<BlogPost>> GetBlogPostsAsync(int numberofposts, int startindex)
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<List<BlogPost>>($"BloggyAPI/BlogPosts?numberofposts={numberofposts}&startindex={startindex}");
        }
        public async Task<BlogPost> SaveBlogPostAsync(BlogPost item)
        {
            try
            {
                var httpClient = _factory.CreateClient("Authenticated");
                var response = await httpClient.PutAsJsonAsync<BlogPost>("BloggyAPI/BlogPosts", item);
                var json = await response.Content.ReadAsStringAsync();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }
        public async Task DeleteBlogPostAsync(BlogPost item)
        {
            try
            {
                var httpClient = _factory.CreateClient("Authenticated");
                await httpClient.DeleteAsJsonAsync<BlogPost>("BloggyAPI/BlogPosts", item);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }
        #endregion BlogPost Methods

        #region Category Methods
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var httpClient = _factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<List<Category>>($"BloggyAPI/Categories");
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            var httpClient = _factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<Category>($"BloggyAPI/Categories/{id}");
        }
        public async Task DeleteCategoryAsync(Category item)
        {
            try
            {
                var httpClient = _factory.CreateClient("Authenticated");
                await httpClient.DeleteAsJsonAsync<Category>("BloggyAPI/Categories", item);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }
        public async Task<Category> SaveCategoryAsync(Category item)
        {
            try
            {
                var httpClient = _factory.CreateClient("Authenticated");
                var response = await httpClient.PutAsJsonAsync<Category>("BloggyAPI/Categories", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Category>(json);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }
        #endregion Category Methods

        #region Tag Methods
        public async Task<Tag> GetTagAsync(int id)
        {
            var httpClient = _factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<Tag>($"BloggyAPI/Tags/{id}");
        }
        public async Task<List<Tag>> GetTagsAsync()
        {
            var httpClient = _factory.CreateClient("Public");
            return await httpClient.GetFromJsonAsync<List<Tag>>($"BloggyAPI/Tags");
        }
        public async Task DeleteTagAsync(Tag item)
        {
            try
            {
                var httpClient = _factory.CreateClient("Authenticated");
                await httpClient.DeleteAsJsonAsync<Tag>("BloggyAPI/Tags", item);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }
        public async Task<Tag> SaveTagAsync(Tag item)
        {
            try
            {
                var httpClient = _factory.CreateClient("Authenticated");
                var response = await httpClient.PutAsJsonAsync<Tag>("BloggyAPI/Tags", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Tag>(json);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }
        #endregion Tag Methods
    }
}
