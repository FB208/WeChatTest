using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZL.Code.WeChat.Template
{
    public class TemplateBase
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public MiniProgram miniprogram { get; set; }
    }

    public class MiniProgram
    {
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

    public class keyword
    {
        public string value { get; set; }
        public string color { get; set; }
    }
}
