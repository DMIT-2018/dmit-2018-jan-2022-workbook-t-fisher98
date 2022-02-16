﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        #region Private Variable and DI Constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly AboutServices _aboutServices;

        public IndexModel(ILogger<IndexModel> logger, AboutServices aboutServices)
        {
            _logger = logger;
            _aboutServices = aboutServices;
        }
        #endregion

        #region FeedBack and ErrorHandling
        [TempData]
        public string FeedBack { get; set; }
        public bool HasFeedBack => !string.IsNullOrWhiteSpace(FeedBack);
        #endregion
        public void OnGet()
        {
            // Consume a service
            DbVersionInfo info = _aboutServices.GetDbVersion();

            if (info == null)
            {
                FeedBack = "Version unknown";
                return;
            }
            
            FeedBack = $"Version: {info.Major}.{info.Minor}.{info.Build}" + $" Release date of {info.ReleaseDate.ToShortDateString()}";
        }
    }
}