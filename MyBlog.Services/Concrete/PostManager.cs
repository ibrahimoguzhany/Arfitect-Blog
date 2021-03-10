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
    public class PostManager : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<PostDto>> Get(int postId)
        {
            var post = await _unitOfWork.Posts.GetAsync(x => x.Id == postId, x => x.User, x => x.Category);
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

        public async Task<IDataResult<PostListDto>> GetAll()
        {
            var posts = await _unitOfWork.Posts.GetAllAsync(null, x => x.User, x => x.Category);
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

        public async Task<IDataResult<PostListDto>> GetAllByNoneDeleted()
        {
            var posts = await _unitOfWork.Posts.GetAllAsync(x => !x.IsDeleted, ar => ar.User, ar => ar.Category);
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

        public async Task<IDataResult<PostListDto>> GetAllByNonDeletedAndActive()
        {
            var posts = await _unitOfWork.Posts.GetAllAsync(x => !x.IsDeleted && x.IsActive, ar => ar.User,
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

        public async Task<IDataResult<PostListDto>> GetAllByCategory(int categoryId)
        {

            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var posts = await _unitOfWork.Posts.GetAllAsync(
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

        public async Task<IResult> Add(PostAddDto postAddDto, string createdByName)
        {
            var post = _mapper.Map<Post>(postAddDto);
            post.CreatedByName = createdByName;
            post.ModifiedByName = createdByName;
            post.UserId = 1;
            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{postAddDto.Title} baslikli makale basariyla eklenmistir.");
        }

        public async Task<IResult> Update(PostUpdateDto postUpdateDto, string modifiedByName)
        {
            var post = _mapper.Map<Post>(postUpdateDto);
            post.ModifiedByName = modifiedByName;
            await _unitOfWork.Posts.UpdateAsync(post);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{postUpdateDto.Title} baslikli makale basariyla guncellenmistir");
        }

        public async Task<IResult> Delete(int postId, string modifiedByName)
        {
            var result = await _unitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await _unitOfWork.Posts.GetAsync(x => x.Id == postId);
                post.ModifiedByName = modifiedByName;
                post.IsDeleted = true;
                post.ModifiedDate= DateTime.Now;
                await _unitOfWork.Posts.UpdateAsync(post);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{post.Title} baslikli makale basariyla guncellenmistir");

            }
            return new Result(ResultStatus.Error, "Boyle bir makale bulunamadi");
        }

        public async Task<IResult> HardDelete(int postId)
        {
            var result = await _unitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await _unitOfWork.Posts.GetAsync(x => x.Id == postId);
              
                await _unitOfWork.Posts.DeleteAsync(post);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{post.Title} baslikli makale veritabanindan basariyla silinmistir.");
            }
            return new Result(ResultStatus.Error, "Boyle bir makale bulunamadi");
        }
    }
}
