using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ImdbCrawler.Utils
{
    public class WebAO
    {
        private static int max_trys = 10;

        public static String CodeHtml(string Url)
        {
            string result = "";
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
            }
            catch
            {
                return CodeHtml(Url, 1);
            }
            return result;
        }

        public static String CodeHtml(string Url, int tentative)
        {
            string result = "";
            if (tentative < max_trys)
            {
                try
                {
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                    myRequest.Method = "GET";
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                    result = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();
                }
                catch
                {
                    return CodeHtml(Url, ++tentative);
                }
            }
            return result;
        }
    }
}
