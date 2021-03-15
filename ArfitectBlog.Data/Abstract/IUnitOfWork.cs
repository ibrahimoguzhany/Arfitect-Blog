using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArfitectBlog.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IPostRepository Posts { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        Task<int> SaveAsync();


    }
}
