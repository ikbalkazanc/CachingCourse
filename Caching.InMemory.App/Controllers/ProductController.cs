using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caching.InMemory.App.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            _memoryCache.Set<string>("Time",DateTime.Now.ToString());
            return View();
        }
        public IActionResult Show()
        {
            ViewBag.Time = _memoryCache.Get<string>("Time");
            return View();
        }
    }
}
