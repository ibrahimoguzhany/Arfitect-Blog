using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Data.Abstract;
using MyBlog.Data.Concrete.EntityFramework.Contexts;
using MyBlog.Data.Concrete.EntityFramework.Repositories;

namespace MyBlog.Data.Concrete
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MyBlogContext _context;
        private EfPostRepository _postRepository;
        private EfCategoryRepository _categoryRepository;
        private EfCommentRepository _commentRepository;
        private EfUserRepository _userRepository;
        private EfRoleRepository _roleRepository;

        public UnitOfWork(MyBlogContext context)
        {
            _context = context;
        }

        

        public IPostRepository Posts => _postRepository ?? new EfPostRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);
        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);
        public IRoleRepository Roles => _roleRepository ?? new EfRoleRepository(_context);
        public IUserRepository Users => _userRepository ?? new EfUserRepository(_context);
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
