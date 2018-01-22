
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChatTest.Controllers
{
    public class WeChatController : Controller
    {
        string WXAPPID = "wx9fc5a12da71e39b6";
        string WXAPPSECRET = "b339ac0d0b776e81cc9bc2b49fe026d4";

        string OPENAPPID = "wx3b60f7793a678151";
        string OPENAPPSECRET = "5cdb18ba9730dadfa62067bac6b99252";

        string CALLBACK_URL = "http://10086cc5.nat123.cc/WeChat/IsBind";
        public string GetQrCodeUrl()
        {
            string url = @"https://open.weixin.qq.com/connect/qrconnect?appid="+OPENAPPID+"&redirect_uri=" + CALLBACK_URL + "&response_type=code&scope=snsapi_login&state=STATE#wechat_redirect";
            return url;
        }
        public ActionResult Index()
        {
            return View();
        }
        public string Verification()
        {
            string echostr = Request["echostr"];
            return echostr;
        }
        public string ShowCode()
        {
            return Request["code"];
        }
        public string IsBind()
        {
            string code = Request["code"];
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + OPENAPPID + "&secret=" + OPENAPPSECRET + "&code=" + code + "&grant_type=authorization_code";
            var json=YZL.Code.WebClientHelper.GetJson(url);
            return json;
            //{"access_token":"6_N9JWt9ZLF436SypVUm5RHbHuXEF3q0y8T7FuS6m0jcybiYe9Ak30kFN1FhSDNY6YRkWMhGFidqiLhXXMYya9HA","expires_in":7200,"refresh_token":"6_eiLYlUDxLteqGj75YLaKp1tRahPr8RT0zRFteCZr2lg3XTHaxLWWSXBJA59C6WpdpH57DTpCgtH0QYfLxsTA7w","openid":"oytpc1iyCUIyyRmwQ3YyOKjy7zb0","scope":"snsapi_login","unionid":"olXI31FSDlXFyCW-vxjtLNYVDRJc"}
        }
        public ActionResult LoginOk()
        {
            return View();
        }
    }
}