using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class WebClientHelper
    {
        public static string PostJson(string url,string jsonStr)
        {
            using (var client = new WebClient())
            {
                Uri uri = new Uri(url);
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add(HttpRequestHeader.Accept, "json");
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded; charset=UTF-8");
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
                //string json =Microsoft.JScript.GlobalObject.encodeURIComponent(tb_Json.Text);
                string response = client.UploadString(url, "POST", jsonStr);
                return response;
                //var responseString = Encoding.Default.GetString(response);
            }
            return "";
        }

        public static string GetJson(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                Uri uri = new Uri(url);
                client.Encoding = System.Text.Encoding.UTF8;//防止乱码
                string json = client.DownloadString(uri);
                return json;
            }
            return "";
        }
    }
}
