using Caching.InMemory.App.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caching.InMemory.App.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Set()
        {
            var db = _redisService.GetDb(0);
            db.StringSet("name", "pelin");
            return RedirectToAction("Index");
        }
        public IActionResult List()
        {
            var db = _redisService.GetDb(0);
            db.ListRightPush("list","selam1");
            db.ListRightPush("list", "selam2");
            return RedirectToAction("Index");
        }
    }
}
