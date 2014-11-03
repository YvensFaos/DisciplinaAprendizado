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
    public class Actor
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
            set 
            {
                if (value[0] != 'n')
                {
                    value = 'n' + value;
                }
                if (value.IndexOf('m') != value.LastIndexOf('m'))
                {
                    value = value.Substring(0, value.LastIndexOf('m'));
                }
                nameUrl = value; 
            }
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

        public Actor()
        {
            awards = 0;
            oscar = false;
        }

        public void GetInfo()
        {
            string url = "http://www.imdb.com/name/@/awards?ref_=nm_ql_2";
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
                    if (line.Contains("Oscar") && won)
                    {
                        Oscar = true;
                    }
                }
            }
        }

        public static List<Actor> ReadDirectorsFromCSV(string file)
        {
            List<Actor> actors = new List<Actor>();

            string[] lines = File.ReadAllLines(file);

            foreach (string line in lines)
            {
                string[] parameters = line.Split(';');

                Actor actor = new Actor();

                int i = 0;
                actor.Name = parameters[i++];
                actor.NameUrl = parameters[i++];
                actor.Awards = int.Parse(parameters[i++]);
                actor.Oscar = (parameters[i++][0] == 'F') ? false : true;

                actors.Add(actor);
            }

            return actors;
        }


        public override string ToString()
        {
            return Name + ";" + NameUrl + ";" + Awards + ";" + Oscar;
        }
    }
}
