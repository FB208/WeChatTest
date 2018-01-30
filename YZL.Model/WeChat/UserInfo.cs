using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace YZL.Models.WeChat
{
    public class UserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息
        /// </summary>
        public int? subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        /// <summary>
        /// 值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int? sex { get; set; }

        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        /// <summary>
        /// zh_CN
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public long? subscribe_time { get; set; }

        public string unionid { get; set; }
        public string remark { get; set; }
        /// <summary>
        /// 用户所在的分组ID（暂时兼容用户分组旧接口）
        /// </summary>
        public int? groupid { get; set; }
        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        public List<string> tagid_list { get; set; }
        /// <summary>
        /// 开放平台独有 用户特权信息，json数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        public string[] privilege { get; set; }
        public string privilege_ { get { return privilege.Length>0?string.Join(",", privilege):""; } }
    }
}
