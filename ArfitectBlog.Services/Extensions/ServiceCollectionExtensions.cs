using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Data.Abstract;
using ArfitectBlog.Data.Concrete;
using ArfitectBlog.Data.Concrete.EntityFramework.Contexts;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Services.Concrete;

namespace ArfitectBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<MyBlogContext>(options=>options.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                //User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // User Username and Email options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<MyBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IPostService, PostManager>();
            serviceCollection.AddScoped<ICommentService, CommentManager>();

            return serviceCollection;
        }

    }
}
