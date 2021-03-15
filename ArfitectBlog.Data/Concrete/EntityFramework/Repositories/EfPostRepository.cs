using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Shared.Data.Concrete.EntityFramework;

namespace MyBlog.Data.Concrete.EntityFramework.Repositories
{
    
    public class EfPostRepository : EfEntityRepositoryBase<Post>, IPostRepository
    {
        public EfPostRepository(DbContext context) : base(context)
        {

        }
    }
}
