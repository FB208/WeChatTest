using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YZL.Model.WeChat
{
    /// <summary>
    /// 装载微信推送的xml的模型
    /// </summary>
    [XmlType(TypeName = "xml")]
    public class ReceiveXmlPush
    {
        
        [XmlAttribute]
        public string ToUserName { get; set; }
        [XmlAttribute]
        public string FromUserName { get; set; }
        [XmlAttribute]
        public long CreateTime { get; set; }
        [XmlAttribute]
        public string MsgType { get; set; }
        [XmlAttribute]
        public string Event { get; set; }

        public string EventKey { get; set; }
        /// <summary>
        /// 创建时间的Datetime形式
        /// </summary>
        public DateTime CreateTime_
        {
            get { return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(CreateTime); }
            
        }
    }
}
