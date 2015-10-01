using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using System.IO;
using System.Net;

namespace DreamGun
{
    class ValidateProxy
    {  
        /// <summary>
        /// 传入ip和端口格式为：192.168.1.6:8080
        /// </summary>
        /// <param name="str">IP and proxy</param>
        /// <returns></returns>
        public string GetUseableIP(string str)
        {
            string result = null;
            if (Validate(str))
            {
                result = str;
            }
            return result;
        }
        public bool Validate(string ipAndPort)
        {
            bool reslut = false;
            ServicePointManager.DefaultConnectionLimit = 1000;
            if (ipAndPort != null)
            {
                string ip;
                int port;
                string[] str = ipAndPort.Split(':');
                if (str.Count() == 2)
                {
                    ip = str[0].ToString();
                    port = Int32.Parse(str[1]);
                    WebProxy wepProxy = new WebProxy(ip, port);
                    HttpWebRequest request = WebRequest.Create("http://www.ip.cn/") as HttpWebRequest;
                    request.Proxy = wepProxy;
                    request.Timeout = 5000;
                 
                    request.Headers.Set("Accept-Encoding", "gzip,deflate,sdch");
                    request.Headers.Set("Accept-Language", "zh-CN,zh;q=0.8");
                    try
                    {
                        Stream responseStream = request.GetResponse().GetResponseStream();
                        StreamReader reader = new StreamReader(responseStream);
                        if (reader.ReadToEnd().Contains("地址查询"))
                        {
                            reslut = true;
                        }
                        else
                        {
                            reslut = false;
                        }
                        reader.Close();
                        responseStream.Close();
                        reader.Dispose();
                        responseStream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        reslut = false;
                    }
                }

            }

            return reslut;

        }
    }
}
