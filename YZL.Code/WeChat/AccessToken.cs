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
        private string access_token;

        public AccessToken()
        {
            access_token = SystemCacheHelp.GetCache("AccessToken") + "";
            if (string.IsNullOrWhiteSpace(access_token))
            {
                string appid = ConfigHelper.GetConfigString("WXAPPID");
                string appsercret = ConfigHelper.GetConfigString("WXAPPSECRET");
                string url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={appsercret}";
                var json = WebClientHelper.GetJson(url);
                JObject jObj = JObject.Parse(json);
                JToken access_tokenJ = jObj["access_token"];
                access_token = access_tokenJ.ToString();
                SystemCacheHelp.SetCache("AccessToken", access_token, new TimeSpan(0, 0, int.Parse(jObj["expires_in"].ToString())));
            }
        }

        public string Get()
        {
            return access_token;
        }
    }
}
