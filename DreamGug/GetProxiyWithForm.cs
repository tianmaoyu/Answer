using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace DreamGug
{
    public partial class GetProxiyWithForm : Form
    {
        List<string> ipList=new List<string>();
        public string html;
        public bool isCompleted = false;
        public GetProxiyWithForm()
        {
            InitializeComponent();
            string address = "http://vxer.daili666.com/ip/?tid=555950095890637&num=10&ports=80&filter=on";
            webBrowser1.Navigate(new Uri(address));
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            html = this.webBrowser1.DocumentText;
            isCompleted = true;
        }

        public string GetProxiyIP()
        {
             return html;
        }
    }
}
