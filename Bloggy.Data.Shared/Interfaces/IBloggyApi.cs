using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggy.Data.Models;

namespace Bloggy.Data.Interfaces
{
    public interface IBloggyApi
    {
        Task<int> GetBlogPostCountAsync();
        Task<List<BlogPost>> GetBlogPostsAsync(int posts, int startIndex);
        Task<BlogPost> GetBlogPostAsync(int id);
        Task<BlogPost> SaveBlogPostAsync(BlogPost item);
        Task DeleteBlogPostAsync(BlogPost item);

        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<Category> SaveCategoryAsync(Category item);
        Task DeleteCategoryAsync(Category item);

        Task<List<Tag>> GetTagsAsync();
        Task<Tag> GetTagAsync(int id);
        Task<Tag> SaveTagAsync(Tag item);
        Task DeleteTagAsync(Tag item);
    }
}
