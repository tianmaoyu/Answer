using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using mshtml;

namespace DreamGun
{

    public class EventHandlers
    {
        public void OnBeforeNavigate2(object sender, ref object URL,
            ref object Flags, ref object Target,
            ref object PostData, ref object Headers,
            ref bool Cancel)
        {

        }
    }

    public class GetProxiyIP
    {

        public static List<string> OperateIE(string url = "")
        {
            Proxies.UnsetProxy();
            List<string> ipList = new List<string>();
            string ids = null;
            bool isCompleted = false;
            EventHandlers everHandlers = new EventHandlers();
            SHDocVw.InternetExplorer ieExplorer = new SHDocVw.InternetExplorer();
            object Empty = 0;
            object URL = "http://vxer.daili666.com/ip/?tid=555950095890637&num=10&ports=80&filter=on";
            ieExplorer.BeforeNavigate2 +=
                new SHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(everHandlers.OnBeforeNavigate2);
            ieExplorer.Visible = false;
            ieExplorer.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);
            ieExplorer.DocumentComplete += delegate
            {
                var documentClass = ieExplorer.Document;
                // mshtml.HTMLDocumentClass 
                ids = documentClass.IHTMLDocument2_body.innerHTML;
                isCompleted = true;
            };
            while (true)
            {
                if (isCompleted)
                {
                    break;

                }
            }
            if (ids != null)
            {
                ids = ids.Substring(ids.IndexOf('>') + 1, ids.LastIndexOf('<') - 5);
                StringReader s = new StringReader(ids);

                while (s.Peek() > 0)
                {
                    var s1 = s.ReadLine();
                    ipList.Add(s1);
                }
                s.Close();
            }
            ieExplorer.Quit();
            return ipList;
        }

        public static List<string> GetProxiesFromFile()
        {
            List<string> reslutList = new List<string>();
            StreamReader sw = new StreamReader("url.txt", Encoding.UTF8);
            while (sw.Peek() > 0)
            {
                reslutList.Add(sw.ReadLine());
            }
            sw.Close();
            return reslutList;
        }
      
    }
}
