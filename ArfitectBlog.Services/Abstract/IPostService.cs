using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Shared.Utilities.Results.Abstract;
using ArfitectBlog.Entities.Concrete;

namespace ArfitectBlog.Services.Abstract
{
    public interface IPostService
    {
        Task<IDataResult<PostDto>> GetAsync(int postId);
        Task<IDataResult<PostUpdateDto>> GetPostUpdateDtoAsync(int postId);
        Task<IDataResult<PostListDto>> GetAllAsync();
        Task<IDataResult<PostListDto>> GetAllByNoneDeletedAsync();
        Task<IDataResult<PostListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<PostListDto>> GetAllByCategoryAsync(int categoryId);
        Task<IResult> AddAsync(PostAddDto postAddDto, string createdByName, int userId);
        Task<IResult> UpdateAsync(PostUpdateDto postUpdateDto, string modifiedByName);
        Task<IResult> DeleteAsync(int postId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int postId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
