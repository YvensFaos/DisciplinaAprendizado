﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}