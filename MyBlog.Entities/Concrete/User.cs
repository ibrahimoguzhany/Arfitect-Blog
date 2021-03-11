using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyBlog.Shared.Entities.Abstract;

namespace MyBlog.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string Picture { get; set; }
        public ICollection<Post> Posts { get; set; }
        //sadsa


    }
}
