using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Shared.Utilities.Results.Abstract;

namespace ArfitectBlog.Services.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto); 
        IResult SendContactEmail(EmailSendDto emailSendDto);
    }
}
