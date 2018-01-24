using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YZL.Model.WeChat;

namespace YZL.Code.WeChat
{
    public class UserManagerOpen
    {
        private string access_token;
        public UserManagerOpen()
        {
            access_token = new AccessTokenOpen().Get();
        }
        /// <summary>
        /// 这个方法不能用 不管传入什么openid获取的都是最初授权access_token那个微信的信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string openid)
        {
            string getuserinfoUrl = $"https://api.weixin.qq.com/sns/userinfo?access_token={access_token}&openid={openid}";
            var json = YZL.Code.WebClientHelper.GetJson(getuserinfoUrl);
            UserInfo user = JsonConvert.DeserializeObject<UserInfo>(json);
            return user;
        }
    }
}
