using Caching.InMemory.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Caching.InMemory.App.Controllers
{
    public class RedisProductsController : Controller
    {
        private IDistributedCache _distributedCache;
        public RedisProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult SaveString()
        {
            DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
            opt.AbsoluteExpiration = DateTime.Now.AddSeconds(30);
            _distributedCache.SetString("name", "ikbal", opt);
            return RedirectToAction("Index");
        }
        public IActionResult SaveObject()
        {
            DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
            opt.AbsoluteExpiration = DateTime.Now.AddSeconds(30);
            Product product = new Product() { Id = 1, Name = "iko" };
            _distributedCache.SetString("product", JsonConvert.SerializeObject(product), opt);
            return RedirectToAction("Index");
        }
        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/toplanti.jpg");
            byte[] image = System.IO.File.ReadAllBytes(path);

            DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
            opt.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            _distributedCache.Set("picture", image, opt);

            byte[] newImage = _distributedCache.Get("picture");
            return File(image, "image/jpg");
        }
    }
}
