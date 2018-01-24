using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YZL.Model.WeChat;

namespace YZL.Code.WeChat
{
    public class AccessTokenOpen
    {
        private string access_token;
        private string openid;
        private string unionid;

        public AccessTokenOpen()
        {
            access_token = SystemCacheHelp.GetCache("AccessTokenOpen") + "";
            if (string.IsNullOrWhiteSpace(access_token))
            {
                string appid = ConfigHelper.GetConfigString("OPENAPPID");
                string refresh_token = SystemCacheHelp.GetCache("RefreshToken") + "";
                //重新扫码获取授权
                if (string.IsNullOrWhiteSpace(refresh_token))
                {
                    //没有code不能重新取
                }
                else//用refresh_token刷新授权
                {
                    string url = $"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={appid}&grant_type=refresh_token&refresh_token={refresh_token}";
                    var json = YZL.Code.WebClientHelper.GetJson(url);
                    JObject jObj = JObject.Parse(json);
                    access_token = jObj["access_token"] + "";
                    openid = jObj["openid"] + "";
                    refresh_token = jObj["refresh_token"] + "";
                    SystemCacheHelp.SetCache("RefreshToken", refresh_token, new TimeSpan(29, 23, 0, 0));
                    SystemCacheHelp.SetCache("AccessToken", access_token, new TimeSpan(0, 0, int.Parse(jObj["expires_in"].ToString())));
                }
            }
        }

        public AccessTokenOpen(string code)
        {
            access_token = SystemCacheHelp.GetCache("AccessTokenOpen") + "";
            if (string.IsNullOrWhiteSpace(access_token))
            {
                string appid = ConfigHelper.GetConfigString("OPENAPPID");
                string appsercret = ConfigHelper.GetConfigString("OPENAPPSECRET");
                string refresh_token = SystemCacheHelp.GetCache("RefreshToken") + "";
                //重新扫码获取授权
                if (string.IsNullOrWhiteSpace(refresh_token))
                {
                    
                    string url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={appid}&secret={appsercret}&code={code}&grant_type=authorization_code";
                    var json = YZL.Code.WebClientHelper.GetJson(url);
                    JObject jObj = JObject.Parse(json);
                    access_token = jObj["access_token"] + "";
                    openid = jObj["openid"] + "";
                    unionid = jObj["unionid"] + "";
                    refresh_token = jObj["refresh_token"] + "";
                    SystemCacheHelp.SetCache("RefreshToken", refresh_token, new TimeSpan(29, 23, 0, 0));
                    SystemCacheHelp.SetCache("AccessToken", access_token, new TimeSpan(0, 0, int.Parse(jObj["expires_in"].ToString())));
                }
                else//用refresh_token刷新授权
                {
                    string url = $"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={appid}&grant_type=refresh_token&refresh_token={refresh_token}";
                    var json = YZL.Code.WebClientHelper.GetJson(url);
                    JObject jObj = JObject.Parse(json);
                    access_token = jObj["access_token"] + "";
                    openid = jObj["openid"] + "";
                    refresh_token = jObj["refresh_token"] + "";
                    SystemCacheHelp.SetCache("RefreshToken", refresh_token, new TimeSpan(29, 23, 0, 0));
                    SystemCacheHelp.SetCache("AccessToken", access_token, new TimeSpan(0, 0, int.Parse(jObj["expires_in"].ToString())));
                }
                
            }
        }

        public string Get()
        {
            return access_token;
        }

        public string OpenID()
        {
            return openid;
        }

        public string UnionID()
        {
            return unionid;
        }
        /// <summary>
        /// 取到当时授权扫码的用户
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserInfo()
        {
            string getuserinfoUrl = $"https://api.weixin.qq.com/sns/userinfo?access_token={access_token}&openid={openid}";
            var json = YZL.Code.WebClientHelper.GetJson(getuserinfoUrl);
            UserInfo user = JsonConvert.DeserializeObject<UserInfo>(json);
            return user;
        }
    }
}
