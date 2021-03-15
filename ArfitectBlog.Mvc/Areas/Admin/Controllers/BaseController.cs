using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Mvc.Helpers.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ArfitectBlog.Mvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
        {
            UserManager = userManager;
            Mapper = mapper;
            ImageHelper = imageHelper;
        }

        protected UserManager<User> UserManager { get; }
        protected IMapper Mapper { get; }
        protected IImageHelper ImageHelper { get; }
        protected User LoggedInUser => UserManager.GetUserAsync(HttpContext.User).Result;

    }
}
