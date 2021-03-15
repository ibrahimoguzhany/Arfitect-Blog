using ArfitectBlog.Data.Abstract;
using ArfitectBlog.Data.Concrete.EntityFramework.Contexts;
using ArfitectBlog.Data.Concrete.EntityFramework.Repositories;
using System.Threading.Tasks;

namespace ArfitectBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArfitectBlogContext _context;
        private EfPostRepository _postRepository;
        private EfCategoryRepository _categoryRepository;
        private EfCommentRepository _commentRepository;

        public UnitOfWork(ArfitectBlogContext context)
        {
            _context = context;
        }

        public IPostRepository Posts => _postRepository ?? new EfPostRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);
        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
