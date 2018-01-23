using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace YZL.Code.WeChat
{
    public class MenuManager
    {
        private string access_token;

        public MenuManager()
        {
            access_token=new AccessToken().Get();
        }
        /// <summary>
        /// 获取现有菜单json
        /// </summary>
        /// <returns></returns>
        public string GetMenuJson()
        {
            string url = $"https://api.weixin.qq.com/cgi-bin/menu/get?access_token={access_token}";
            string json = WebClientHelper.GetJson(url);
            return json;
        }
        /// <summary>
        /// 设置新菜单json
        /// </summary>
        /// <param name="menuJson">新菜单json</param>
        /// <returns>更新后的菜单json</returns>
        public string SetMenuJson(string menuJson)
        {
            bool isOk = true;
            //写入新菜单
            string createUrl = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={access_token}";
            string createJson = WebClientHelper.PostJson(createUrl, menuJson);
            JObject createJObj = JObject.Parse(createJson);
            if (createJObj["errcode"].ToString() == "0" && createJObj["errmsg"].ToString() == "ok")
            {
                //查询现有菜单json
                string returnJson = GetMenuJson();
                return returnJson;
            }
            else
            {
                isOk = false;
            }
            return "";
        }
    }
}
