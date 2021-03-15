using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Shared.Data.Abstract;

namespace ArfitectBlog.Data.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<Category> GetById(int categoryId);
    }
}
