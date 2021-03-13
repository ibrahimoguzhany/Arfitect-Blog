using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Abstract;
using MyBlog.Data.Concrete.EntityFramework.Contexts;
using MyBlog.Entities.Concrete;
using MyBlog.Shared.Data.Concrete.EntityFramework;

namespace MyBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        }
        private MyBlogContext MyBlogContext
        {
            get
            {
                return _context as MyBlogContext;
            }
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await MyBlogContext.Categories.SingleOrDefaultAsync(x => x.Id == categoryId);
        }


    }
}
