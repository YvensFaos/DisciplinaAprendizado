using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using ImdbCrawler.Model;

namespace ImdbCrawler
{
    public class Program
    {
        public static String code(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }

        public static void Main(string[] args)
        {
            string htmlPage = code("http://www.imdb.com/search/title?year=2013,2013&title_type=feature&sort=moviemeter,asc");

            List<Movie> movies = Movie.ReadMoviesFromUrl(htmlPage);
        }
    }
}
