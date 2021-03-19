using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Mvc.Areas.Admin.Models;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Shared.Utilities.Helpers.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace ArfitectBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OptionsController : Controller
    {
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;
        private readonly IToastNotification _toastNotification;
        private readonly WebsiteInfo _websiteInfo;
        private readonly IWritableOptions<WebsiteInfo> _websiteInfoWriter;
        private readonly SmtpSettings _smtpSettings;
        private readonly IWritableOptions<SmtpSettings> _smtpSettingsWriter;
        private readonly PostRightSideBarWidgetOptions _postRightSideBarWidgetOptions;
        private readonly ICategoryService _categoryService;
        private readonly IWritableOptions<PostRightSideBarWidgetOptions> _postRightSideBarWidWritableOptionsWriter;
        private readonly IMapper _mapper;
       

        public OptionsController(IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter, IToastNotification toastNotification, IOptionsSnapshot<WebsiteInfo> websiteInfo, IWritableOptions<WebsiteInfo> websiteInfoWriter, IOptionsSnapshot<SmtpSettings> smtpSettings, IWritableOptions<SmtpSettings> smtpSettingsWriter, IOptionsSnapshot<PostRightSideBarWidgetOptions> postRightSideBarWidgetOptions, IWritableOptions<PostRightSideBarWidgetOptions> postRightSideBarWidWritableOptionsWriter, ICategoryService categoryService, IMapper mapper)
        {
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
            _toastNotification = toastNotification;
            _websiteInfoWriter = websiteInfoWriter;
            _smtpSettingsWriter = smtpSettingsWriter;
            _postRightSideBarWidWritableOptionsWriter = postRightSideBarWidWritableOptionsWriter;
            _categoryService = categoryService;
            _mapper = mapper;
            _postRightSideBarWidgetOptions = postRightSideBarWidgetOptions.Value;
            _smtpSettings = smtpSettings.Value;
            _websiteInfo = websiteInfo.Value;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }

        [HttpPost]
        public IActionResult About(AboutUsPageInfo aboutUsPageInfo)
        {
            if (ModelState.IsValid)
            {
                _aboutUsPageInfoWriter.Update(x =>
                {
                    x.Header = aboutUsPageInfo.Header;
                    x.Content = aboutUsPageInfo.Content;
                    x.SeoAuthor = aboutUsPageInfo.SeoAuthor;
                    x.SeoDescription = aboutUsPageInfo.SeoDescription;
                    x.SeoTags = aboutUsPageInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Hakkımızda Sayfa İçerikleri Başarıyla Güncellenmiştir.", new ToastrOptions()
                {
                    Title = "Başarılı İşlem"
                });
                return View(aboutUsPageInfo);
            }
            return View(aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult GeneralSettings()
        {
            return View(_websiteInfo);
        }

        [HttpPost]
        public IActionResult GeneralSettings(WebsiteInfo websiteInfo)
        {
            if (ModelState.IsValid)
            {
                _websiteInfoWriter.Update(x =>
                {
                    x.Title = websiteInfo.Title;
                    x.MenuTitle = websiteInfo.MenuTitle;
                    x.SeoAuthor = websiteInfo.SeoAuthor;
                    x.SeoDescription = websiteInfo.SeoDescription;
                    x.SeoTags = websiteInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin genel ayarlari basariyla guncellenmistir.", new ToastrOptions()
                {
                    Title = "Başarılı İşlem"
                });
                return View(websiteInfo);
            }
            return View(websiteInfo);
        }

        [HttpGet]
        public IActionResult EmailSettings()
        {
            return View(_smtpSettings);
        }

        [HttpPost]
        public IActionResult EmailSettings(SmtpSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                _smtpSettingsWriter.Update(x =>
                {
                    x.Server = smtpSettings.Server;
                    x.Port = smtpSettings.Port;
                    x.SenderName = smtpSettings.SenderName;
                    x.SenderEmail = smtpSettings.SenderEmail;
                    x.Username = smtpSettings.Username;
                    x.Password = smtpSettings.Password;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin e-posta ayarlari basariyla guncellenmistir.", new ToastrOptions()
                {
                    Title = "Başarılı İşlem"
                });
                return View(smtpSettings);
            }
            return View(smtpSettings);
        }

        [HttpGet]
        public async Task<IActionResult> PostRightSideBarWidgetSettings()
        {
            var categoriesResult = await _categoryService.GetAllByNoneDeletedAndActiveAsync();
            var postRightSideBarWidgetsOptionsViewModel = _mapper.Map<PostRightSideBarWidgetOptionsViewModel>(_postRightSideBarWidgetOptions);
            postRightSideBarWidgetsOptionsViewModel.Categories = categoriesResult.Data.Categories;
            return View(postRightSideBarWidgetsOptionsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostRightSideBarWidgetSettings(PostRightSideBarWidgetOptionsViewModel postRightSideBarWidgetOptionsViewModel)
        {
            var categoriesResult = await _categoryService.GetAllByNoneDeletedAndActiveAsync();
            postRightSideBarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;
            if (ModelState.IsValid)
            {
                _postRightSideBarWidWritableOptionsWriter.Update(x =>
                {
                    x.Header = postRightSideBarWidgetOptionsViewModel.Header;
                    x.TakeSize = postRightSideBarWidgetOptionsViewModel.TakeSize;
                    x.CategoryId = postRightSideBarWidgetOptionsViewModel.CategoryId;
                    x.FilterBy = postRightSideBarWidgetOptionsViewModel.FilterBy;
                    x.OrderBy = postRightSideBarWidgetOptionsViewModel.OrderBy;
                    x.IsAscending = postRightSideBarWidgetOptionsViewModel.IsAscending;
                    x.StartAt = postRightSideBarWidgetOptionsViewModel.StartAt;
                    x.EndAt = postRightSideBarWidgetOptionsViewModel.EndAt;
                    x.MaxViewCount = postRightSideBarWidgetOptionsViewModel.MaxViewCount;
                    x.MinViewCount = postRightSideBarWidgetOptionsViewModel.MinViewCount;
                    x.MaxCommentCount = postRightSideBarWidgetOptionsViewModel.MaxCommentCount;
                    x.MinCommentCount = postRightSideBarWidgetOptionsViewModel.MinCommentCount;
                });
                _toastNotification.AddSuccessToastMessage("Paylaşım sayfalarınızın wifget ayarları başarıyla guncellenmistir.", new ToastrOptions()
                {
                    Title = "Başarılı İşlem"
                });
                return View(postRightSideBarWidgetOptionsViewModel);
            }
            return View(postRightSideBarWidgetOptionsViewModel);
        }
    }
}
