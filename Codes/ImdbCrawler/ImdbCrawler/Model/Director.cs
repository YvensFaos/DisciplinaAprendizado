using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using ImdbCrawler.Utils;

namespace ImdbCrawler.Model
{
    public class Director
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
        private int awards;

        public int Awards
        {
            get { return awards; }
            set { awards = value; }
        }
        private bool oscar;

        public bool Oscar
        {
            get { return oscar; }
            set { oscar = value; }
        }
        #endregion

        public Director()
        {
            awards = 0;
            oscar = false;
        }

        public void GetInfo()
        {
            string url = "http://www.imdb.com/name/n@/awards?ref_=nm_ql_2";
            url = url.Replace("@", nameUrl);
            string htmlPage = WebAO.CodeHtml(url);
            string[] pageLines = htmlPage.Split('\n');

            int i = 0;
            for (i = 0; i < pageLines.Length; i++)
            {
                string line = pageLines[i].Trim();
                if (line.Contains("<div class=\"desc\">Showing"))
                {
                    int index1 = line.IndexOf(" all ");
                    int index2 = line.IndexOf(" wins ");

                    awards = int.Parse(line.Substring(index1 + " all ".Length, index2 - (index1 + " all ".Length)));
                }
                if (line.Contains("<td class=\"award_outcome\" rowspan=\"2\">"))
                {
                    i++;
                    line = pageLines[i].Trim();
                    bool won = false;
                    if (line.Contains("Won"))
                    {
                        won = true;
                    }
                    i++;
                    line = pageLines[i].Trim();
                    Console.WriteLine(line);
                    if (line.Contains("Oscar") && won)
                    {
                        Oscar = true;
                    }
                }
            }
        }

        public static List<Director> ReadDirectorsFromCSV(string file)
        {
            List<Director> directors = new List<Director>();

            string[] lines = FileAO.ReadFile(file);

            foreach (string line in lines)
            {
                string[] parameters = line.Split(';');

                Director director = new Director();

                int i = 0;
                director.Name = parameters[i++];
                director.NameUrl = parameters[i++];
                director.Awards = int.Parse(parameters[i++]);
                director.Oscar = (parameters[i++][0] == 'F') ? false : true;

                directors.Add(director);
            }

            return directors;
        }

        public override string ToString()
        {
            return Name + ";" + NameUrl + ";" + Awards + ";" + Oscar;
        }
    }
}
