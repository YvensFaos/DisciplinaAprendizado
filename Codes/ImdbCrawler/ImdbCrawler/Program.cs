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

            //movies = Movie.ReadMoviesFromCSV(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\movies.txt");
            int counter = 1;

            //Dictionary<string, Director> hashDirectors = new Dictionary<string, Director>();
            //foreach (Movie movie in movies)
            //{
            //    Console.WriteLine("Movie: " + counter + "/" + movies.Count);
            //    if (movie.Director.Length != 0)
            //    {
            //        Director director = new Director();
            //        director.Name = movie.Director;
            //        director.NameUrl = movie.DirectorUrl;

            //        if (!hashDirectors.ContainsKey(director.NameUrl))
            //        {
            //            hashDirectors.Add(director.NameUrl, director);
            //            director.GetInfo();
            //        }
            //    }
            //    counter++;
            //}

            //List<Director> directors = new List<Director>();
            //foreach (string key in hashDirectors.Keys)
            //{
            //    directors.Add(hashDirectors[key]);
            //}

            //Console.WriteLine(movies.Count);
            //Console.WriteLine(hashDirectors.Count);
            //FileAO.ExportDirectorsToCSV(directors, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\directors.txt");

            //Dictionary<string, Actor> hashActors = new Dictionary<string, Actor>();
            //foreach (Movie movie in movies)
            //{
            //    Console.WriteLine("Movie: " + counter + "/" + movies.Count);
            //    if (movie.Actors.Length != 0)
            //    {
            //        string[] actorsName = movie.Actors.Split('@');
            //        string[] actorsUrl = movie.ActorsUrl.Split('@');

            //        for (int i = 0; i < actorsName.Length; i++)
            //        {
            //            Actor actor = new Actor();
            //            actor.Name = actorsName[i];
            //            actor.NameUrl = actorsUrl[i];

            //            if (!hashActors.ContainsKey(actor.NameUrl))
            //            {
            //                hashActors.Add(actor.NameUrl, actor);
            //                actor.GetInfo();
            //                FileAO.ExportActorToCSV(actor, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\actors.txt");
            //            }
            //        }
            //    }
            //    counter++;
            //}

            //List<Actor> actors = new List<Actor>();
            //foreach (string key in hashActors.Keys)
            //{
            //    actors.Add(hashActors[key]);
            //}

            //Console.WriteLine(movies.Count);
            //Console.WriteLine(hashActors.Count);

            //Dictionary<string, Actor> hashActors = new Dictionary<string, Actor>();
            //foreach (Movie movie in movies)
            //{
            //    Console.WriteLine("Movie: " + counter + "/" + movies.Count);
            //    counter++;
            //    if (movie.Actors.Length != 0)
            //    {
            //        string[] actorsName = movie.Actors.Split('@');
            //        string[] actorsUrl = movie.ActorsUrl.Split('@');

            //        for (int i = 0; i < actorsName.Length; i++)
            //        {
            //            Actor actor = new Actor();
            //            actor.Name = actorsName[i];
            //            actor.NameUrl = actorsUrl[i];

            //            if (!hashActors.ContainsKey(actor.NameUrl))
            //            {
            //                hashActors.Add(actor.NameUrl, actor);
            //            }
            //        }
            //    }
            //}

            //List<Actor> actors = new List<Actor>();
            //foreach (string key in hashActors.Keys)
            //{
            //    actors.Add(hashActors[key]);
            //}

            //bool rereading = false;
            //counter = 0;
            //foreach (Actor actor in actors)
            //{
            //    Console.WriteLine("Actor: " + counter + "/" + actors.Count);
            //    counter++;
            //    if (rereading)
            //    {
            //        actor.GetInfo();
            //        FileAO.ExportActorToCSV(actor, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\actors.txt");
            //    }
            //    else
            //    {
            //        if (actor.Name.Equals("Ann Augustine"))
            //        {
            //            rereading = true;
            //        }
            //    }
            //}

            //Genre.GenreStatistics(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\genres.txt");

            //movies = Movie.ReadMoviesFromCSV(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\movies.txt");
            //Dictionary<string, Actor> actors = Actor.ReadHashActorsFromCSV(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\actors.txt");
            //Dictionary<string, Director> directors = Director.ReadHashDirectorsFromCSV(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\directors.txt");

            //foreach (Movie movie in movies)
            //{
            //    movie.GetDetailedInfo(actors, directors);
            //}
            //FileAO.ExportMoviesDetailedToCSV(movies, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\moviesDetailed_2.txt");

            //movies = Movie.ReadMoviesFromCSV(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\moviesDetailed.txt");
            //foreach (Movie movie in movies)
            //{
            //    movie.GetBusinessInfo();
            //    Console.WriteLine("Movie [" + movie.EnglishName + "]: " + counter + "/" + movies.Count);
            //    counter++;
            //}

            //FileAO.ExportMoviesDetailedBusinessToCSV(movies, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\moviesDetailedBusiness.txt");
            //Console.Read();


            //Movie.GetStatistics(movies);

            movies = Movie.ReadMoviesFromCSV(@"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\moviesDetailed_2.txt");
            Movie.ExportMoviesToWeka(movies, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\movies.arff");
        }

    }
}
