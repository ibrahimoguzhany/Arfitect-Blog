using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;

namespace ArfitectBlog.Mvc.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
