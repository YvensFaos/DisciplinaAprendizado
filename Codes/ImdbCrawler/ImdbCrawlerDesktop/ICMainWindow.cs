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
        private Dictionary<string, Director> hashDirectors;
        private Dictionary<string, Actor> hashActors;
        private Dictionary<string, int> hashTags;

        private Semaphore logMutex;
        private Semaphore moviesMutex;
        private Semaphore directorsMutex;
        private Semaphore actorsMutex;

        public ICMainWindow()
        {
            InitializeComponent();

            comboBoxAction.SelectedIndex = 3;
            comboBoxCharts.SelectedIndex = 0;
            comboBoxSortBy.SelectedIndex = 0;

            comboBoxCharts.SelectedIndexChanged += new System.EventHandler(ComboBoxCharts_SelectedIndexChanged);

            textBoxCustomURL.Text = "http://www.imdb.com/search/title?at=0&sort=user_rating&title_type=feature&year=2009,2009";

            CheckForIllegalCrossThreadCalls = false;

            hashMovies = new Dictionary<string, Movie>();
            hashDirectors = new Dictionary<string, Director>();
            hashActors = new Dictionary<string, Actor>();
            hashTags = new Dictionary<string, int>();

            logMutex = new Semaphore(0, 1);
            logMutex.Release();
            moviesMutex = new Semaphore(0, 1);
            moviesMutex.Release();
            directorsMutex = new Semaphore(0, 1);
            directorsMutex.Release();
            actorsMutex = new Semaphore(0, 1);
            actorsMutex.Release();
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
            moviesMutex.WaitOne();
            switch (comboBoxCharts.SelectedIndex)
            {
                case 0:
                    updateCharts(ChartType.MOVIES_BY_RATING);
                    break;
                case 1:
                    updateCharts(ChartType.ACTORS_AWARDED);
                    break;
                case 2:
                    updateCharts(ChartType.DIRECTORS_AWARDED);
                    break;
                case 3:
                    updateCharts(ChartType.MOVIE_BY_GENRE);
                    break;
            }
            moviesMutex.Release();
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

                        area.AxisX.CustomLabels.Clear();
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
                case ChartType.MOVIE_BY_GENRE:
                    {
                        chartArea.Titles.Clear();
                        chartArea.Titles.Add(new Title("Filmes por gênero"));
                        chartArea.Series.Clear();

                        int drama = 0, horror = 0, action = 0, comedy = 0, others = 0;

                        Series movieSeries = new Series("Classificação");
                        foreach (string key in hashMovies.Keys)
                        {
                            Movie movie = hashMovies[key];
                            movie.GetMovieGenre();

                            switch (movie.Genre)
                            {
                                case "Drama":
                                    drama++; break;
                                case "Horror":
                                    horror++; break;
                                case "Action":
                                    action++; break;
                                case "Comedy":
                                    comedy++; break;
                                case "Others":
                                    others++; break;
                            }
                        }

                        int i = 1;
                        movieSeries.Points.AddXY(i++, drama);
                        movieSeries.Points.AddXY(i++, horror);
                        movieSeries.Points.AddXY(i++, action);
                        movieSeries.Points.AddXY(i++, comedy);
                        movieSeries.Points.AddXY(i++, others);

                        area.RecalculateAxesScale();

                        area.AxisX.Interval = 1;
                        area.AxisX.Maximum = 6;

                        double j = 1.0;
                        area.AxisX.CustomLabels.Clear();
                        area.AxisX.TextOrientation = TextOrientation.Horizontal;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Drama");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Horror");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Action");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Comedy");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Others");
                        j += 1.0;

                        if (logging())
                        {
                            logInfo("Qtde. Drama:  " + drama);
                            logInfo("Qtde. Horror: " + horror);
                            logInfo("Qtde. Action: " + action);
                            logInfo("Qtde. Comedy: " + comedy);
                            logInfo("Qtde. Others: " + others);
                        }

                        movieSeries.ChartType = SeriesChartType.Column;

                        chartArea.Series.Add(movieSeries);
                    }
                    break;
                case ChartType.ACTORS_AWARDED:
                    {
                        chartArea.Titles.Clear();
                        chartArea.Titles.Add(new Title("Atores Premiados"));
                        chartArea.Series.Clear();

                        int nonawarded = 0, awarded = 0, oscar = 0;

                        Series actorSeries = new Series("Atores");
                        foreach (string key in hashActors.Keys)
                        {
                            Actor actor = hashActors[key];

                            if (actor.Oscar)
                            {
                                oscar++;
                            }
                            else if (actor.Awards > 0)
                            {
                                awarded++;
                            }
                            else
                            {
                                nonawarded++;
                            }

                        }

                        int i = 1;
                        actorSeries.Points.AddXY(i++, nonawarded);
                        actorSeries.Points.AddXY(i++, awarded);
                        actorSeries.Points.AddXY(i++, oscar);

                        area.RecalculateAxesScale();

                        area.AxisX.Interval = 1;
                        area.AxisX.Maximum = 4;

                        double j = 1.0;
                        area.AxisX.CustomLabels.Clear();
                        area.AxisX.TextOrientation = TextOrientation.Horizontal;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Sem Prêmio");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Premiado");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Oscar");
                        j += 1.0;

                        if (logging())
                        {
                            logInfo("Qtde. Sem Prêmio: " + nonawarded);
                            logInfo("Qtde. Premiado:   " + awarded);
                            logInfo("Qtde. Oscar:      " + oscar);
                        }

                        actorSeries.ChartType = SeriesChartType.Column;

                        chartArea.Series.Add(actorSeries);
                    }
                    break;
                case ChartType.DIRECTORS_AWARDED:
                    {
                        chartArea.Titles.Clear();
                        chartArea.Titles.Add(new Title("Diretores Premiados"));
                        chartArea.Series.Clear();

                        int nonawarded = 0, awarded = 0, oscar = 0;

                        Series directorSeries = new Series("Diretores");
                        foreach (string key in hashDirectors.Keys)
                        {
                            Director director = hashDirectors[key];

                           
                            if (director.Oscar)
                            {
                                oscar++;
                            }
                            else if (director.Awards > 0)
                            {
                                awarded++;
                            }
                            else
                            {
                                nonawarded++;
                            }

                        }

                        int i = 1;
                        directorSeries.Points.AddXY(i++, nonawarded);
                        directorSeries.Points.AddXY(i++, awarded);
                        directorSeries.Points.AddXY(i++, oscar);

                        area.RecalculateAxesScale();

                        area.AxisX.Interval = 1;
                        area.AxisX.Maximum = 4;

                        double j = 1.0;
                        area.AxisX.CustomLabels.Clear();
                        area.AxisX.TextOrientation = TextOrientation.Horizontal;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Sem Prêmio");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Premiado");
                        j += 1.0;
                        area.AxisX.CustomLabels.Add(j - 0.1, j + 0.1, "Oscar");
                        j += 1.0;

                        if (logging())
                        {
                            logInfo("Qtde. Sem Prêmio: " + nonawarded);
                            logInfo("Qtde. Premiado:   " + awarded);
                            logInfo("Qtde. Oscar:      " + oscar);
                        }

                        directorSeries.ChartType = SeriesChartType.Column;

                        chartArea.Series.Add(directorSeries);
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

        public bool tempFile()
        {
            return checkBoxTempFile.Checked;
        }

        public void updateMoviesCount(int count)
        {
            labelTotalMovies.Text = count.ToString();
        }

        public void updateDirectorsCount(int count)
        {
            labelTotalDirectors.Text = count.ToString();
        }

        public void updateActorsCount(int count)
        {
            labelTotalActors.Text = count.ToString();
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

            updateMoviesCount(hashMovies.Count);
            moviesMutex.Release();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            
            saveFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            string path = "";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;

                List<Movie> movies = new List<Movie>();
                foreach (string key in hashMovies.Keys)
                {
                    movies.Add(hashMovies[key]);
                }
                FileAO.ExportMoviesToCSV(movies, path);
            }
        }

        private void buttonOpenMovieList_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string path = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;

                List<Movie> movies = Movie.ReadMoviesFromCSV(path);

                moviesMutex.WaitOne();
                foreach (Movie movie in movies)
                {
                    if (!hashMovies.ContainsKey(movie.NameUrl))
                    {
                        hashMovies.Add(movie.NameUrl, movie);
                    }
                }

                updateMoviesCount(hashMovies.Count);
                moviesMutex.Release();
            }
        }

        private void buttonOpenDirectorsList_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string path = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                if (logging())
                {
                    logInfo("Caminho do arquivo de diretores: " + path);
                }
                List<Director> directors = Director.ReadDirectorsFromCSV(path);

                directorsMutex.WaitOne();
                foreach (Director director in directors)
                {
                    if (!hashDirectors.ContainsKey(director.NameUrl))
                    {
                        hashDirectors.Add(director.NameUrl, director);
                    }
                }

                if (logging())
                {
                    logInfo("Leitura de diretores concluída!");
                }
                updateDirectorsCount(hashDirectors.Count);
                directorsMutex.Release();
            }
        }

        private void buttonOpenActorsList_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string path = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                if (logging())
                {
                    logInfo("Caminho do arquivo de atores: " + path);
                }
                List<Actor> actors = Actor.ReadActorsFromCSV(path);

                actorsMutex.WaitOne();
                foreach (Actor actor in actors)
                {
                    if (!hashActors.ContainsKey(actor.NameUrl))
                    {
                        hashActors.Add(actor.NameUrl, actor);
                    }
                }

                if (logging())
                {
                    logInfo("Leitura de atores concluída!");
                }
                updateActorsCount(hashActors.Count);
                actorsMutex.Release();
            }
        }

        private void buttonExportActors_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            string path = "";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;

                List<Actor> actors = new List<Actor>();
                foreach (string key in hashActors.Keys)
                {
                    actors.Add(hashActors[key]);
                }
                FileAO.ExportActorsToCSV(actors, path);
            }
        }

        private void buttonExportDirectors_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            string path = "";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog.FileName;

                List<Director> directors = new List<Director>();
                foreach (string key in hashDirectors.Keys)
                {
                    directors.Add(hashDirectors[key]);
                }
                FileAO.ExportDirectorsToCSV(directors, path);
            }
        }

        private void buttonExecuteAction_Click(object sender, EventArgs e)
        {
            if (comboBoxAction.SelectedIndex == 0)
            {
                Thread thread = new Thread(() => GetActors());
                thread.Start();
            }
            else if (comboBoxAction.SelectedIndex == 1)
            {
                Thread thread = new Thread(() => GetDirectors());
                thread.Start();
            }
            else if (comboBoxAction.SelectedIndex == 2)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Reset();
                saveFileDialog.InitialDirectory = @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\";
                saveFileDialog.Filter = "arff files (*.arff)|*.arff";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                string path = "";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog.FileName;
                    Thread thread = new Thread(() => ExportToWeka(path));
                    thread.Start();
                }
            }
            else if (comboBoxAction.SelectedIndex == 3)
            {
                Thread thread = new Thread(() => GetMovieTags());
                thread.Start();
            }
            else if (comboBoxAction.SelectedIndex == 4)
            {
                Thread thread = new Thread(() => MoviesArithmetic());
                thread.Start();
            }
        }

        private void GetActors()
        {
            int counter = 0;
            int actualActorsCount = hashActors.Count;
            progressBarProcess.Value = 0;

            foreach (string key in hashMovies.Keys)
            {
                Movie movie = hashMovies[key];

                progressBarProcess.Value = ((int)((counter / (float)hashMovies.Count) * 100.0f));

                if (movie.Actors.Length != 0)
                {
                    string[] actorsName = movie.Actors.Split('@');
                    string[] actorsUrl = movie.ActorsUrl.Split('@');

                    for (int i = 0; i < actorsName.Length; i++)
                    {
                        Actor actor = new Actor();
                        actor.Name = actorsName[i];
                        actor.NameUrl = actorsUrl[i];

                        if (!hashActors.ContainsKey(actor.NameUrl))
                        {
                            actor.GetInfo();
                            actorsMutex.WaitOne();
                            hashActors.Add(actor.NameUrl, actor);
                            if (tempFile())
                            {
                                FileAO.ExportActorToCSV(actor, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\temp_actors.txt");
                            }
                            actorsMutex.Release();
                        }
                    }
                }
                counter++;
            }

            progressBarProcess.Value = 100;
            actualActorsCount = hashActors.Count - actualActorsCount;
            if (logging())
            {
                logInfo("Atores adicionados: " + actualActorsCount);
            }
        }

        private void GetDirectors()
        {
            int counter = 0;
            int actualDirectorsCount = hashDirectors.Count;
            progressBarProcess.Value = 0;

            foreach (string key in hashMovies.Keys)
            {
                Movie movie = hashMovies[key];

                progressBarProcess.Value = ((int)((counter / (float)hashMovies.Count) * 100.0f));

                if (movie.Director.Length != 0)
                {
                    Director director = new Director();
                    director.Name = movie.Director;
                    director.NameUrl = movie.DirectorUrl;

                    if (!hashDirectors.ContainsKey(director.NameUrl))
                    {
                        director.GetInfo();

                        directorsMutex.WaitOne();
                        hashDirectors.Add(director.NameUrl, director);
                        if (tempFile())
                        {
                            FileAO.ExportDirectorToCSV(director, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\temp_directors.txt");
                        }
                        directorsMutex.Release();
                    }
                }
                counter++;
            }

            progressBarProcess.Value = 100;
            actualDirectorsCount = hashDirectors.Count - actualDirectorsCount;
            if (logging())
            {
                logInfo("Diretores adicionados: " + actualDirectorsCount);
            }
        }

        private void ExportToWeka(string destination)
        {
            if (logging())
            {
                logInfo("Exportando para WEKA.");
            }
            int counter = 0;

            foreach (string key in hashMovies.Keys)
            {
                hashMovies[key].GetDetailedInfo(hashActors, hashDirectors);
                progressBarProcess.Value = ((int)((counter / (float)hashMovies.Count) * 50.0f));
            }

            Movie.ExportMoviesToWeka(hashMovies, destination);

            progressBarProcess.Value = 100;

            if (logging())
            {
                logInfo("Exportado para: " + destination);
            }
        }

        private void MoviesArithmetic()
        {
            if (logging())
            {
                logInfo("Fazendo cálculos com os filmes.");
            }
            progressBarProcess.Value = 0;

            float mean = 0;
            int counter = 0;
            foreach (string key in hashMovies.Keys)
            {
                mean += hashMovies[key].Rating;
                progressBarProcess.Value = ((int)((counter++ / (float)hashMovies.Count) * 100.0f));
            }
            mean /= hashMovies.Count;

            logInfo("Média das notas: " + mean);

            float standard = 0;
            counter = 0;
            progressBarProcess.Value = 0;
            foreach (string key in hashMovies.Keys)
            {
                standard += (float)Math.Pow((double)(hashMovies[key].Rating - mean), 2.0f);
                progressBarProcess.Value = ((int)((counter++ / (float)hashMovies.Count) * 100.0f));
            }

            standard /= hashMovies.Count;
            standard = (float)Math.Sqrt((double)standard);
            logInfo("Desvio padrão das notas: " + standard);

            progressBarProcess.Value = 100;
        }

        private void GetMovieTags()
        {
            if (logging())
            {
                logInfo("Carregando tags dos filmes.");
            }
            int counter = 0;
            int actual = progressBarProcess.Value - 1;

            try
            {
                foreach (string key in hashMovies.Keys)
                {
                    List<string> tags = hashMovies[key].GetTags();
                    progressBarProcess.Value = ((int)((counter++ / (float)hashMovies.Count) * 100.0f));

                    foreach (string tag in tags)
                    {
                        if (hashTags.ContainsKey(tag))
                        {
                            hashTags[tag]++;
                        }
                        else
                        {
                            hashTags.Add(tag, 1);
                        }
                    }

                    if (progressBarProcess.Value % 10 == 0 && progressBarProcess.Value != actual)
                    {
                        actual = progressBarProcess.Value;
                        FileAO.ExportTagsToTxt(hashTags, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\temp_tags" + progressBarProcess.Value + ".txt");
                    }
                    if (logging())
                    {
                        logInfo("Filme nº: " + counter);
                    }
                }
            }
            catch
            {
                FileAO.ExportTagsToTxt(hashTags, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\temp_tags.txt");
            }

            FileAO.ExportTagsToTxt(hashTags, @"C:\Users\Yvens\Documents\GitHub\DisciplinaAprendizado\Codes\ImdbCrawler\CSV files\tags.txt");
            progressBarProcess.Value = 100;

            if (logging())
            {
                logInfo("Sucesso!");
            }
        }
    }
}
