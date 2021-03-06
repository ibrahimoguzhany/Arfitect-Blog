using System.Threading.Tasks;
using ArfitectBlog.Data.Abstract;
using ArfitectBlog.Data.Concrete.EntityFramework.Contexts;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ArfitectBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        }
        private ArfitectBlogContext ArfitectBlogContext
        {
            get
            {
                return _context as ArfitectBlogContext;
            }
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await ArfitectBlogContext.Categories.SingleOrDefaultAsync(x => x.Id == categoryId);
        }


    }
}
