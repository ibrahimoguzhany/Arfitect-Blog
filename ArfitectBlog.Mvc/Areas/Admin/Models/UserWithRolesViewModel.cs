using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;

namespace ArfitectBlog.Mvc.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
