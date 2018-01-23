using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace YZL.Code.WeChat
{
    public class AccessToken
    {
        string WXAPPID = ConfigHelper.GetConfigString("WXAPPID");
        string WXAPPSECRET = ConfigHelper.GetConfigString("WXAPPSECRET");
        public string Get()
        {
            string url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={WXAPPID}&secret={WXAPPSECRET}";
            string json = WebClientHelper.GetJson(url);
            JObject jObj=JObject.Parse(json);
            string access_token = jObj["access_token"].ToString();
            return access_token;
        }
    }
}
