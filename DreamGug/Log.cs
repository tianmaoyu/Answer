using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DreamGun
{
    class Log
    {
        /// <summary>
        ///  日志记录
        /// </summary>
        /// <param name="message">要写入消息</param>
        /// <param name="path">文件路径：C:\\Log.txt</param>
        public static void WriteLog(string message, LogFile logFile)
        {
            string pathFile = null;
            switch (logFile)
            {
                case LogFile.GetProxyIP:
                    pathFile = "C:\\GreamGugGetProxyIP.txt";
                    break;
                case LogFile.SetIE:
                    pathFile = "C:\\GreamGugProxies.txt";
                    break;
                case LogFile.ValidateProxy:
                    pathFile = "C:\\GreamGugValidateProxy.txt";
                    break;
                default:
                    pathFile = "C:\\GreamGugLog.txt";
                    break;
            }
            try
            {

                StreamWriter sw = new StreamWriter(pathFile, true, Encoding.UTF8);
                sw.WriteLine("{0} ---{1}", DateTime.Now.ToLongDateString(), message);
                sw.Close();
                sw.Dispose();
            }
            catch (IOException ex)
            {
                return;
            }
        }
        public enum LogFile
        {
            [Description("设置IE代理")]
            SetIE,
            [Description("验证端口")]
            ValidateProxy,
            [Description("提取端口")]
            GetProxyIP,
            [Description("提取端口")]
            Log

        }
    }
}
