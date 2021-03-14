using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBlog.Data.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities.Results.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using MyBlog.Shared.Utilities.Results.Concrete;

namespace MyBlog.Services.Concrete
{
    public class PostManager : ManagerBase, IPostService
    {


        public PostManager(IUnitOfWork unitOfWork, IMapper mapper) : base(mapper,unitOfWork)
        {

        }

        public async Task<IDataResult<PostDto>> GetAsync(int postId)
        {
            var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId, x => x.User, x => x.Category);
            if (post != null)
            {
                return new DataResult<PostDto>(ResultStatus.Success, new PostDto
                {
                    Post = post,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostDto>(ResultStatus.Error, "Boyle bir makale bulunamadi.", null);

        }

        public async Task<IDataResult<PostUpdateDto>> GetPostUpdateDtoAsync(int postId)
        {
            var result = await UnitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await UnitOfWork.Posts.GetAsync(c => c.Id == postId);
                var postUpdateDto = Mapper.Map<PostUpdateDto>(post);
                return new DataResult<PostUpdateDto>(ResultStatus.Success, postUpdateDto);
            }
            else
            {
                return new DataResult<PostUpdateDto>(ResultStatus.Error, "Böyle bir paylaşım bulunamadı.", null);
            }
        }

        public async Task<IDataResult<PostListDto>> GetAllAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(null, x => x.User, x => x.Category);
            if (posts.Count > -1)
            {
                return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
                {
                    Posts = posts,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostListDto>(ResultStatus.Error, "Makaleler bulunamadi.", null);
        }

        public async Task<IDataResult<PostListDto>> GetAllByNoneDeletedAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(x => !x.IsDeleted, ar => ar.User, ar => ar.Category);
            if (posts.Count > -1)
            {
                return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
                {
                    Posts = posts,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostListDto>(ResultStatus.Error, "Makaleler bulunamadi.", null);

        }

        public async Task<IDataResult<PostListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(x => !x.IsDeleted && x.IsActive, ar => ar.User,
                ar => ar.Category);
            if (posts.Count > -1)
            {
                return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
                {
                    Posts = posts,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostListDto>(ResultStatus.Error, "Makaleler bulunamadi.", null);
        }

        public async Task<IDataResult<PostListDto>> GetAllByCategoryAsync(int categoryId)
        {

            var result = await UnitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var posts = await UnitOfWork.Posts.GetAllAsync(
                    x => x.CategoryId == categoryId && !x.IsDeleted && x.IsActive, ar => ar.User, ar => ar.Category);
                if (posts.Count > -1)
                {
                    return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
                    {
                        Posts = posts,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<PostListDto>(ResultStatus.Error, "Makaleler bulunamadi.", null);
            }
            return new DataResult<PostListDto>(ResultStatus.Error, "Boyle bir kategori bulunamadi.", null);
        }

        public async Task<IResult> AddAsync(PostAddDto postAddDto, string createdByName, int userId)
        {
            var post = Mapper.Map<Post>(postAddDto);
            post.CreatedByName = createdByName;
            post.ModifiedByName = createdByName;
            post.UserId = userId;
            await UnitOfWork.Posts.AddAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{postAddDto.Title} baslikli makale basariyla eklenmistir.");
        }

        public async Task<IResult> UpdateAsync(PostUpdateDto postUpdateDto, string modifiedByName)
        {
            var oldPost = await UnitOfWork.Posts.GetAsync(p => p.Id == postUpdateDto.Id);
            var post = Mapper.Map<PostUpdateDto, Post>(postUpdateDto, oldPost);
            post.ModifiedByName = modifiedByName;
            await UnitOfWork.Posts.UpdateAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{postUpdateDto.Title} baslikli makale basariyla guncellenmistir");
        }

        public async Task<IResult> DeleteAsync(int postId, string modifiedByName)
        {
            var result = await UnitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId);
                post.ModifiedByName = modifiedByName;
                post.IsDeleted = true;
                post.ModifiedDate = DateTime.Now;
                await UnitOfWork.Posts.UpdateAsync(post);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{post.Title} baslikli makale basariyla guncellenmistir");

            }
            return new Result(ResultStatus.Error, "Boyle bir makale bulunamadi");
        }

        public async Task<IResult> HardDeleteAsync(int postId)
        {
            var result = await UnitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId);

                await UnitOfWork.Posts.DeleteAsync(post);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{post.Title} baslikli makale veritabanindan basariyla silinmistir.");
            }
            return new Result(ResultStatus.Error, "Boyle bir makale bulunamadi");
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var postsCount = await UnitOfWork.Posts.CountAsync();
            if (postsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, postsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var postsCount = await UnitOfWork.Posts.CountAsync(a => !a.IsDeleted);
            if (postsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, postsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }
    }
}
