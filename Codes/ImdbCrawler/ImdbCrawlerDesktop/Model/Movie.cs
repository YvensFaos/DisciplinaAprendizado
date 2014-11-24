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

        private bool isViolent;

        public bool IsViolent
        {
            get { return isViolent; }
            set { isViolent = value; }
        }
        private bool hasFireguns;

        public bool HasFireguns
        {
            get { return hasFireguns; }
            set { hasFireguns = value; }
        }
        private bool isGoreViolent;

        public bool IsGoreViolent
        {
            get { return isGoreViolent; }
            set { isGoreViolent = value; }
        }
        private bool hasSex;

        public bool HasSex
        {
            get { return hasSex; }
            set { hasSex = value; }
        }
        private bool hasNudeScenes;

        public bool HasNudeScenes
        {
            get { return hasNudeScenes; }
            set { hasNudeScenes = value; }
        }
        private bool aboutRelationships;

        public bool AboutRelationships
        {
            get { return aboutRelationships; }
            set { aboutRelationships = value; }
        }
        private bool aboutFamily;

        public bool AboutFamily
        {
            get { return aboutFamily; }
            set { aboutFamily = value; }
        }
        private bool hasFlashbacks;

        public bool HasFlashbacks
        {
            get { return hasFlashbacks; }
            set { hasFlashbacks = value; }
        }
        private bool hasSurpriseEnding;

        public bool HasSurpriseEnding
        {
            get { return hasSurpriseEnding; }
            set { hasSurpriseEnding = value; }
        }
        private bool aboutHumanDrama;

        public bool AboutHumanDrama
        {
            get { return aboutHumanDrama; }
            set { aboutHumanDrama = value; }
        }
        private bool aboutNatureOrCity;

        public bool AboutNatureOrCity
        {
            get { return aboutNatureOrCity; }
            set { aboutNatureOrCity = value; }
        }
        private bool hasNowadaysTechnology;

        public bool HasNowadaysTechnology
        {
            get { return hasNowadaysTechnology; }
            set { hasNowadaysTechnology = value; }
        }
        private bool isSequel;

        public bool IsSequel
        {
            get { return isSequel; }
            set { isSequel = value; }
        }
        private bool isBasedOnNovel;

        public bool IsBasedOnNovel
        {
            get { return isBasedOnNovel; }
            set { isBasedOnNovel = value; }
        }
        private bool isWrittenByDirector;

        public bool IsWrittenByDirector
        {
            get { return isWrittenByDirector; }
            set { isWrittenByDirector = value; }
        }
        private bool isIndependent;

        public bool IsIndependent
        {
            get { return isIndependent; }
            set { isIndependent = value; }
        }
        private bool hasCameraWorks;

        public bool HasCameraWorks
        {
            get { return hasCameraWorks; }
            set { hasCameraWorks = value; }
        }
#endregion

        private static List<string> violent;
        private static List<string> firegun;
        private static List<string> gore;
        private static List<string> sex;
        private static List<string> nude;
        private static List<string> relationship;
        private static List<string> family;
        private static List<string> humandrama;
        private static List<string> natureorcity;
        private static List<string> nowadaystechnology;
        private static List<string> camerawork;

        static Movie()
        {
            violent = new List<string>();
            firegun = new List<string>();
            gore = new List<string>();
            sex = new List<string>();
            nude = new List<string>();
            relationship = new List<string>();
            family = new List<string>();
            humandrama = new List<string>();
            natureorcity = new List<string>();
            nowadaystechnology = new List<string>();
            camerawork = new List<string>();

            camerawork.Add("photograph");
            camerawork.Add("slow motion scene");
            camerawork.Add("lens flare");
            camerawork.Add("subjective camera");
            camerawork.Add("character's point of view camera shot");

            nowadaystechnology.Add("cell phone");
            nowadaystechnology.Add("telephone call");
            nowadaystechnology.Add("helicopter");
            nowadaystechnology.Add("news report");
            nowadaystechnology.Add("watching tv");
            nowadaystechnology.Add("motorcycle");
            nowadaystechnology.Add("airplane");
            nowadaystechnology.Add("airport");

            natureorcity.Add("new york city");
            natureorcity.Add("snow");
            natureorcity.Add("rain");
            natureorcity.Add("beach");
            natureorcity.Add("forest");
            natureorcity.Add("los angeles california");
            natureorcity.Add("london england");
            natureorcity.Add("desert");
            natureorcity.Add("airport");

            humandrama.Add("death");
            humandrama.Add("violence");
            humandrama.Add("friendship");
            humandrama.Add("revenge");
            humandrama.Add("love");
            humandrama.Add("cigarette smoking");
            humandrama.Add("hospital");
            humandrama.Add("kidnapping");
            humandrama.Add("drunkenness");
            humandrama.Add("deception");
            humandrama.Add("betrayal");
            humandrama.Add("crying");
            humandrama.Add("infidelity");
            humandrama.Add("fear");
            humandrama.Add("money");
            humandrama.Add("drinking");
            humandrama.Add("marriage");
            humandrama.Add("drugs");
            humandrama.Add("teacher");

            family.Add("husband wife relationship");
            family.Add("father son relationship");
            family.Add("father daughter relationship");
            family.Add("mother son relationship");
            family.Add("mother daughter relationship");
            family.Add("family relationships");
            family.Add("brother sister relationship");
            family.Add("brother brother relationship");
            family.Add("pregnancy");
            family.Add("death of father");
            family.Add("wedding");
            family.Add("marriage");
            family.Add("sister sister relationship");
            family.Add("death of mother");

            relationship.Add("husband wife relationship");
            relationship.Add("father son relationship");
            relationship.Add("father daughter relationship");
            relationship.Add("mother son relationship");
            relationship.Add("friendship");
            relationship.Add("love");
            relationship.Add("mother daughter relationship");
            relationship.Add("boyfriend girlfriend relationship");
            relationship.Add("family relationships");
            relationship.Add("brother sister relationship");
            relationship.Add("character says i love you");
            relationship.Add("brother brother relationship");
            relationship.Add("pregnancy");
            relationship.Add("friend");
            relationship.Add("infidelity");
            relationship.Add("death of father");
            relationship.Add("death of friend");
            relationship.Add("teenage boy");
            relationship.Add("boy");
            relationship.Add("wedding");
            relationship.Add("marriage");
            relationship.Add("sister sister relationship");
            relationship.Add("jealousy");
            relationship.Add("death of mother");
            relationship.Add("school");

            nude.Add("female nudity");
            nude.Add("topless female nudity");
            nude.Add("bare breasts");
            nude.Add("male rear nudity");
            nude.Add("nudity");
            nude.Add("male nudity");
            nude.Add("female frontal nudity");
            nude.Add("female rear nudity");
            nude.Add("male frontal nudity");

            sex.Add("bare chested male");
            sex.Add("female nudity");
            sex.Add("kiss");
            sex.Add("topless female nudity");
            sex.Add("bare breasts");
            sex.Add("male rear nudity");
            sex.Add("nudity");
            sex.Add("sex");
            sex.Add("male nudity");
            sex.Add("sex scene");
            sex.Add("masturbation");
            sex.Add("kissing");
            sex.Add("prostitute");
            sex.Add("female frontal nudity");
            sex.Add("female rear nudity");
            sex.Add("male frontal nudity");

            gore.Add("blood");
            gore.Add("corpse");
            gore.Add("torture");
            gore.Add("gore");
            gore.Add("blood splatter");
            gore.Add("blood spatter");
            gore.Add("exploding body");
            gore.Add("decapitation");

            firegun.Add("pistol");
            firegun.Add("shot in the chest");
            firegun.Add("shot to death");
            firegun.Add("shot in the head");
            firegun.Add("shot in the back");
            firegun.Add("shotgun");
            firegun.Add("gun");
            firegun.Add("shot in the forehead");

            violent.Add("murder");
            violent.Add("blood");
            violent.Add("death");
            violent.Add("violence");
            violent.Add("pistol");
            violent.Add("punched in the face");
            violent.Add("revenge");
            violent.Add("shot in the chest");
            violent.Add("shot to death");
            violent.Add("shot in the head");
            violent.Add("corpse");
            violent.Add("knife");
            violent.Add("kidnapping");
            violent.Add("beating");
            violent.Add("shot in the back");
            violent.Add("martial arts");
            violent.Add("shotgun");
            violent.Add("torture");
            violent.Add("gore");
            violent.Add("blood splatter");
            violent.Add("gun");
            violent.Add("suicide");
            violent.Add("stabbed in the chest");
            violent.Add("stabbed to death");
            violent.Add("hostage");
            violent.Add("hand to hand combat");
            violent.Add("shot in the forehead");
            violent.Add("blood spatter");
            violent.Add("stabbed in the back");
            violent.Add("exploding body");
            violent.Add("decapitation");
            violent.Add("battle");
            violent.Add("impalement");
            violent.Add("severed head");
        }

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
                    movie.isViolent = (parameters[i++][0] == 'T') ? true : false;
                    movie.hasFireguns = (parameters[i++][0] == 'T') ? true : false;
                    movie.isGoreViolent = (parameters[i++][0] == 'T') ? true : false;
                    movie.hasSex = (parameters[i++][0] == 'T') ? true : false;
                    movie.hasNudeScenes = (parameters[i++][0] == 'T') ? true : false;
                    movie.aboutRelationships = (parameters[i++][0] == 'T') ? true : false;
                    movie.aboutFamily = (parameters[i++][0] == 'T') ? true : false;
                    movie.hasFlashbacks = (parameters[i++][0] == 'T') ? true : false;
                    movie.hasSurpriseEnding = (parameters[i++][0] == 'T') ? true : false;
                    movie.aboutHumanDrama = (parameters[i++][0] == 'T') ? true : false;
                    movie.aboutNatureOrCity = (parameters[i++][0] == 'T') ? true : false;
                    movie.hasNowadaysTechnology = (parameters[i++][0] == 'T') ? true : false;
                    movie.isSequel = (parameters[i++][0] == 'T') ? true : false;
                    movie.isBasedOnNovel = (parameters[i++][0] == 'T') ? true : false;
                    movie.isWrittenByDirector = (parameters[i++][0] == 'T') ? true : false;
                    movie.isIndependent = (parameters[i++][0] == 'T') ? true : false;
                    movie.hasCameraWorks = (parameters[i++][0] == 'T') ? true : false;
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
            fileLines.Add("@ATTRIBUTE duration {Short,Regular,Long,Very_Long}");
            fileLines.Add("@ATTRIBUTE awardedDirector {0,1}");
            fileLines.Add("@ATTRIBUTE oscarDirector {0,1}");
            fileLines.Add("@ATTRIBUTE awardedActor {0,1}");
            fileLines.Add("@ATTRIBUTE oscarActor {0,1}");
            fileLines.Add("@ATTRIBUTE genre {Drama,Horror,Action,Comedy,Others}");
            fileLines.Add("@ATTRIBUTE certificate {G, R, PG_13, PG, NOT_RATED}");
            fileLines.Add("@ATTRIBUTE rating {Very Bad, Bad,Regular,Good, Excellent}");

            //
            fileLines.Add("@DATA");

            string line = "";

            foreach (Movie movie in movies)
            {
                string runtime = "";
                if (movie.Runtime < 88.0f)
                {
                    runtime = "Short";
                }
                else if (movie.Runtime < 122.0f)
                {
                    runtime = "Regular";
                }
                else if (movie.Runtime < 160.0f)
                {
                    runtime = "Long";
                }
                else
                {
                    runtime = "Very_Long";
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
            if (DirectorUrl.Length != 0)
            {
                if (directors.ContainsKey(DirectorUrl))
                {
                    Director director = directors[DirectorUrl];
                    AwardedDirector = (director.Awards > 0) ? true : false;
                    OscarDirector = director.Oscar;
                }
            }

            if (ActorsUrl.Length != 0)
            {
                string[] actorsNameUrl = ActorsUrl.Split('@');

                foreach (string actorUrl in actorsNameUrl)
                {
                    if (actors.ContainsKey("n" + actorUrl))
                    {
                        Actor actor = actors["n" + actorUrl];
                        AwardedActors |= (actor.Awards > 0) ? true : false;
                        OscarActors |= actor.Oscar;
                    }
                }
            }
        }

        public static void ExportMoviesToWeka(Dictionary<string, Movie> movies, string destination)
        {
            List<string> fileLines = new List<string>();

            fileLines.Add("@RELATION movie");
            fileLines.Add("@ATTRIBUTE duration {Short,Regular,Long,Very_Long}");
            fileLines.Add("@ATTRIBUTE awardedDirector {0,1}");
            fileLines.Add("@ATTRIBUTE oscarDirector {0,1}");
            fileLines.Add("@ATTRIBUTE awardedActor {0,1}");
            fileLines.Add("@ATTRIBUTE oscarActor {0,1}");
            fileLines.Add("@ATTRIBUTE genre {Drama,Horror,Action,Comedy,Others}");
            fileLines.Add("@ATTRIBUTE certificate {G, R, PG_13, PG, NOT_RATED}");
            fileLines.Add("@ATTRIBUTE isViolent {0,1}");
            fileLines.Add("@ATTRIBUTE hasFireguns {0,1}");
            fileLines.Add("@ATTRIBUTE isGoreViolent {0,1}");
            fileLines.Add("@ATTRIBUTE hasSex {0,1}");
            fileLines.Add("@ATTRIBUTE hasNudeScenes {0,1}");
            fileLines.Add("@ATTRIBUTE aboutRelationships {0,1}");
            fileLines.Add("@ATTRIBUTE aboutFamily {0,1}");
            fileLines.Add("@ATTRIBUTE hasFlashbacks {0,1}");
            fileLines.Add("@ATTRIBUTE hasSurpriseEnding {0,1}");
            fileLines.Add("@ATTRIBUTE aboutHumanDrama {0,1}");
            fileLines.Add("@ATTRIBUTE aboutNatureOrCity {0,1}");
            fileLines.Add("@ATTRIBUTE hasNowadaysTechnology {0,1}");
            fileLines.Add("@ATTRIBUTE isSequel {0,1}");
            fileLines.Add("@ATTRIBUTE isBasedOnNovel {0,1}");
            fileLines.Add("@ATTRIBUTE isWrittenByDirector {0,1}");
            fileLines.Add("@ATTRIBUTE isIndependent {0,1}");
            fileLines.Add("@ATTRIBUTE hasCameraWorks {0,1}");
            fileLines.Add("@ATTRIBUTE rating {Bad,Regular,Good}");
            fileLines.Add("@DATA");

            string line = "";
            foreach (string key in movies.Keys)
            {
                Movie movie = movies[key];

                string runtime = "";
                if (movie.Runtime < 88.0f)
                {
                    runtime = "Short";
                }
                else if (movie.Runtime < 122.0f)
                {
                    runtime = "Regular";
                }
                else if (movie.Runtime < 160.0f)
                {
                    runtime = "Long";
                }
                else
                {
                    runtime = "Very_Long";
                }
                line += runtime + ",";

                line += ((movie.AwardedDirector) ? "1" : "0") + ",";
                line += ((movie.OscarDirector) ? "1" : "0") + ",";
                line += ((movie.AwardedActors) ? "1" : "0") + ",";
                line += ((movie.OscarActors) ? "1" : "0") + ",";

                string genre = "";
                string actualGenre = movie.Genre.Split('@')[0];
                #region defining genre
                if (actualGenre.Equals("Drama") || actualGenre.Equals("Family") || actualGenre.Equals("Romance") || actualGenre.Equals("History") || actualGenre.Equals("Reality-TV") || actualGenre.Equals("Adult") || actualGenre.Equals("Biography"))
                {
                    genre = "Drama";
                }
                else if (actualGenre.Equals("Horror") || actualGenre.Equals("Thriller") || actualGenre.Equals("Mistery"))
                {
                    genre = "Horror";
                }
                else if (actualGenre.Equals("Action") || actualGenre.Equals("Adventure") || actualGenre.Equals("Crime") || actualGenre.Equals("Sci-Fi") || actualGenre.Equals("Fantasy") || actualGenre.Equals("War") || actualGenre.Equals("Western") || actualGenre.Equals("Sport"))
                {
                    genre = "Action";
                }
                else if (actualGenre.Equals("Comedy"))
                {
                    genre = "Comedy";
                }
                else
                {
                    genre = "Others";
                }
                #endregion

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

                //isViolent + ";" + hasFireguns + ";" + isGoreViolent + ";" + hasSex + ";" + hasNudeScenes + ";" + 
                //aboutRelationships + ";" + aboutFamily + ";" + hasFlashbacks + ";" + hasSurpriseEnding + ";" + 
                //aboutHumanDrama + ";" + aboutNatureOrCity + ";" + hasNowadaysTechnology + ";" + isSequel + ";" + 
                //isBasedOnNovel + ";" + isWrittenByDirector + ";" + isIndependent + ";" + hasCameraWorks;

                line += ((movie.IsViolent) ? "1" : "0") + ",";
                line += ((movie.HasFireguns) ? "1" : "0") + ",";
                line += ((movie.IsGoreViolent) ? "1" : "0") + ",";
                line += ((movie.HasSex) ? "1" : "0") + ",";
                line += ((movie.HasNudeScenes) ? "1" : "0") + ",";
                line += ((movie.AboutRelationships) ? "1" : "0") + ",";
                line += ((movie.AboutFamily) ? "1" : "0") + ",";
                line += ((movie.HasFlashbacks) ? "1" : "0") + ",";
                line += ((movie.HasSurpriseEnding) ? "1" : "0") + ",";
                line += ((movie.AboutHumanDrama) ? "1" : "0") + ",";
                line += ((movie.AboutNatureOrCity) ? "1" : "0") + ",";
                line += ((movie.HasNowadaysTechnology) ? "1" : "0") + ",";
                line += ((movie.IsSequel) ? "1" : "0") + ",";
                line += ((movie.IsBasedOnNovel) ? "1" : "0") + ",";
                line += ((movie.IsWrittenByDirector) ? "1" : "0") + ",";
                line += ((movie.IsIndependent) ? "1" : "0") + ",";
                line += ((movie.HasCameraWorks) ? "1" : "0") + ",";

                line += rating;
                fileLines.Add(line);
                line = "";
            }

            FileAO.ExportToArff(fileLines, destination);
        }

        public List<string> GetTags()
        {
            List<string> tags = new List<string>();

            string htmlPage = WebAO.CodeHtml("http://www.imdb.com/title/"+NameUrl+"/keywords?ref_=ttpl_sa_3");
            string[] pageLines = htmlPage.Split('\n');

            for (int i = 0; i < pageLines.Length; i++)
            {
                string line = pageLines[i].Trim();
                if (line.Contains("<a href=\"/keyword"))
                {
                    i++;
                    line = pageLines[i].Trim();

                    string tag = line.Substring(1);
                    int pos = tag.IndexOf("</");
                    tag = tag.Substring(0, pos);
                    tags.Add(tag);
                    Console.WriteLine();
                }
            }

            if (tags.Count > 0)
            {
                foreach (string tag in tags)
                {
                    if (violent.Contains(tag))
                    {
                        IsViolent = true;
                    }
                    if (firegun.Contains(tag))
                    {
                        hasFireguns = true;
                    }
                    if (gore.Contains(tag))
                    {
                        isGoreViolent = true;
                    }
                    if (sex.Contains(tag))
                    {
                        hasSex = true;
                    }
                    if (nude.Contains(tag))
                    {
                        hasNudeScenes = true;
                    }
                    if (relationship.Contains(tag))
                    {
                        aboutRelationships = true;
                    }
                    if (family.Contains(tag))
                    {
                        aboutFamily = true;
                    }
                    if (tag.Contains("flashback"))
                    {
                        hasFlashbacks = true;
                    }
                    if (tag.Contains("surprise ending"))
                    {
                        hasSurpriseEnding = true;
                    }
                    if (humandrama.Contains(tag))
                    {
                        aboutHumanDrama = true;
                    }
                    if (nowadaystechnology.Contains(tag))
                    {
                        hasNowadaysTechnology = true;
                    }
                    if (tag.Contains("sequel"))
                    {
                        isSequel = true;
                    }
                    if (tag.Contains("based on novel"))
                    {
                        isBasedOnNovel = true;
                    }
                    if (tag.Contains("written by director"))
                    {
                        isWrittenByDirector = true;
                    }
                    if (tag.Contains("independent"))
                    {
                        isIndependent = true;
                    }
                    if (camerawork.Contains(tag))
                    {
                        hasCameraWorks = true;
                    }
                }
            }

            return tags;
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

        public string ToStringDetailedTags()
        {
            return Name + ";" + NameUrl + ";" + Rating + ";" + Director + ";" + DirectorUrl + ";" + Actors + ";" + ActorsUrl + ";" + Genre + ";" + Certificate + ";" + Runtime + ";" + AwardedDirector + ";" + OscarDirector + ";" + AwardedActors + ";" + OscarActors + ";" + isViolent + ";" + hasFireguns + ";" + isGoreViolent + ";" + hasSex + ";" + hasNudeScenes + ";" + aboutRelationships + ";" + aboutFamily + ";" + hasFlashbacks + ";" + hasSurpriseEnding + ";" + aboutHumanDrama + ";" + aboutNatureOrCity + ";" + hasNowadaysTechnology + ";" + isSequel + ";" + isBasedOnNovel + ";" + isWrittenByDirector + ";" + isIndependent + ";" + hasCameraWorks;
        }

        public string ToStringDetailedBusiness()
        {
            return Name + ";" + NameUrl + ";" + Rating + ";" + Director + ";" + DirectorUrl + ";" + Actors + ";" + ActorsUrl + ";" + Genre + ";" + Certificate + ";" + Runtime + ";" + AwardedDirector + ";" + OscarDirector + ";" + AwardedActors + ";" + OscarActors + ";" + Budget + ";" + EnglishName;
        }
    }
}
