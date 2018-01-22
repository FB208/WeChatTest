using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YZL.Model.WeChat;

namespace YZL.Code.WeChat
{
    public class UserManager
    {
        private string access_token ;

        public UserManager()
        {
            access_token=new AccessToken().Get();
        }

        /// <summary>
        /// 取粉丝openids
        /// </summary>
        /// <returns></returns>
        public List<string> GetFansOpenIdList()
        {
            int total;
            int count;
            int historyCount = 0;
            string next_openid = "";
            List<string> openids=new List<string>();
            do
            {
                string fansUrl = $"https://api.weixin.qq.com/cgi-bin/user/get?access_token={access_token}&next_openid={next_openid}";
                string json = WebClientHelper.GetJson(fansUrl);
                JObject jObj = JObject.Parse(json);
                total = int.Parse(jObj["total"].ToString());
                count = int.Parse(jObj["count"].ToString());
                historyCount += count;
                List<string> openid = jObj["data"]["openid"].Select(m => m.ToString()).ToList();
                next_openid = jObj["next_openid"] + "";
                openids.AddRange(openid);
            } while (total> historyCount);
            return openids;
        }


        /// <summary>
        /// 批量取用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUserInfos(List<string> openids)
        {
            JArray jArray = new JArray();
            List<UserInfo> userinfos = new List<UserInfo>();
            int i = 0;
            do
            {
                int j;
                for (j = 0; j < 100; j++)
                {
                    if (i+j<openids.Count)
                    {
                        jArray.Add(new JObject(new JProperty("openid", openids[i + j]), new JProperty("lang", "zh_CN")));
                    }
                    else
                    {
                        break;
                    }
                    
                }
                i = i + j;
                JObject postJObj = new JObject(new JProperty("user_list", jArray));
                string userinfosUrl = $"https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={access_token}";
                string json = WebClientHelper.PostJson(userinfosUrl, postJObj.ToString());
                JObject jObj = JObject.Parse(json);
                List<UserInfo> temp_userinfos = new List<UserInfo>();
                for (int k = 0; k < jObj["user_info_list"].Count(); k++)
                {
                    UserInfo user = JsonConvert.DeserializeObject<UserInfo>(jObj["user_info_list"][k] + "");
                    temp_userinfos.Add(user);
                }
                
                
                userinfos.AddRange(temp_userinfos);
                jArray.Clear();
                
            } while (i<openids.Count);
            
            return userinfos;
        }
    }
}
