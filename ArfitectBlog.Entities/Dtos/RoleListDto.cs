using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;

namespace ArfitectBlog.Entities.Dtos
{
    public class RoleListDto
    {
        public IList<Role> Roles { get; set; }
    }
}
