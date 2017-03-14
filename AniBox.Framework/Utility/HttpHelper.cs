using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Utility
{
    class HttpHelper
    {
        public static string GetHtmlOfUrl(string Url, Encoding encoding)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {

            }

            return "";
        }
    }
}
