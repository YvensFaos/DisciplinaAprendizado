using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using ImdbCrawler.Model;

namespace ImdbCrawler.Utils
{
    public class FileAO
    {
        public static void ExportMoviesToCSV(List<Movie> movies, string destination)
        {
            foreach (Movie movie in movies)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
                {
                    file.WriteLine(movie.ToString());
                }
            }
        }

        public static void ExportMoviesDetailedToCSV(List<Movie> movies, string destination)
        {
            foreach (Movie movie in movies)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
                {
                    file.WriteLine(movie.ToStringDetailed());
                }
            }
        }

        public static void ExportMoviesDetailedBusinessToCSV(List<Movie> movies, string destination)
        {
            foreach (Movie movie in movies)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
                {
                    file.WriteLine(movie.ToStringDetailedBusiness());
                }
            }
        }

        public static void ExportToArff(List<string> fileLines, string destination)
        {
            int counter = 0;
            foreach (string line in fileLines)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
                {
                    file.WriteLine(line);
                    Console.WriteLine(++counter + "/ " + fileLines.Count);
                }
            }
        }

        public static void ExportDirectorsToCSV(List<Director> directors, string destination)
        {
            foreach (Director director in directors)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
                {
                    file.WriteLine(director.ToString());
                }
            }
        }

        public static void ExportDirectorToCSV(Director director, string destination)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
            {
                file.WriteLine(director.ToString());
            }
        }

        public static void ExportActorsToCSV(List<Actor> actors, string destination)
        {
            foreach (Actor actor in actors)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
                {
                    file.WriteLine(actor.ToString());
                }
            }
        }

        public static void ExportActorToCSV(Actor actor, string destination)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
            {
                file.WriteLine(actor.ToString());
            }
        }

        public static void ExportTagsToTxt(Dictionary<string, int> tags, string destination)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(destination, true))
            {
                foreach (string tag in tags.Keys)
                {
                    file.WriteLine(tag+";"+tags[tag]);
                }
            }
        }

        public static string[] ReadFile(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            return lines;
        }
    }
}
