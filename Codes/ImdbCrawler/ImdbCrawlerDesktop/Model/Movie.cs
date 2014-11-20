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

        private string budget;

        public string Budget
        {
            get { return budget; }
            set { budget = value; }
        }

        private string englishName;

        public string EnglishName
        {
            get { return englishName; }
            set { englishName = value; }
        }

        private string classification;

        public string Classification
        {
            get { return classification; }
            set { classification = value; }
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
                if (parameters.Length > i)
                {
                    movie.AwardedDirector = (parameters[i++][0] == 'T') ? true : false;
                    movie.OscarDirector =   (parameters[i++][0] == 'T') ? true : false;
                    movie.AwardedActors =   (parameters[i++][0] == 'T') ? true : false;
                    movie.OscarActors =     (parameters[i++][0] == 'T') ? true : false;
                }
                if (parameters.Length > i)
                {

                }
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

                    try
                    {
                        rating = float.Parse(line.Substring(indexStart + "<span class=\"rating-rating\"><span class=\"value\">".Length,
                            indexEnd - (indexStart + "<span class=\"rating-rating\"><span class=\"value\">".Length)));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        rating = 0;
                    }

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

        public static void ExportMoviesToWeka(List<Movie> movies, string destination)
        {
            List<string> fileLines = new List<string>();

            fileLines.Add("@RELATION movie");
            //fileLines.Add("@ATTRIBUTE duration NUMERIC");
            fileLines.Add("@ATTRIBUTE duration {Short,Regular,Long,Very_Long}");
            fileLines.Add("@ATTRIBUTE awardedDirector {0,1}");
            fileLines.Add("@ATTRIBUTE oscarDirector {0,1}");
            fileLines.Add("@ATTRIBUTE awardedActor {0,1}");
            fileLines.Add("@ATTRIBUTE oscarActor {0,1}");
            fileLines.Add("@ATTRIBUTE genre {Drama,Horror,Action,Comedy,Others}");
            fileLines.Add("@ATTRIBUTE certificate {G, R, PG_13, PG, NOT_RATED}");
            fileLines.Add("@ATTRIBUTE rating {Bad,Regular,Good}");
            fileLines.Add("@DATA");

            string line = "";

            int a = 0, b = 0, c = 0;

            int drama = 0, horror = 0, action = 0, comedy = 0, others = 0;
            int shorte = 0, regular = 0, longe = 0, verylong = 0;
            foreach (Movie movie in movies)
            {
                //line += movie.Runtime + ",";

                string runtime = "";
                if (movie.Runtime < 88.0f)
                {
                    runtime = "Short";
                    shorte++;
                }
                else if (movie.Runtime < 122.0f)
                {
                    runtime = "Regular";
                    regular++;
                }
                else if (movie.Runtime < 160.0f)
                {
                    runtime = "Long";
                    longe++;
                }
                else
                {
                    runtime = "Very_Long";
                    verylong++;
                }
                line += runtime + ",";

                line += ((movie.AwardedDirector)? "1" : "0") + ",";
                line += ((movie.OscarDirector)? "1" : "0") + ",";
                line += ((movie.AwardedActors)? "1" : "0") + ",";
                line += ((movie.OscarActors)? "1" : "0") + ",";

                string genre = "";
                string actualGenre = movie.Genre.Split('@')[0];
                movie.Genre = actualGenre;
                movie.GetMovieGenre();

                line += genre + ",";

                string certificate = "";
                if (movie.Certificate.Equals("R"))
                {
                    certificate = "R";
                }
                else if (movie.Certificate.Equals("G"))
                {
                    certificate = "G";
                }
                else if (movie.Certificate.Equals("PG_13"))
                {
                    certificate = "PG_13";
                }
                else if (movie.Certificate.Equals("PG"))
                {
                    certificate = "PG";
                }
                else
                {
                    certificate = "NOT_RATED";
                }

                line += certificate + ",";

                movie.GetClassification();
                string rating = movie.Classification;

                line += rating;
                fileLines.Add(line);
                line = "";
            }

            Console.WriteLine("drama = " + drama);
            Console.WriteLine("horror = " + horror);
            Console.WriteLine("action = " + action);
            Console.WriteLine("comedy = " + comedy);
            Console.WriteLine("others = " + others);

            Console.WriteLine();

            Console.WriteLine("Bad = " + c);
            Console.WriteLine("Regular = " + b);
            Console.WriteLine("Good = " + a);

            Console.WriteLine();

            Console.WriteLine("shorte = " + shorte);
            Console.WriteLine("regular = " + regular);
            Console.WriteLine("longe = " + longe);
            Console.WriteLine("verylong = " + verylong);

            FileAO.ExportToArff(fileLines, destination);
        }

        public static void GetStatistics(List<Movie> movies)
        {
            Dictionary<string, int> certificates = new Dictionary<string, int>();
            Dictionary<string, int> genres  = new Dictionary<string, int>();

            foreach (Movie movie in movies)
            {
                if (!certificates.ContainsKey(movie.Certificate))
                {
                    certificates.Add(movie.Certificate, 0);
                }

                certificates[movie.Certificate]++;

                string genre = movie.Genre.Split('@')[0];
                if (!genres.ContainsKey(genre))
                {
                    genres.Add(genre, 0);
                }

                genres[genre]++;
            }

            foreach (string key in certificates.Keys)
            {
                Console.WriteLine(key + " " + certificates[key]);
            }

            Console.WriteLine();
            foreach (string key in genres.Keys)
            {
                Console.WriteLine(key + " " + genres[key]);
            }

            Console.ReadLine();
        }

        public void GetDetailedInfo(Dictionary<string, Actor> actors, Dictionary<string, Director> directors)
        {
            if (Director.Length != 0)
            {
                Director director = directors[Director];
                AwardedDirector = (director.Awards > 0) ? true : false;
                OscarDirector = director.Oscar;
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

        public void GetBusinessInfo()
        {
            string url = "http://www.imdb.com/title/@/business?ref_=ttco_ql_4";

            url = url.Replace("@", nameUrl);
            string htmlPage = WebAO.CodeHtml(url);
            string[] pageLines = htmlPage.Split('\n');

            EnglishName = "";
            int i = 0;
            for (i = 0; i < pageLines.Length; i++)
            {
                string line = pageLines[i].Trim();
                if (line.Contains("Budget"))
                {
                    line = pageLines[++i].Trim();

                    Budget = (line.Substring(1, line.IndexOf(" ("))).Replace(",", "");
                    Budget = Budget.Trim();
                }

                if (line.Contains("<span class=\"title-extra\""))
                {
                    //Pode ser que o nome nacional seja igual ao nome original
                    int indexItalic = line.IndexOf("<i>(or");
                    if (indexItalic != -1)
                    {
                        int jump = line.IndexOf("<span class=\"title-extra\">") + "<span class=\"title-extra\">".Length;

                        EnglishName = line.Substring(jump, line.IndexOf("<i>(or") - (jump + 1));
                        EnglishName = EnglishName.Trim();
                    }
                }
            }
            if (EnglishName.Length == 0)
            {
                EnglishName = Name;
            }
        }

        public void GetClassification()
        {
            string classification = "";
            if (this.Rating < 50.0f)
            {
                classification = "Bad";
            }
            else if (this.Rating < 70.0f)
            {
                classification = "Regular";
            }
            else
            {
                classification = "Good";
            }

            Classification = classification;
        }

        public void GetMovieGenre()
        {
            if(Genre.Contains('@'))
            {
                Genre = Genre.Split('@')[0];
            }

            #region defining genre
            if (Genre.Equals("Drama") || Genre.Equals("Family") || Genre.Equals("Romance") || Genre.Equals("History") || Genre.Equals("Reality-TV") || Genre.Equals("Adult") || Genre.Equals("Biography"))
            {
                Genre = "Drama";
            }
            else if (Genre.Equals("Horror") || Genre.Equals("Thriller") || Genre.Equals("Mistery"))
            {
                Genre = "Horror";
            }
            else if (Genre.Equals("Action") || Genre.Equals("Adventure") || Genre.Equals("Crime") || Genre.Equals("Sci-Fi") || Genre.Equals("Fantasy") || Genre.Equals("War") || Genre.Equals("Western") || Genre.Equals("Sport"))
            {
                Genre = "Action";
            }
            else if (Genre.Equals("Comedy"))
            {
                Genre = "Comedy";
            }
            else
            {
                Genre = "Others";
            }
            #endregion
        }

        public override string ToString()
        {
            return Name + ";" + NameUrl + ";" + Rating + ";" + Director + ";" + DirectorUrl + ";" + Actors + ";" + ActorsUrl + ";" + Genre + ";" + Certificate + ";" + Runtime;
        }

        public string ToStringDetailed()
        {
            return Name + ";" + NameUrl + ";" + Rating + ";" + Director + ";" + DirectorUrl + ";" + Actors + ";" + ActorsUrl + ";" + Genre + ";" + Certificate + ";" + Runtime + ";" + AwardedDirector + ";" + OscarDirector + ";" + AwardedActors + ";" + OscarActors;
        }

        public string ToStringDetailedBusiness()
        {
            return Name + ";" + NameUrl + ";" + Rating + ";" + Director + ";" + DirectorUrl + ";" + Actors + ";" + ActorsUrl + ";" + Genre + ";" + Certificate + ";" + Runtime + ";" + AwardedDirector + ";" + OscarDirector + ";" + AwardedActors + ";" + OscarActors + ";" + Budget + ";" + EnglishName;
        }
    }
}
