using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Bloggy.Data.Interfaces;
using Bloggy.Data.Models;

namespace Bloggy.Data
{
    public class BloggyApiServerSide : IBloggyApi
    {
        private readonly IDbContextFactory<BloggyDBContext> _factory;

        public BloggyApiServerSide(IDbContextFactory<BloggyDBContext> factory)
        {
            _factory = factory;
        }

        public async Task<BlogPost> GetBlogPostAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Posts.Include(p => p.Category).Include(p => p.Tags).FirstOrDefaultAsync(p => p.ID == id);
        }
        public async Task<int> GetBlogPostCountAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Posts.CountAsync();
        }
        public async Task<List<BlogPost>> GetBlogPostsAsync(int numberOfPosts, int startIndex)
        {
            using var context = _factory.CreateDbContext();
            return await context.Posts.OrderByDescending(p => p.PublishDate).Skip(startIndex).Take(numberOfPosts).ToListAsync();
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Categories.Include(p => p.Posts).FirstOrDefaultAsync(c => c.ID == id);
        }
        public async Task<Tag> GetTagAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Tags.Include(p => p.Posts).FirstOrDefaultAsync(c => c.ID == id);
        }
        public async Task<List<Tag>> GetTagsAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Tags.ToListAsync();
        }
        public async Task DeleteBlogPostAsync(BlogPost item) => await DeleteItem(item);
        public async Task DeleteCategoryAsync(Category item) => await DeleteItem(item);
        public async Task DeleteTagAsync(Tag item) => await DeleteItem(item);
        public async Task<BlogPost> SaveBlogPostAsync(BlogPost item) => (await SaveItem(item)) as BlogPost;
        public async Task<Category> SaveCategoryAsync(Category item) => (await SaveItem(item)) as Category;
        public async Task<Tag> SaveTagAsync(Tag item) => (await SaveItem(item)) as Tag;


        private async Task DeleteItem(IBloggyItem item)
        {
            using var context = _factory.CreateDbContext();
            context.Remove(item);
            await context.SaveChangesAsync();
        }
        private async Task<IBloggyItem> SaveItem(IBloggyItem item)
        {
            using var context = _factory.CreateDbContext();
            if (item.ID == 0)
            {
                context.Add(item);
            }
            else
            {
                if (item is BlogPost)
                {
                    var post = item as BlogPost;
                    var currentPost = await context.Posts.Include(p => p.Category).Include(p => p.Tags).FirstOrDefaultAsync(p => p.ID == post.ID);
                    currentPost.PublishDate = post.PublishDate;
                    currentPost.Title = post.Title;
                    currentPost.Text = post.Text;
                    var ids = post.Tags.Select(t => t.ID);
                    currentPost.Tags = context.Tags.Where(t => ids.Contains(t.ID)).ToList();
                    currentPost.Category = await context.Categories.FirstOrDefaultAsync(c => c.ID == post.Category.ID);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Entry(item).State = EntityState.Modified;
                }
            }
            await context.SaveChangesAsync();
            return item;
        }
    }
}