using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using ImdbCrawler.Utils;

namespace ImdbCrawler.Model
{
    public class Genre
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Genre(string Name)
        {
            this.Name = Name;
        }

        public static void GenreStatistics(string filepath)
        {
            string[] lines = FileAO.ReadFile(filepath);
            GenreStatistics(lines);
        }

        public static void GenreStatistics(string[] lines)
        {
            Dictionary<string, int> statistics = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                if (statistics.ContainsKey(line))
                {
                    statistics[line] += 1;
                }
                else
                {
                    statistics.Add(line, 1);
                }
            }

            foreach (string key in statistics.Keys)
            {
                Console.WriteLine("Genre " + key + ": " + statistics[key]);
            }
        }
    }
}
