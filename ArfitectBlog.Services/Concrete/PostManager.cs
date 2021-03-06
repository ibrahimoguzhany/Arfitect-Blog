using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Data.Abstract;
using ArfitectBlog.Entities.ComplexTypes;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Services.Utilities;
using ArfitectBlog.Shared.Utilities.Results.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;
using ArfitectBlog.Shared.Utilities.Results.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArfitectBlog.Services.Concrete
{
    public class PostManager : ManagerBase, IPostService
    {
        private readonly UserManager<User> _userManager;
        public PostManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(mapper, unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<IDataResult<PostDto>> GetAsync(int postId)
        {
            var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId, x => x.User, x => x.Category);
            if (post != null)
            {
                post.Comments = await UnitOfWork.Comments.GetAllAsync(c => c.PostId == postId && !c.IsDeleted && c.IsActive);
                return new DataResult<PostDto>(ResultStatus.Success, new PostDto
                {
                    Post = post,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostDto>(ResultStatus.Error, Messages.Post.NotFound(false), null);
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
                return new DataResult<PostUpdateDto>(ResultStatus.Error, Messages.Post.NotFound(false), null);
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
            return new DataResult<PostListDto>(ResultStatus.Error, Messages.Post.NotFound(true), null);
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
            return new DataResult<PostListDto>(ResultStatus.Error, Messages.Post.NotFound(true), null);
        }

        public async Task<IDataResult<PostListDto>> GetAllByDeletedAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(x => x.IsDeleted, p => p.User, p => p.Category);
            if (posts.Count > -1)
            {
                return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto()
                {
                    Posts = posts,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostListDto>(ResultStatus.Error, Messages.Post.NotFound(true), null);
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
            return new DataResult<PostListDto>(ResultStatus.Error, Messages.Post.NotFound(true), null);
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
                return new DataResult<PostListDto>(ResultStatus.Error, Messages.Post.NotFound(true), null);
            }
            return new DataResult<PostListDto>(ResultStatus.Error, Messages.Post.NotFound(true), null);
        }

        public async Task<IDataResult<PostListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize)
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(p => p.IsActive && !p.IsDeleted, x => x.Category,
                x => x.User);
            var sortedPosts = isAscending ? posts.OrderBy(p => p.ViewCount) : posts.OrderByDescending(p => p.ViewCount);
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto()
            {
                Posts = takeSize == null ? sortedPosts.ToList() : sortedPosts.Take(takeSize.Value).ToList()
            });
        }

        public async Task<IDataResult<PostListDto>> GetAllByPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var posts = categoryId == null
                ? await UnitOfWork.Posts.GetAllAsync(x => x.IsActive && !x.IsDeleted, x => x.Category, x => x.User)
                : await UnitOfWork.Posts.GetAllAsync(x => x.CategoryId == categoryId && x.IsActive && !x.IsDeleted, x => x.Category, x => x.User);
            var sortedPosts = isAscending
                ? posts.OrderBy(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : posts.OrderByDescending(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto()
            {
                Posts = sortedPosts,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = posts.Count,
                IsAscending = isAscending
            });
        }

        public async Task<IDataResult<PostListDto>> GetAllByUserIdOnFilter(int userId, FilterBy filterBy, OrderBy orderBy, bool isAscending, int takeSize,
            int categoryId, DateTime startAt, DateTime endAt, int minViewCount, int maxViewCount, int minCommentCount,
            int maxCommentCount)
        {
            var anyUser = await _userManager.Users.AnyAsync(u => u.Id == userId);
            if (!anyUser)
                return new DataResult<PostListDto>(ResultStatus.Error, $"{userId} id'li kullanıcı bulunamadı.", null);

            var userPosts = await UnitOfWork.Posts.GetAllAsync(p => p.IsActive && !p.IsDeleted && p.UserId == userId);
            List<Post> sortedPosts = new List<Post>();

            switch (filterBy)
            {
                case FilterBy.Category:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderBy(x => x.Date)
                                    .ToList()
                                : userPosts.Where(x => x.CategoryId == categoryId).Take(takeSize)
                                    .OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderBy(x => x.ViewCount)
                                    .ToList()
                                : userPosts.Where(x => x.CategoryId == categoryId).Take(takeSize)
                                    .OrderByDescending(x => x.ViewCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderBy(x => x.CommentCount)
                                    .ToList()
                                : userPosts.Where(x => x.CategoryId == categoryId).Take(takeSize)
                                    .OrderByDescending(x => x.CommentCount).ToList();
                            break;
                    }

                    break;
                case FilterBy.Date:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderBy(x => x.Date)
                                    .ToList()
                                : userPosts.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize)
                                    .OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderBy(x => x.ViewCount)
                                    .ToList()
                                : userPosts.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize)
                                    .OrderByDescending(x => x.ViewCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderBy(x => x.CommentCount)
                                    .ToList()
                                : userPosts.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize)
                                    .OrderByDescending(x => x.CommentCount).ToList();
                            break;

                    }
                    break;
                case FilterBy.ViewCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.ViewCount >= minViewCount && x.ViewCount <= maxViewCount).Take(takeSize).OrderBy(x => x.Date)
                                    .ToList()
                                : userPosts.Where(x => x.ViewCount >= minViewCount && x.ViewCount <= maxViewCount).Take(takeSize)
                                    .OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.ViewCount >= minViewCount && x.ViewCount <= maxViewCount).Take(takeSize).OrderBy(x => x.ViewCount)
                                    .ToList()
                                : userPosts.Where(x => x.ViewCount >= minViewCount && x.ViewCount <= maxViewCount).Take(takeSize)
                                    .OrderByDescending(x => x.ViewCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.ViewCount >= minViewCount && x.ViewCount <= maxViewCount).Take(takeSize).OrderBy(x => x.CommentCount)
                                    .ToList()
                                : userPosts.Where(x => x.ViewCount >= minViewCount && x.ViewCount <= maxViewCount).Take(takeSize)
                                    .OrderByDescending(x => x.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.CommentCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(x => x.Date)
                                    .ToList()
                                : userPosts.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize)
                                    .OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(x => x.ViewCount)
                                    .ToList()
                                : userPosts.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize)
                                    .OrderByDescending(x => x.ViewCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(x => x.CommentCount)
                                    .ToList()
                                : userPosts.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize)
                                    .OrderByDescending(x => x.CommentCount).ToList();
                            break;
                    }
                    break;
            }
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
            {
                Posts = sortedPosts
            });
        }

        public async Task<IDataResult<PostListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var posts = await UnitOfWork.Posts.GetAllAsync(x => x.IsActive && !x.IsDeleted, x => x.Category,
                    x => x.User);

                var sortedPosts = isAscending
                    ? posts.OrderBy(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                    : posts.OrderByDescending(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto()
                {
                    Posts = sortedPosts,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = posts.Count,
                    IsAscending = isAscending
                });
            }
            var searchPosts = await UnitOfWork.Posts.SearchAsync(new List<Expression<Func<Post, bool>>>()
            {
                (p)=>p.Title.Contains(keyword),
                (p)=>p.Category.Name.Contains(keyword),
                (p)=>p.SeoDescription.Contains(keyword),
                (p)=>p.SeoTags.Contains(keyword)
            }, p => p.Category, p => p.User);
            var searchedAndSortedPosts = isAscending
                ? searchPosts.OrderBy(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : searchPosts.OrderByDescending(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto()
            {
                Posts = searchedAndSortedPosts,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = searchPosts.Count,
                IsAscending = isAscending
            });
        }

        public async Task<IResult> IncreaseViewCountAsync(int postId)
        {
            var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId);
            if (post == null)
            {
                return new Result(ResultStatus.Error, Messages.Post.NotFound(false));
            }
            post.ViewCount += 1;
            await UnitOfWork.Posts.UpdateAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Post.IncreaseViewCount(post.Title));
        }

        public async Task<IResult> AddAsync(PostAddDto postAddDto, string createdByName, int userId)
        {
            var post = Mapper.Map<Post>(postAddDto);
            post.CreatedByName = createdByName;
            post.ModifiedByName = createdByName;
            post.UserId = userId;
            await UnitOfWork.Posts.AddAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Post.Add(post.Title));
        }

        public async Task<IResult> UpdateAsync(PostUpdateDto postUpdateDto, string modifiedByName)
        {
            var oldPost = await UnitOfWork.Posts.GetAsync(p => p.Id == postUpdateDto.Id);
            var post = Mapper.Map<PostUpdateDto, Post>(postUpdateDto, oldPost);
            post.ModifiedByName = modifiedByName;
            await UnitOfWork.Posts.UpdateAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Post.Update(post.Title));
        }

        public async Task<IResult> DeleteAsync(int postId, string modifiedByName)
        {
            var result = await UnitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId);
                post.ModifiedByName = modifiedByName;
                post.IsDeleted = true;
                post.IsActive = false;
                post.ModifiedDate = DateTime.Now;
                await UnitOfWork.Posts.UpdateAsync(post);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Post.Delete(post.Title));

            }
            return new Result(ResultStatus.Error, Messages.Post.NotFound(false));
        }

        public async Task<IResult> UndoDeleteAsync(int postId, string modifiedByName)
        {
            var result = await UnitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId);
                post.IsDeleted = false;
                post.IsActive = true;
                post.ModifiedByName = modifiedByName;
                post.ModifiedDate = DateTime.Now;
                await UnitOfWork.Posts.UpdateAsync(post);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Post.UndoDelete(post.Title));
            }

            return new Result(ResultStatus.Error, Messages.Post.NotFound(false));
        }

        public async Task<IResult> HardDeleteAsync(int postId)
        {
            var result = await UnitOfWork.Posts.AnyAsync(x => x.Id == postId);
            if (result)
            {
                var post = await UnitOfWork.Posts.GetAsync(x => x.Id == postId);

                await UnitOfWork.Posts.DeleteAsync(post);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Post.Delete(post.Title));
            }
            return new Result(ResultStatus.Error, Messages.Post.NotFound(false));
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
