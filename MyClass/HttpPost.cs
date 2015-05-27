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
    /// Post方法
    /// </summary>
    class HttpPost
    {
        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="Url">URL</param>
        /// <param name="Params">Post数据 多个参数用&分隔</param>
        /// <returns></returns>
        public static string of_SendPost_utf8(string Url, string Params)
        {
            // 初始化WebClient  
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("Accept", "*/*");
            webClient.Headers.Add("Accept-Language", "zh-cn");
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //将字符串转换成字节数组
            byte[] postData = Encoding.GetEncoding("utf-8").GetBytes(Params);
            try
            {
                byte[] responseData = webClient.UploadData(Url, "POST", postData);
                string srcString = Encoding.GetEncoding("utf-8").GetString(responseData);
                return srcString.Trim();
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="posturl">URL</param>
        /// <param name="postData">Post数据 多个参数用&分隔</param>
        public string PostPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }
    }
}
