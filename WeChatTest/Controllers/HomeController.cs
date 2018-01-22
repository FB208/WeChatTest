using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using YZL.Code;
using YZL.Code.WeChat;

namespace WeChatTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void UpdateFansList()
        {
            //string fansUrl = $"https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=6_8TJ_Q6AaZf33QvAwMpPcxpnkDlVyg-1_oXvuZCnCtMaBaIav892uEVmzqd1mILfD6-PwKhOEpQIW-3a8sCvTrMOL2lLSwwEkAFdmouABSFpkL0sfU1WkDvPCJRrdw_XYkroTboxG5Dd-ncp7EGPbAFAHJS";
            //string json = WebClientHelper.GetJson(fansUrl);
            //JObject jObj = JObject.Parse(json);
            var ss = new UserManager().GetUserInfos(new UserManager().GetFansOpenIdList());
        }
    }
}