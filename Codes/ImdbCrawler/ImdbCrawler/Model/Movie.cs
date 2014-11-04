using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using ImdbCrawler.Utils;

namespace ImdbCrawler.Model
{
    public class Movie
    {
        #region atributos
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
        private string actorsUrl;

        public string ActorsUrl
        {
            get { return actorsUrl; }
            set { actorsUrl = value; }
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

        private bool awardedDirector;

        public bool AwardedDirector
        {
            get { return awardedDirector; }
            set { awardedDirector = value; }
        }
        private bool oscarDirector;

        public bool OscarDirector
        {
            get { return oscarDirector; }
            set { oscarDirector = value; }
        }
        private bool awardedActors;

        public bool AwardedActors
        {
            get { return awardedActors; }
            set { awardedActors = value; }
        }
        private bool oscarActors;

        public bool OscarActors
        {
            get { return oscarActors; }
            set { oscarActors = value; }
        }
#endregion

        public Movie()
        { }

        public static List<Movie> ReadMoviesFromCSV(string file)
        {
            List<Movie> movies = new List<Movie>();

            string[] lines = FileAO.ReadFile(file);

            foreach(string line in lines)
            {
                string[] parameters = line.Split(';');

                Movie movie = new Movie();

                int i = 0;
                movie.Name = parameters[i++];
                movie.NameUrl = parameters[i++];
                movie.Rating = float.Parse(parameters[i++]);
                movie.Director = parameters[i++];
                movie.DirectorUrl = parameters[i++];
                movie.Actors = parameters[i++];
                movie.ActorsUrl = parameters[i++];
                movie.Genre = parameters[i++];
                movie.Certificate = parameters[i++];
                movie.Runtime = float.Parse(parameters[i++]);

                movies.Add(movie);
            }

            return movies;
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

            string name = "";
            string nameUrl = "";
            float rating = 0.0f;
            string director = "";
            string directorUrl = "";
            string actors = "";
            string actorsUrl = "";
            string genre = "";
            string certificate = "?";
            float runtime = 0.0f;

            for (; i < pageLines.Length; i++)
            {
                string line = pageLines[i].Trim();
                if (line.Contains("<a href=\"/title") && !line.Contains("<img") && line[1] == 'a')
                {
                    int initialSize = "<a href=\"/title/".Length;
                    int indexTitle = line.IndexOf("/\">");
                    int indexImg   = line.IndexOf("</a>");

                    if (nameUrl.Length != 0)
                    {
                        Movie movie = new Movie();
                        movie.Name = name.Replace(';', ' ');
                        movie.NameUrl = nameUrl;
                        movie.Rating = rating;
                        movie.Director = director.Replace(';', ' ');
                        movie.DirectorUrl = directorUrl;
                        movie.Actors = actors.Replace(';', ' ');
                        movie.ActorsUrl = actorsUrl;
                        movie.Genre = genre;
                        movie.Certificate = certificate;
                        movie.Runtime = runtime;
                        movies.Add(movie);

                        name = "";
                        nameUrl = "";
                        rating = 0.0f;
                        director = "";
                        directorUrl = "";
                        actors = "";
                        actorsUrl = "";
                        genre = "";
                        certificate = "?";
                        runtime = 0.0f;
                    }

                    nameUrl = line.Substring(initialSize, indexTitle - initialSize);
                    name = line.Substring(indexTitle + "/\">".Length, indexImg - (indexTitle + "/\">".Length));
                    Console.WriteLine(name);
                    Console.WriteLine(nameUrl);
                }
                if (line.Contains("<span class=\"rating-rating\"><span class=\"value\">"))
                {
                    int indexStart = line.IndexOf("<span class=\"rating-rating\"><span class=\"value\">");
                    int indexEnd = line.IndexOf("</span><span class=\"grey\">/");

                    rating = float.Parse(line.Substring(indexStart + "<span class=\"rating-rating\"><span class=\"value\">".Length,
                        indexEnd - (indexStart + "<span class=\"rating-rating\"><span class=\"value\">".Length)));
                    Console.WriteLine(rating);
                }
                if (line.Contains("Dir:"))
                {
                    int size = "Dir: <a href=\"/name/".Length;
                    int indexEnd1 = line.IndexOf("/\">");
                    int indexEnd2 = line.IndexOf("</a>");

                    directorUrl = line.Substring(size + 1, indexEnd1 - (size + 1));
                    director = line.Substring(indexEnd1 + "/\">".Length, indexEnd2 - (indexEnd1 + "/\">".Length));

                    Console.WriteLine(director);
                    Console.WriteLine(directorUrl);
                }
                if (line.Contains("With:"))
                {
                    line = line.Substring(line.IndexOf("<a"));
                    line = line.Trim();
                    string[] actorParams = line.Split(',');


                    foreach (string act1 in actorParams)
                    {
                        //act = "<a href=\"/name/nm0705356/\">Daniel Radcliffe</a>"
                        string act = act1.Trim();
                        int size = "<a href=\"/name/".Length;
                        int indexEnd1 = act.IndexOf("/\">");
                        int indexEnd2 = act.IndexOf("</a>");

                        actorsUrl += act.Substring(size + 1, indexEnd1 - (size + 1)) + "@";
                        actors += act.Substring(indexEnd1 + "/\">".Length, indexEnd2 - (indexEnd1 + "/\">".Length)) + "@";
                    }

                    actors = actors.Substring(0, actors.Length - 1);
                    actorsUrl = actorsUrl.Substring(0, actorsUrl.Length - 1);
                    Console.WriteLine(actors);
                    Console.WriteLine(actorsUrl);
                }
                if (line.Contains("<span class=\"genre\">"))
                {
                    line = line.Substring(line.IndexOf("<a"));
                    line = line.Trim();
                    string[] genreParams = line.Split('|');

                    foreach (string gen in genreParams)
                    {
                        int indexEnd1 = gen.IndexOf("\">");
                        int indexEnd2 = gen.IndexOf("</a>");

                        genre += gen.Substring(indexEnd1 + "\">".Length, indexEnd2 - (indexEnd1 + "\">".Length)) + "@";
                    }

                    genre = genre.Substring(0, genre.Length - 1);
                    Console.WriteLine(genre);
                }
                if (line.Contains("<span class=\"certificate\">"))
                {
                    if(line.Contains("<span title=\""))
                    {
                        int indexEnd1 = line.IndexOf("title=\"");
                        int indexEnd2 = line.IndexOf("\" class=\"");

                        certificate = line.Substring(indexEnd1 + "title=\"".Length, indexEnd2 - (indexEnd1 + "title=\"".Length));
                        Console.WriteLine(certificate);
                    }
                }
                if (line.Contains("<span class=\"runtime\">"))
                {
                    int indexEnd1 = line.IndexOf("<span class=\"runtime\">");
                    int indexEnd2 = line.IndexOf(" mins.");

                    runtime = float.Parse(line.Substring(indexEnd1 + "<span class=\"runtime\">".Length, indexEnd2 - (indexEnd1 + "<span class=\"runtime\">".Length)));
                    Console.WriteLine(runtime);

                    Movie movie = new Movie();
                    movie.Name = name.Replace(';', ' ');
                    movie.NameUrl = nameUrl;
                    movie.Rating = rating;
                    movie.Director = director.Replace(';', ' ');
                    movie.DirectorUrl = directorUrl;
                    movie.Actors = actors.Replace(';', ' ');
                    movie.ActorsUrl = actorsUrl;
                    movie.Genre = genre;
                    movie.Certificate = certificate;
                    movie.Runtime = runtime;
                    movies.Add(movie);

                    name = "";
                    nameUrl = "";
                    rating = 0.0f;
                    director = "";
                    directorUrl = "";
                    actors = "";
                    actorsUrl = "";
                    genre = "";
                    certificate = "?";
                    runtime = 0.0f;
                }
            }
            
            return movies;
        }

        public void GetDetailedInfo(Dictionary<string, Actor> actors, Dictionary<string, Director> directors)
        {
            if (Director.Length != 0)
            {
                Director director = directors[Director];
                AwardedDirector = (director.Awards > 0) ? true : false;
                AwardedDirector = director.Oscar;
            }
            if (Actors.Length != 0)
            {
                string[] actorsName = Actors.Split('@');

                foreach (string actorName in actorsName)
                {
                    Actor actor = actors[actorName];
                    AwardedActors |= (actor.Awards > 0) ? true : false;
                    OscarActors |= actor.Oscar;
                }
            }
        }

        public override string ToString()
        {
            return Name + ";" + NameUrl + ";" + Rating + ";" + Director + ";" + DirectorUrl + ";" + Actors + ";" + ActorsUrl + ";" + Genre + ";" + Certificate + ";" + Runtime;
        }

        public string ToStringDetailed()
        {
            return Name + ";" + NameUrl + ";" + Rating + ";" + Director + ";" + DirectorUrl + ";" + Actors + ";" + ActorsUrl + ";" + Genre + ";" + Certificate + ";" + Runtime + ";" + AwardedDirector + ";" + OscarDirector + ";" + AwardedActors + ";" + OscarActors;
        }
    }

    //http://www.imdb.com/name/nm1547964/awards?ref_=nm_ql_2
    //http://www.imdb.com/name/(n)m0014960/awards?ref_=nm_ql_2

}
