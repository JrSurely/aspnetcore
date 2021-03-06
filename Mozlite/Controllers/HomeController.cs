﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mozlite.Extensions.Security;
using Mozlite.Extensions.Storages;
using Mozlite.Models;
using Mozlite.Mvc.Themes;

namespace Mozlite.Controllers
{
    public class HomeController : Mozlite.Mvc.ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediaFileProvider _fileProvider;

        public HomeController(ILogger<HomeController> logger, IMediaFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModalTest(IFormCollection form)
        {
            return Error("你已经成功提交了信息：" + form["role"]);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult RWin()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _fileProvider.UploadAsync(file, SecuritySettings.ExtensionName, UserId);
            if (result.Succeeded)
                return Success(new {result.Url});
            return Error(result.Message);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ModalTest()
        {
            return View();
        }
    }
}
