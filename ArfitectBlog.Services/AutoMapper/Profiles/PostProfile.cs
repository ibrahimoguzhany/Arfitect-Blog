using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;
using AutoMapper;
using ArfitectBlog.Services.Concrete;

namespace ArfitectBlog.Services.AutoMapper.Profiles
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<PostAddDto, Post>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<PostUpdateDto, Post>().ForMember(dest=>dest.ModifiedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<Post, PostUpdateDto>();

        }
    }
}
