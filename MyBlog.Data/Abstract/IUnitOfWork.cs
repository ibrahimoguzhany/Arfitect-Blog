using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IPostRepository Posts { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }// _unitOfWork.Categories.AddAsync();
        // _unitOfWork.Users.AddAsync();
        // _unitOfWork.SaveAsync();
        Task<int> SaveAsync();


    }
}
