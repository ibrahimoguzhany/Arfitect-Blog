using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;
using System.Threading.Tasks;

namespace ArfitectBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;

        public HomeController(IPostService postService, IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IMailService mailService, IToastNotification toastNotification)
        {
            _postService = postService;
            _mailService = mailService;
            _toastNotification = toastNotification;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var postsResult = await (categoryId == null
                ? _postService.GetAllByPagingAsync(null, currentPage, pageSize, isAscending)
                : _postService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize, isAscending));
            return View(postsResult.Data);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            
            if (ModelState.IsValid)
            {
                var result = _mailService.SendContactEmail(emailSendDto);
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions()
                {
                    Title = "Basarili islem"
                });
                return View();
            }
            return View(emailSendDto);
        }
    }
}
