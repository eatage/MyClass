using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyClass
{
    /// <summary>
    /// Get方法
    /// </summary>
    class HttpGet
    {
        /// <summary>
        /// Get方法请求url并接收返回消息
        /// </summary>
        /// <param name="strUrl">Url地址</param>
        /// <returns></returns>
        public string GetPageInfo(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string ret = string.Empty;
            Stream s;
            string StrDate = "";
            string strValue = "";

            if (response.StatusCode == HttpStatusCode.OK)
            {
                s = response.GetResponseStream();
                ////在这儿处理返回的文本
                StreamReader Reader = new StreamReader(s, Encoding.UTF8);

                while ((StrDate = Reader.ReadLine()) != null)
                {
                    strValue += StrDate + "\r\n";
                }
                //strValue = Reader.ReadToEnd();
            }
            return strValue;
        }
    }
}
