using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using ImdbCrawler.Model;
using ImdbCrawler.Utils;

namespace ImdbCrawler
{
    public class Program
    {
       

        public static void Main(string[] args)
        {
            List<Movie> movies = new List<Movie>();

            //string htmlPage;
            //int index;

            //int[] years = { 2013, 2012, 2011, 2010 };

            //foreach (int year in years)
            //{
            //    for (index = 1; index < 501; index += 50)
            //    {
            //        htmlPage = WebAO.CodeHtml("http://www.imdb.com/search/title?sort=moviemeter,asc&start=" + index + "&title_type=feature&year=" + year + "," + year);
            //        movies.AddRange(Movie.ReadMoviesFromUrl(htmlPage));

            //        htmlPage = WebAO.CodeHtml("http://www.imdb.com/search/title?at=0&sort=user_rating,asc&start=" + index + "&title_type=feature&year=" + year + "," + year);
            //        movies.AddRange(Movie.ReadMoviesFromUrl(htmlPage));
            //    }
            //}

            //FileAO.ExportMoviesToCSV(movies, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\movies.txt");

            movies = Movie.ReadMoviesFromCSV(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\movies.txt");

            Dictionary<string, Director> hashDirectors = new Dictionary<string,Director>();
            foreach (Movie movie in movies)
            {
                if (movie.Director.Length != 0)
                {
                    Director director = new Director();
                    director.Name = movie.Director;
                    director.NameUrl = movie.DirectorUrl;

                    if (!hashDirectors.ContainsKey(director.NameUrl))
                    {
                        hashDirectors.Add(director.NameUrl, director);
                        director.GetInfo();
                    }
                }
            }

            List<Director> directors = new List<Director>();
            foreach (string key in hashDirectors.Keys)
            {
                directors.Add(hashDirectors[key]);
            }

            Console.WriteLine(movies.Count);
            Console.WriteLine(hashDirectors.Count);
            FileAO.ExportDirectorsToCSV(directors, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\directors.txt");
        }

    }
}
