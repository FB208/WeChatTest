
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YZL.Code;
using YZL.Code.WeChat;
using YZL.Code.WeChat.Template;
using YZL.Models.WeChat;

namespace WeChatTest.Controllers
{
    public class WeChatController : Controller
    {
        string WXAPPID = "wx9fc5a12da71e39b6";
        string WXAPPSECRET = "b339ac0d0b776e81cc9bc2b49fe026d4";

        string OPENAPPID = "wx3b60f7793a678151";
        string OPENAPPSECRET = "5cdb18ba9730dadfa62067bac6b99252";

        string CALLBACK_URL = "http://10086cc5.nat123.cc/WeChat/GetThisUser";

        //我的个人信息
        //[{"subscribe":1,"openid":"ok7Pvv0bOpCqOp3jfVznkdcP1UGQ","nickname":"杨惠超","sex":1,"city":"河东","province":"天津","country":"中国","language":"zh_CN","headimgurl":"http://wx.qlogo.cn/mmopen/ajNVdqHZLLAs7JFeR2peoaXXA6nuq3icFfys5rInIvu5dsTkIpQLVDpdxd8ZWQndFrNHLbC1LOoZZhQeddWmLqQ/132","subscribe_time":1516689036,"unionid":"olXI31FSDlXFyCW-vxjtLNYVDRJc","remark":"","groupid":0,"tagid_list":[],"privilege":null,"privilege_":""}]
        private const string myOpenId = "ok7Pvv0bOpCqOp3jfVznkdcP1UGQ";
        public string GetQrCodeUrl()
        {
            string url = @"https://open.weixin.qq.com/connect/qrconnect?appid="+OPENAPPID+"&redirect_uri=" + CALLBACK_URL + "&response_type=code&scope=snsapi_login&state=STATE#wechat_redirect";
            Response.Redirect(url);
            return url;
        }
        public ActionResult Index()
        {
            return View();
        }
        public string Verification()
        {
            //此处应该有校验
            string echostr = Request["echostr"];


            // 将解析结果存储在HashMap中
            //Map<String, String> map = new HashMap<String, String>();
            // 从request中取得输入流
            using (Stream inputStream = Request.InputStream)
            {
                StreamReader reader = new StreamReader(inputStream);
                XmlDocument xml = new XmlDocument();
                xml.Load(reader);
                XmlElement root = xml.DocumentElement;
                ReceiveXmlPush model = new ReceiveXmlPush()
                {
                    ToUserName = root.GetElementsByTagName("ToUserName").Item(0).InnerText,
                    FromUserName =  root.GetElementsByTagName("FromUserName").Item(0)?.InnerText,
                    CreateTime = Convert.ToInt64(root.GetElementsByTagName("CreateTime").Item(0)?.InnerText),
                    MsgType = root.GetElementsByTagName("MsgType").Item(0)?.InnerText,
                    Event = root.GetElementsByTagName("Event").Item(0)?.InnerText,
                    EventKey= root.GetElementsByTagName("EventKey").Item(0)?.InnerText

                };
                var cacheCreateTime = SystemCacheHelp.GetCache(model.FromUserName);
                if (cacheCreateTime!=null&& cacheCreateTime.ToString()==model.CreateTime.ToString())
                {
                    //已经接收到相同请求并正在处理
                    return echostr;
                }
                else
                {
                    SystemCacheHelp.SetCache(model.FromUserName, model.CreateTime, new TimeSpan(0, 0, 1, 0));
                    switch (model.Event)
                    {
                        //关注
                        case "subscribe":
                        {
                            new EventManager().FollowEvent(model);
                        }break;
                        //取消关注
                        case "unsubscribe":
                        {
                            new EventManager().UnFollowEvent(model);
                        }break;
                        //点击菜单事件
                        case "CLICK":
                        {
                            //return "<?xml version=\"1.0\" encoding=\"gb2312\"?><xml><ToUserName><![CDATA[ok7Pvv0bOpCqOp3jfVznkdcP1UGQ]]></ToUserName><FromUserName><![CDATA[gh_e93c814ce835]]></FromUserName><CreateTime>1516694789</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[qqq]]></Content></xml>";
                            return new EventManager().ClickEvent(model);
                        }break;
                        default:break;
                                ;
                    }
                }
                


            }
            
            
            return echostr;
        }

        
        public string ShowCode()
        {
            return Request["code"];
        }
        public string IsBind()
        {
            string code = Request["code"];
            AccessTokenOpen open = new AccessTokenOpen(code);
            string access_token = open.Get();
            string openid = open.OpenID();
            
            string getuserinfoUrl = $"https://api.weixin.qq.com/sns/userinfo?access_token={access_token}&openid={openid}";
            var json2 = YZL.Code.WebClientHelper.GetJson(getuserinfoUrl);
            //JObject jObj = JObject.Parse(json);
            return json2;
            //{"access_token":"6_N9JWt9ZLF436SypVUm5RHbHuXEF3q0y8T7FuS6m0jcybiYe9Ak30kFN1FhSDNY6YRkWMhGFidqiLhXXMYya9HA","expires_in":7200,"refresh_token":"6_eiLYlUDxLteqGj75YLaKp1tRahPr8RT0zRFteCZr2lg3XTHaxLWWSXBJA59C6WpdpH57DTpCgtH0QYfLxsTA7w","openid":"oytpc1iyCUIyyRmwQ3YyOKjy7zb0","scope":"snsapi_login","unionid":"olXI31FSDlXFyCW-vxjtLNYVDRJc"}
        }

        public JsonResult GetThisUser()
        {
            string code = Request["code"];
            UserInfo user =new AccessTokenOpen(code).GetUserInfo();
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser()
        {
            string openid = "sdfw353rf";
            UserInfo user = new UserManagerOpen().GetUserInfo(openid);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoginOk()
        {
            return View();
        }

        public string SendMessage()
        {
            //var xdoc = new XDocument(new XElement("xml",
            //    new XElement("touser", new XText(myOpenId)),
            //    new XElement("template_id", new XText("ncyp1ir19iNPiyfswMw1KkZ7VYbA1Zbqe86bY_JTNPw")),
            //    new XElement("url", new XText("http://pctest.cloudhvacr.com")),
            //    new XElement("data", 
            //        new XElement("first",
            //            new XElement("value","假装有个报警"),
            //            new XElement("color", "#173177")),
            //        new XElement("keyword1",
            //            new XElement("value", "2018/01/30"),
            //            new XElement("color", "#173177")),
            //        new XElement("keyword2",
            //            new XElement("value", "实验室"),
            //            new XElement("color", "#173177")),
            //        new XElement("keyword3",
            //            new XElement("value", "测试设备"),
            //            new XElement("color", "#173177")),
            //        new XElement("keyword4",
            //            new XElement("value", "10-20"),
            //            new XElement("color", "#173177")),
            //        new XElement("keyword5",
            //            new XElement("value", "25"),
            //            new XElement("color", "#173177")),
            //        new XElement("remark",
            //            new XElement("value", "维修员：张三，联系方式：13811111111"),
            //            new XElement("color", "#173177"))
            //)));
            ncyp1ir19iNPiyfswMw1KkZ7VYbA1Zbqe86bY_JTNPw model = new ncyp1ir19iNPiyfswMw1KkZ7VYbA1Zbqe86bY_JTNPw();
            model.touser = myOpenId;
            model.template_id = "ncyp1ir19iNPiyfswMw1KkZ7VYbA1Zbqe86bY_JTNPw";
            model.url = "http://pctest.cloudhvacr.com";
            model.miniprogram = null;
            model.data=new Data()
            {
                first = new keyword() { value = "假装有个报警",color = "#173177" },
                //keyword1 = new keyword() { value = "2018/01/30", color = "#173177" },
                //keyword2 = new keyword() { value = "实验室", color = "#173177" },
                //keyword3 = new keyword() { value = "测试设备", color = "#173177" },
                //keyword4 = new keyword() { value = "10-20", color = "#173177" },
                //keyword5 = new keyword() { value = "25", color = "#173177" },
                remark = new keyword() { value = "维修员：张三，联系方式：13811111111", color = "#173177" },
            };
            string postJson = JsonConvert.SerializeObject(model);
            JObject jObj = JObject.Parse(postJson);
            jObj["data"]["first"].Parent.AddAfterSelf(new JProperty("keyword1", new JObject(new JProperty("value", "确定全文"), new JProperty("color", "#173177"))));
            jObj["data"]["first"].Parent.AddAfterSelf(new JProperty("keyword2", new JObject(new JProperty("value", "确定全文"), new JProperty("color", "#173177"))));
            var ss = jObj.ToString();
            string access_token=new AccessToken().Get();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={access_token}";
            string json = WebClientHelper.PostJson(url, postJson);
            return json;
        }
    }
}