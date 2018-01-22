using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChatTest.Controllers
{
    public class TModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            TModel model = new TModel() { Name = "123", Address = "456" };
            var ss = EasyDotNet.Utility.XmlHelper.Serializer(typeof(TModel), model);
            ViewBag.Message = ss;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}