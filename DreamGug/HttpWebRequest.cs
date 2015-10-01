using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace DreamGug
{
    class HttpWebRequest
    {
        const string URL = "HTTP://";
        HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
    }
}
