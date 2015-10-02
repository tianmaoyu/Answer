using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DreamGug;
using Timer = System.Timers.Timer;

namespace DreamGun
{
    class OpenIE
    {
        public void Open(string url)
        {
            //Proxies.UnsetProxy();
            //string ids = null;
            bool isCompleted = false;
            EventHandlers everHandlers = new EventHandlers();
            SHDocVw.InternetExplorer ieExplorer = new SHDocVw.InternetExplorer();
            object Empty = 0;
            object URL = url;
            ieExplorer.BeforeNavigate2 += new SHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(everHandlers.OnBeforeNavigate2);
            ieExplorer.Visible = true;
            ieExplorer.Navigate2(ref URL, ref Empty, ref Empty, ref Empty, ref Empty);
            Timer timer=new Timer(10000);
            timer.AutoReset = false;
            timer.Start();
            timer.Elapsed += delegate { isCompleted = true; };
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
                try
                {
                    if (url.Contains("2345"))
                    {
                        mshtml.IHTMLDocument2 doc2 = (mshtml.IHTMLDocument2) ieExplorer.Document;
                        mshtml.IHTMLElementCollection inputs = (mshtml.IHTMLElementCollection) doc2.all.tags("INPUT");
                        mshtml.HTMLInputElement input1 = (mshtml.HTMLInputElement) inputs.item("word", 0);
                        input1.value = "刘德华";
                        mshtml.IHTMLElementCollection ccCollection = doc2.all.tags("A");
                        int random = (new Random()).Next(25, 73); //25新浪，73唯品会
                        mshtml.IHTMLElement element2 = (mshtml.IHTMLElement) ccCollection.item("2", random);
                        element2.click();
                    }
                    if (url.Contains("360"))
                    {
                        mshtml.IHTMLDocument2 doc2 = (mshtml.IHTMLDocument2) ieExplorer.Document;
                        mshtml.IHTMLElementCollection ccCollection = doc2.all.tags("A");
                        int random = (new Random()).Next(70, 140);
                        mshtml.IHTMLElement element2 = (mshtml.IHTMLElement) ccCollection.item(random);
                        element2.click();
                    }
                }
                catch (Exception)
                {


                }
                finally
                {
                    Thread.Sleep(7000);
                    ieExplorer.Quit();

                    System.Diagnostics.Process[] myProcesses;
                    myProcesses = System.Diagnostics.Process.GetProcessesByName("IEXPLORE");
                    foreach (System.Diagnostics.Process instance in myProcesses)
                    {
                        instance.CloseMainWindow();
                    }
                    CleanIECookics.CleanCookie();
                    Thread.Sleep(7000);
                }
               
              
            }
        }

        public List<string> GetUrList()
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
