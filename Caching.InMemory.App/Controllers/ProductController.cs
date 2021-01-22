using Caching.InMemory.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caching.InMemory.App.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        private ILogger<ProductController> _logger;
        public ProductController(IMemoryCache memoryCache,ILogger<ProductController> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }
        public IActionResult Index()
        {
            if(_memoryCache.TryGetValue("Time",out string timecache)){
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                //options.AbsoluteExpiration = DateTime.Now.AddSeconds(100);
                //options.SlidingExpiration = TimeSpan.FromSeconds(10);
                options.Priority = CacheItemPriority.High;

                options.RegisterPostEvictionCallback((key,value,reason,state)=>
                {
                    _logger.LogInformation("callback",$"{key}->{value} (Reason:{reason})");
                });
               
                _memoryCache.Set<string>("Time", DateTime.Now.ToString(),options);
                
            }
            Product prdt = new Product { Id = 1, Name = "Zımba" };
            _memoryCache.Set<Product>("product:1", prdt);
            return View();
        }
        public IActionResult Show()
        {
            
            ViewBag.Time = _memoryCache.GetOrCreate<string>("Time",entry => {
                return DateTime.Now.ToString();
            });
            ViewBag.product = _memoryCache.Get<Product>("product:1");
   
            //_memoryCache.Remove("Time");
            return View();
        }
    }
}
