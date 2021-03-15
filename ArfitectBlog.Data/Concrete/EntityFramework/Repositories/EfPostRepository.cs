using ArfitectBlog.Data.Abstract;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ArfitectBlog.Data.Concrete.EntityFramework.Repositories
{
    
    public class EfPostRepository : EfEntityRepositoryBase<Post>, IPostRepository
    {
        public EfPostRepository(DbContext context) : base(context)
        {

        }
    }
}
