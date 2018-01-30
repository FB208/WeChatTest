using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using YZL.Models.WeChat;

namespace YZL.Code.WeChat
{
    public class EventManager
    {
        public EventManager()
        {
        }

        /// <summary>
        /// 关注事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool FollowEvent(ReceiveXmlPush model)
        {
            return true;
        }
        /// <summary>
        /// 取消关注事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UnFollowEvent(ReceiveXmlPush model)
        {
            return true;
        }
        /// <summary>
        /// 点击菜单事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ClickEvent(ReceiveXmlPush model)
        {
            SendXml smodel= new SendXml();
            smodel.ToUserName = model.FromUserName;
            smodel.FromUserName = model.ToUserName;
            smodel.CreateTime = (int)(System.DateTime.Now.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks) / 10000;
            smodel.MsgType = "text";
            smodel.Content = $"此消息来自开发服务器，您点击了按钮ID:{model.EventKey},id:{smodel.ToUserName}";

            var xdoc = new XDocument(new XElement("xml",
                new XElement("ToUserName", new XCData(smodel.ToUserName)),
                new XElement("FromUserName", new XCData(smodel.FromUserName)),
                new XElement("CreateTime", new XText(smodel.CreateTime.ToString())),
                new XElement("MsgType", new XCData(smodel.MsgType)),
                new XElement("Content", new XCData(smodel.Content))
            ));

            return xdoc.ToString();
        }
    }
}
