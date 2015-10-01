using System;
using System.Collections.Generic;
using System.Threading;
using mshtml;
namespace DreamGun
{
    class Program
    {
        public delegate void MethodCaller();
        static void Main(string[] args)
        {
            for (int i = 0; i <= 7; i++)
            {
                Fire();
            }
          
         // OpenIE();
        }

        private static void Fire()
        {
            UIAutomationForProxyThorn uiAutomationForProxyThornn = new UIAutomationForProxyThorn();
            uiAutomationForProxyThornn.GetUseableIP();
            uiAutomationForProxyThornn.InitGridPattern();
            var ipCount = uiAutomationForProxyThornn.gridRowCount;
            for (int i = 0; i < ipCount; i++)
            {
                uiAutomationForProxyThornn.SetIPandSetIE(i);
                StartIE();
            }
            uiAutomationForProxyThornn.DeleteIPList();
        }

        private static void StartIE()
        {
            OpenIE openIe = new OpenIE();
            var urls = openIe.GetUrList();
            foreach (var url in urls)
            {
                try
                {
                    openIe.Open(url);
                }
                catch (Exception)
                {
                    Thread.Sleep(10000);
                }
            }
        }

        public static void OpenIE()
        {
            Proxies.UnsetProxy();
            List<string> ipList = new List<string>();
            string ids = null;
            bool isCompleted = false;
            EventHandlers everHandlers = new EventHandlers();
            SHDocVw.InternetExplorer ieExplorer = new SHDocVw.InternetExplorer();
            object Empty = 0;
            object URL = "http://hao.360.cn/?src=lm&ls=n1b07c70297";
            ieExplorer.BeforeNavigate2 += new SHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(everHandlers.OnBeforeNavigate2);
            ieExplorer.Visible = true;
            ieExplorer.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);
            ieExplorer.DocumentComplete += delegate
            {
                //var documentClass = ieExplorer.Document;
                //// mshtml.HTMLDocumentClass 
                //ids = documentClass.IHTMLDocument2_body.innerHTML;
                isCompleted = true;
            };
            while (true)
            {
                if (isCompleted)
                {
                    break;

                }
            }
            if (isCompleted)
            {
                mshtml.IHTMLDocument2 doc2 = (mshtml.IHTMLDocument2)ieExplorer.Document;
                //mshtml.IHTMLElementCollection inputs = (mshtml.IHTMLElementCollection)doc2.all.tags("INPUT");
                //mshtml.HTMLInputElement input1 = (mshtml.HTMLInputElement)inputs.item("word", 0);
                //input1.value = "刘德华";
                mshtml.IHTMLElementCollection ccCollection = doc2.all.tags("A");

                //mshtml.IHTMLElement element2 = (mshtml.IHTMLElement)ccCollection;   //25新浪，73唯品会
                //int all = ccCollection.length;
                //element2.click();
                int i=0;
            }
            Thread.Sleep(5000);
            ieExplorer.Quit();
           
            System.Diagnostics.Process[] myProcesses;
            myProcesses = System.Diagnostics.Process.GetProcessesByName("IEXPLORE");
            foreach (System.Diagnostics.Process instance in myProcesses)
            {
                instance.CloseMainWindow();
            }
        }
    }
}
