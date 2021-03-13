using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Shared.Utilities.Results.Abstract;

namespace MyBlog.Services.Abstract
{
    public interface IPostService
    {
        Task<IDataResult<PostDto>> Get(int postId);
        Task<IDataResult<PostListDto>> GetAll();
        Task<IDataResult<PostListDto>> GetAllByNoneDeleted();
        Task<IDataResult<PostListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<PostListDto>> GetAllByCategory(int categoryId);
        Task<IResult> Add(PostAddDto postAddDto, string createdByName);
        Task<IResult> Update(PostUpdateDto postUpdateDto, string modifiedByName);
        Task<IResult> Delete(int postId, string modifiedByName);
        Task<IResult> HardDelete(int postId);
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByIsDeleted();
    }
}
