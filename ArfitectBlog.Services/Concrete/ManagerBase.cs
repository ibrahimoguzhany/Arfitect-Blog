using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Data.Abstract;
using AutoMapper;

namespace ArfitectBlog.Services.Concrete
{
   public class ManagerBase
    {
        public ManagerBase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

    }
}
