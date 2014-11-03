using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbCrawler.Model
{
    public class Movie
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string nameUrl;

        public string NameUrl
        {
            get { return nameUrl; }
            set { nameUrl = value; }
        }

        private float rating;

        public float Rating
        {
            get { return rating; }
            set { rating = value; }
        }
        private string director;

        public string Director
        {
            get { return director; }
            set { director = value; }
        }
        private string directorUrl;

        public string DirectorUrl
        {
            get { return directorUrl; }
            set { directorUrl = value; }
        }
        private string actors;

        public string Actors
        {
            get { return actors; }
            set { actors = value; }
        }
        private string genre;

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        private string certificate;

        public string Certificate
        {
            get { return certificate; }
            set { certificate = value; }
        }
        private float runtime;

        public float Runtime
        {
            get { return runtime; }
            set { runtime = value; }
        }

        public static List<Movie> ReadMoviesFromUrl(string htmlPage)
        {
            List<Movie> movies = new List<Movie>();

            string[] pageLines = htmlPage.Split('\n');

            int i = 0;
            for (i = 0; i < pageLines.Length; i++)
            {
                if (pageLines[i].Contains("<table class=\"results\">"))
                {
                    break;
                }
            }
            //Console.WriteLine(pageLines[i]);

            //Começa a leitura de fato
            string name = "";
            string nameUrl = "";
            float rating = 0.0f;
            string director = "";
            string directorUrl = "";
            string actors = "";
            string genre = "";
            string certificate = "";
            float runtime = 0.0f;

            for (; i < pageLines.Length; i++)
            {
                string titleLine = pageLines[i].Trim();
                if (titleLine.Contains("<a href=\"/title") && !titleLine.Contains("<img") && titleLine[1] == 'a')
                {
                    int initialSize = "<a href=\"/title/".Length;
                    int indexTitle = titleLine.IndexOf("/\">");
                    int indexImg   = titleLine.IndexOf("</a>");

                    nameUrl = titleLine.Substring(initialSize, indexTitle - initialSize);
                    name = titleLine.Substring(indexTitle + "/\">".Length, indexImg - (indexTitle + "/\">".Length));
                    Console.WriteLine(name);
                    Console.WriteLine(nameUrl);
                }
                if (titleLine.Contains("<span class=\"rating-rating\"><span class=\"value\">"))
                {
                    int indexStart = titleLine.IndexOf("<span class=\"rating-rating\"><span class=\"value\">");
                    int indexEnd = titleLine.IndexOf("</span><span class=\"grey\">/");

                    rating = float.Parse(titleLine.Substring(indexStart + "<span class=\"rating-rating\"><span class=\"value\">".Length,
                        indexEnd - (indexStart + "<span class=\"rating-rating\"><span class=\"value\">".Length)));
                    Console.WriteLine(rating);
                }
                if (titleLine.Contains("Dir:"))
                {
                    int size = "Dir: <a href=\"/name/".Length;
                    int indexEnd1 = titleLine.IndexOf("/\">");
                    int indexEnd2 = titleLine.IndexOf("</a>");

                    directorUrl = titleLine.Substring(size + 1, indexEnd1 - (size + 1));
                    director = titleLine.Substring(indexEnd1 + "/\">".Length, indexEnd2 - (indexEnd1 + "/\">".Length));

                    Console.WriteLine(director);
                    Console.WriteLine(directorUrl);
                }
                if (titleLine.Contains("With:"))
                {
                    //titleLine	"With: <a href=\"/name/nm0705356/\">Daniel Radcliffe</a>, <a href=\"/name/nm1017334/\">Juno Temple</a>, <a href=\"/name/nm1540404/\">Max Minghella</a>"	string

                }
            }
            name = "";
            nameUrl = "";
            rating = 0.0f;
            director = "";
            directorUrl = "";
            actors = "";
            genre = "";
            certificate = "";
            runtime = 0.0f;
            
            return movies;
        }
    }
}
