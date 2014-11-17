using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using System.Windows.Forms.DataVisualization.Charting;

using ImdbCrawler.Model;
using ImdbCrawler.Utils;

namespace ImdbCrawlerDesktop
{
    public partial class ICMainWindow : Form
    {
        private Dictionary<string, Movie> hashMovies;

        private Semaphore logMutex;
        private Semaphore moviesMutex;

        public ICMainWindow()
        {
            InitializeComponent();

            comboBoxAction.SelectedIndex = 0;
            comboBoxCharts.SelectedIndex = 0;
            comboBoxSortBy.SelectedIndex = 0;

            comboBoxCharts.SelectedIndexChanged += new System.EventHandler(ComboBoxCharts_SelectedIndexChanged);

            textBoxCustomURL.Text = "http://www.imdb.com/search/title?at=0&sort=user_rating&title_type=feature&year=2009,2009";

            CheckForIllegalCrossThreadCalls = false;

            hashMovies = new Dictionary<string, Movie>();

            logMutex = new Semaphore(0, 1);
            logMutex.Release();
            moviesMutex = new Semaphore(0, 1);
            moviesMutex.Release();
        }

        private void buttonFindMovies_Click(object sender, EventArgs e)
        {
            int startIndex = int.Parse(textBoxStartIndex.Text);
            int finalIndex = int.Parse(textBoxLastIndex.Text);
            if (finalIndex <= startIndex)
            {
                finalIndex = startIndex;
            }

            string asc = (radioButtonAsc.Checked) ? "asc" : "desc";
            string sortBy = comboBoxSortBy.SelectedItem.ToString();
            for (int index = startIndex; index <= finalIndex; index += 50)
            {
                string url = "http://www.imdb.com/search/title?at=0&sort=" + sortBy + "," + asc + "&start=" + index + "&title_type=feature&year=" + textBoxYear1.Text + "," + textBoxYear2.Text;

                Thread thread = new Thread(() => GetMovies(url));
                thread.Start();
            }
        }

        private void buttonFindCustom_Click(object sender, EventArgs e)
        {
            string url = textBoxCustomURL.Text;
            Thread thread = new Thread(() => GetMovies(url));
            thread.Start();
        }

        private void buttonClearCustom_Click(object sender, EventArgs e)
        {
            textBoxCustomURL.Text = "";
        }

        private void ComboBoxCharts_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (comboBoxCharts.SelectedIndex)
            {
                case 0:
                    updateCharts(ChartType.MOVIES_BY_RATING);
                    break;
            }
        }

        private void updateCharts(ChartType chartType)
        {
            ChartArea area = chartArea.ChartAreas[0];

            switch (chartType)
            {
                case ChartType.MOVIES_BY_RATING:
                    {
                        chartArea.Titles.Clear();
                        chartArea.Titles.Add(new Title("Filmes por classificação (nota)"));
                        chartArea.Series.Clear();

                        int bad = 0, regular = 0, good = 0;

                        Series movieSeries = new Series("Classificação");
                        foreach (string key in hashMovies.Keys)
                        {
                            Movie movie = hashMovies[key];
                            movie.GetClassification();

                            switch (movie.Classification)
                            {
                                case "Bad":
                                    bad++; break;
                                case "Regular":
                                    regular++; break;
                                case "Good":
                                    good++; break;
                            }
                        }

                        movieSeries.Points.AddXY(1, bad);
                        movieSeries.Points.AddXY(2, regular);
                        movieSeries.Points.AddXY(3, good);

                        area.RecalculateAxesScale();
                        
                        area.AxisX.Interval = 1;
                        area.AxisX.Maximum = 4;

                        area.AxisX.TextOrientation = TextOrientation.Horizontal;
                        area.AxisX.CustomLabels.Add(0.9, 1.1, "Bad");
                        area.AxisX.CustomLabels.Add(1.9, 2.1, "Regular");
                        area.AxisX.CustomLabels.Add(2.9, 3.1, "Good");

                        if (logging())
                        {
                            logInfo("Qtde. Bad:     " + bad);
                            logInfo("Qtde. Regular: " + regular);
                            logInfo("Qtde. Good:    " + good);
                        }

                        movieSeries.ChartType = SeriesChartType.Column;

                        chartArea.Series.Add(movieSeries);
                    }
                    break;
            }
        }

        private void logInfo(string message)
        {
            logMutex.WaitOne();
            message = DateTime.Now.ToString("%h:mm:ss") + " >  " + message + "\r\n";
            textBoxLog.Text = message + textBoxLog.Text;
            logMutex.Release();
        }

        public bool logging()
        {
            return checkBoxLog.Checked;
        }

        public void updateMoviesCount(int count)
        {
            labelTotalMovies.Text = count.ToString();
        }

        private void GetMovies(string url)
        {
            if (logging())
            {
                logInfo("Busca customizada, URL: " + url);
            }
            string htmlPage = WebAO.CodeHtml(url);

            List<Movie> movies = Movie.ReadMoviesFromUrl(htmlPage);
            if (logging())
            {
                logInfo("Filmes encontrados: " + movies.Count);
            }

            moviesMutex.WaitOne();
            foreach (Movie movie in movies)
            {
                if (!hashMovies.ContainsKey(movie.NameUrl))
                {
                    hashMovies.Add(movie.NameUrl, movie);
                }
            }

            updateCharts(ChartType.MOVIES_BY_RATING);
            updateMoviesCount(hashMovies.Count);
            moviesMutex.Release();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            
            saveFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            string path = "";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
            }

            List<Movie> movies = new List<Movie>();
            foreach (string key in hashMovies.Keys)
            {
                movies.Add(hashMovies[key]);
            }
            FileAO.ExportMoviesToCSV(movies, path);
        }
    }
}
