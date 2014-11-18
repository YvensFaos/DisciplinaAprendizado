namespace ImdbCrawlerDesktop
{
    partial class ICMainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxLastIndex = new System.Windows.Forms.TextBox();
            this.textBoxYear2 = new System.Windows.Forms.TextBox();
            this.labelTotalMovies = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonExportMovies = new System.Windows.Forms.Button();
            this.comboBoxSortBy = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxStartIndex = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.progressBarProcess = new System.Windows.Forms.ProgressBar();
            this.buttonClearCustom = new System.Windows.Forms.Button();
            this.buttonFindCustom = new System.Windows.Forms.Button();
            this.textBoxCustomURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonFindMovies = new System.Windows.Forms.Button();
            this.textBoxYear1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButtonDesc = new System.Windows.Forms.RadioButton();
            this.radioButtonAsc = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.comboBoxCharts = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chartArea = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonOpenMovieList = new System.Windows.Forms.Button();
            this.buttonExecuteAction = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.labelTotalActors = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelTotalDirectors = new System.Windows.Forms.Label();
            this.buttonOpenActorsList = new System.Windows.Forms.Button();
            this.buttonOpenDirectorsList = new System.Windows.Forms.Button();
            this.buttonExportDirectors = new System.Windows.Forms.Button();
            this.buttonExportActors = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.graphPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartArea)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonExecuteAction);
            this.panel1.Controls.Add(this.comboBoxAction);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 508);
            this.panel1.TabIndex = 0;
            // 
            // comboBoxAction
            // 
            this.comboBoxAction.FormattingEnabled = true;
            this.comboBoxAction.Items.AddRange(new object[] {
            "Buscar Atores",
            "Buscar Diretores"});
            this.comboBoxAction.Location = new System.Drawing.Point(6, 26);
            this.comboBoxAction.Name = "comboBoxAction";
            this.comboBoxAction.Size = new System.Drawing.Size(168, 21);
            this.comboBoxAction.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ação";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBoxLog);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxLog);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.graphPanel);
            this.panel2.Location = new System.Drawing.Point(197, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(808, 511);
            this.panel2.TabIndex = 1;
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Checked = true;
            this.checkBoxLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLog.Location = new System.Drawing.Point(411, 339);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(64, 17);
            this.checkBoxLog.TabIndex = 4;
            this.checkBoxLog.Text = "Logging";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(379, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Log";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(378, 355);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(427, 153);
            this.textBoxLog.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.buttonExportActors);
            this.panel4.Controls.Add(this.buttonExportDirectors);
            this.panel4.Controls.Add(this.buttonOpenDirectorsList);
            this.panel4.Controls.Add(this.buttonOpenActorsList);
            this.panel4.Controls.Add(this.labelTotalDirectors);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.labelTotalActors);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.buttonOpenMovieList);
            this.panel4.Controls.Add(this.textBoxLastIndex);
            this.panel4.Controls.Add(this.textBoxYear2);
            this.panel4.Controls.Add(this.labelTotalMovies);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.buttonExportMovies);
            this.panel4.Controls.Add(this.comboBoxSortBy);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.textBoxStartIndex);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.progressBarProcess);
            this.panel4.Controls.Add(this.buttonClearCustom);
            this.panel4.Controls.Add(this.buttonFindCustom);
            this.panel4.Controls.Add(this.textBoxCustomURL);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.buttonFindMovies);
            this.panel4.Controls.Add(this.textBoxYear1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.radioButtonDesc);
            this.panel4.Controls.Add(this.radioButtonAsc);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(369, 508);
            this.panel4.TabIndex = 1;
            // 
            // textBoxLastIndex
            // 
            this.textBoxLastIndex.Location = new System.Drawing.Point(92, 87);
            this.textBoxLastIndex.Name = "textBoxLastIndex";
            this.textBoxLastIndex.Size = new System.Drawing.Size(92, 20);
            this.textBoxLastIndex.TabIndex = 20;
            this.textBoxLastIndex.Text = "0";
            // 
            // textBoxYear2
            // 
            this.textBoxYear2.Location = new System.Drawing.Point(187, 126);
            this.textBoxYear2.Name = "textBoxYear2";
            this.textBoxYear2.Size = new System.Drawing.Size(177, 20);
            this.textBoxYear2.TabIndex = 19;
            this.textBoxYear2.Text = "2009";
            // 
            // labelTotalMovies
            // 
            this.labelTotalMovies.AutoSize = true;
            this.labelTotalMovies.Location = new System.Drawing.Point(133, 372);
            this.labelTotalMovies.Name = "labelTotalMovies";
            this.labelTotalMovies.Size = new System.Drawing.Size(13, 13);
            this.labelTotalMovies.TabIndex = 18;
            this.labelTotalMovies.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 372);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Total de Filmes: ";
            // 
            // buttonExportMovies
            // 
            this.buttonExportMovies.Location = new System.Drawing.Point(7, 449);
            this.buttonExportMovies.Name = "buttonExportMovies";
            this.buttonExportMovies.Size = new System.Drawing.Size(102, 23);
            this.buttonExportMovies.TabIndex = 16;
            this.buttonExportMovies.Text = "Exportar Filmes";
            this.buttonExportMovies.UseVisualStyleBackColor = true;
            this.buttonExportMovies.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // comboBoxSortBy
            // 
            this.comboBoxSortBy.FormattingEnabled = true;
            this.comboBoxSortBy.Items.AddRange(new object[] {
            "moviemeter",
            "alpha",
            "user_rating",
            "num_votes",
            "boxoffice_gross_us",
            "runtime",
            "release_date_us"});
            this.comboBoxSortBy.Location = new System.Drawing.Point(187, 87);
            this.comboBoxSortBy.Name = "comboBoxSortBy";
            this.comboBoxSortBy.Size = new System.Drawing.Size(177, 21);
            this.comboBoxSortBy.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(184, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Ordenado por";
            // 
            // textBoxStartIndex
            // 
            this.textBoxStartIndex.Location = new System.Drawing.Point(7, 87);
            this.textBoxStartIndex.Name = "textBoxStartIndex";
            this.textBoxStartIndex.Size = new System.Drawing.Size(81, 20);
            this.textBoxStartIndex.TabIndex = 13;
            this.textBoxStartIndex.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Index de busca";
            // 
            // progressBarProcess
            // 
            this.progressBarProcess.ForeColor = System.Drawing.Color.Transparent;
            this.progressBarProcess.Location = new System.Drawing.Point(7, 480);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(357, 23);
            this.progressBarProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarProcess.TabIndex = 11;
            // 
            // buttonClearCustom
            // 
            this.buttonClearCustom.Location = new System.Drawing.Point(87, 244);
            this.buttonClearCustom.Name = "buttonClearCustom";
            this.buttonClearCustom.Size = new System.Drawing.Size(75, 23);
            this.buttonClearCustom.TabIndex = 10;
            this.buttonClearCustom.Text = "Limpar";
            this.buttonClearCustom.UseVisualStyleBackColor = true;
            this.buttonClearCustom.Click += new System.EventHandler(this.buttonClearCustom_Click);
            // 
            // buttonFindCustom
            // 
            this.buttonFindCustom.Location = new System.Drawing.Point(6, 244);
            this.buttonFindCustom.Name = "buttonFindCustom";
            this.buttonFindCustom.Size = new System.Drawing.Size(75, 23);
            this.buttonFindCustom.TabIndex = 9;
            this.buttonFindCustom.Text = "Buscar";
            this.buttonFindCustom.UseVisualStyleBackColor = true;
            this.buttonFindCustom.Click += new System.EventHandler(this.buttonFindCustom_Click);
            // 
            // textBoxCustomURL
            // 
            this.textBoxCustomURL.Location = new System.Drawing.Point(7, 218);
            this.textBoxCustomURL.Name = "textBoxCustomURL";
            this.textBoxCustomURL.Size = new System.Drawing.Size(357, 20);
            this.textBoxCustomURL.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "URL Customizada";
            // 
            // buttonFindMovies
            // 
            this.buttonFindMovies.Location = new System.Drawing.Point(6, 152);
            this.buttonFindMovies.Name = "buttonFindMovies";
            this.buttonFindMovies.Size = new System.Drawing.Size(75, 23);
            this.buttonFindMovies.TabIndex = 6;
            this.buttonFindMovies.Text = "Buscar";
            this.buttonFindMovies.UseVisualStyleBackColor = true;
            this.buttonFindMovies.Click += new System.EventHandler(this.buttonFindMovies_Click);
            // 
            // textBoxYear1
            // 
            this.textBoxYear1.Location = new System.Drawing.Point(7, 126);
            this.textBoxYear1.Name = "textBoxYear1";
            this.textBoxYear1.Size = new System.Drawing.Size(177, 20);
            this.textBoxYear1.TabIndex = 5;
            this.textBoxYear1.Text = "2009";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ano";
            // 
            // radioButtonDesc
            // 
            this.radioButtonDesc.AutoSize = true;
            this.radioButtonDesc.Location = new System.Drawing.Point(95, 46);
            this.radioButtonDesc.Name = "radioButtonDesc";
            this.radioButtonDesc.Size = new System.Drawing.Size(89, 17);
            this.radioButtonDesc.TabIndex = 3;
            this.radioButtonDesc.Text = "Descendente";
            this.radioButtonDesc.UseVisualStyleBackColor = true;
            // 
            // radioButtonAsc
            // 
            this.radioButtonAsc.AutoSize = true;
            this.radioButtonAsc.Checked = true;
            this.radioButtonAsc.Location = new System.Drawing.Point(6, 46);
            this.radioButtonAsc.Name = "radioButtonAsc";
            this.radioButtonAsc.Size = new System.Drawing.Size(82, 17);
            this.radioButtonAsc.TabIndex = 2;
            this.radioButtonAsc.TabStop = true;
            this.radioButtonAsc.Text = "Ascendente";
            this.radioButtonAsc.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Formato";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Buscar Filmes";
            // 
            // graphPanel
            // 
            this.graphPanel.BackColor = System.Drawing.Color.White;
            this.graphPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphPanel.Controls.Add(this.comboBoxCharts);
            this.graphPanel.Controls.Add(this.label7);
            this.graphPanel.Controls.Add(this.chartArea);
            this.graphPanel.Location = new System.Drawing.Point(378, 3);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(427, 333);
            this.graphPanel.TabIndex = 0;
            // 
            // comboBoxCharts
            // 
            this.comboBoxCharts.FormattingEnabled = true;
            this.comboBoxCharts.Items.AddRange(new object[] {
            "Filmes por classificação",
            "Atores por premiação",
            "Diretores por premiação",
            "Filmes por gênero"});
            this.comboBoxCharts.Location = new System.Drawing.Point(6, 26);
            this.comboBoxCharts.Name = "comboBoxCharts";
            this.comboBoxCharts.Size = new System.Drawing.Size(416, 21);
            this.comboBoxCharts.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Gráfico";
            // 
            // chartArea
            // 
            chartArea4.Name = "ChartArea1";
            this.chartArea.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartArea.Legends.Add(legend4);
            this.chartArea.Location = new System.Drawing.Point(3, 53);
            this.chartArea.Name = "chartArea";
            this.chartArea.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chartArea.Size = new System.Drawing.Size(419, 275);
            this.chartArea.TabIndex = 0;
            this.chartArea.Text = "chart1";
            // 
            // buttonOpenMovieList
            // 
            this.buttonOpenMovieList.Location = new System.Drawing.Point(7, 420);
            this.buttonOpenMovieList.Name = "buttonOpenMovieList";
            this.buttonOpenMovieList.Size = new System.Drawing.Size(102, 23);
            this.buttonOpenMovieList.TabIndex = 21;
            this.buttonOpenMovieList.Text = "Carregar Filmes";
            this.buttonOpenMovieList.UseVisualStyleBackColor = true;
            this.buttonOpenMovieList.Click += new System.EventHandler(this.buttonOpenMovieList_Click);
            // 
            // buttonExecuteAction
            // 
            this.buttonExecuteAction.Location = new System.Drawing.Point(6, 54);
            this.buttonExecuteAction.Name = "buttonExecuteAction";
            this.buttonExecuteAction.Size = new System.Drawing.Size(75, 23);
            this.buttonExecuteAction.TabIndex = 2;
            this.buttonExecuteAction.Text = "Executar";
            this.buttonExecuteAction.UseVisualStyleBackColor = true;
            this.buttonExecuteAction.Click += new System.EventHandler(this.buttonExecuteAction_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 385);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Total de Atores: ";
            // 
            // labelTotalActors
            // 
            this.labelTotalActors.AutoSize = true;
            this.labelTotalActors.Location = new System.Drawing.Point(133, 385);
            this.labelTotalActors.Name = "labelTotalActors";
            this.labelTotalActors.Size = new System.Drawing.Size(13, 13);
            this.labelTotalActors.TabIndex = 23;
            this.labelTotalActors.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 398);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Total de Diretores: ";
            // 
            // labelTotalDirectors
            // 
            this.labelTotalDirectors.AutoSize = true;
            this.labelTotalDirectors.Location = new System.Drawing.Point(133, 398);
            this.labelTotalDirectors.Name = "labelTotalDirectors";
            this.labelTotalDirectors.Size = new System.Drawing.Size(13, 13);
            this.labelTotalDirectors.TabIndex = 25;
            this.labelTotalDirectors.Text = "0";
            // 
            // buttonOpenActorsList
            // 
            this.buttonOpenActorsList.Location = new System.Drawing.Point(263, 422);
            this.buttonOpenActorsList.Name = "buttonOpenActorsList";
            this.buttonOpenActorsList.Size = new System.Drawing.Size(101, 23);
            this.buttonOpenActorsList.TabIndex = 26;
            this.buttonOpenActorsList.Text = "Carregar Atores";
            this.buttonOpenActorsList.UseVisualStyleBackColor = true;
            this.buttonOpenActorsList.Click += new System.EventHandler(this.buttonOpenActorsList_Click);
            // 
            // buttonOpenDirectorsList
            // 
            this.buttonOpenDirectorsList.Location = new System.Drawing.Point(136, 422);
            this.buttonOpenDirectorsList.Name = "buttonOpenDirectorsList";
            this.buttonOpenDirectorsList.Size = new System.Drawing.Size(101, 23);
            this.buttonOpenDirectorsList.TabIndex = 27;
            this.buttonOpenDirectorsList.Text = "Carregar Diretores";
            this.buttonOpenDirectorsList.UseVisualStyleBackColor = true;
            this.buttonOpenDirectorsList.Click += new System.EventHandler(this.buttonOpenDirectorsList_Click);
            // 
            // buttonExportDirectors
            // 
            this.buttonExportDirectors.Location = new System.Drawing.Point(136, 449);
            this.buttonExportDirectors.Name = "buttonExportDirectors";
            this.buttonExportDirectors.Size = new System.Drawing.Size(102, 23);
            this.buttonExportDirectors.TabIndex = 28;
            this.buttonExportDirectors.Text = "Exportar Diretores";
            this.buttonExportDirectors.UseVisualStyleBackColor = true;
            this.buttonExportDirectors.Click += new System.EventHandler(this.buttonExportDirectors_Click);
            // 
            // buttonExportActors
            // 
            this.buttonExportActors.Location = new System.Drawing.Point(263, 449);
            this.buttonExportActors.Name = "buttonExportActors";
            this.buttonExportActors.Size = new System.Drawing.Size(102, 23);
            this.buttonExportActors.TabIndex = 29;
            this.buttonExportActors.Text = "Exportar Atores";
            this.buttonExportActors.UseVisualStyleBackColor = true;
            this.buttonExportActors.Click += new System.EventHandler(this.buttonExportActors_Click);
            // 
            // ICMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1017, 535);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ICMainWindow";
            this.Text = "Imdb Crawler Desktop";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.graphPanel.ResumeLayout(false);
            this.graphPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel graphPanel;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonDesc;
        private System.Windows.Forms.RadioButton radioButtonAsc;
        private System.Windows.Forms.TextBox textBoxYear1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonFindMovies;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartArea;
        private System.Windows.Forms.Button buttonClearCustom;
        private System.Windows.Forms.Button buttonFindCustom;
        private System.Windows.Forms.TextBox textBoxCustomURL;
        private System.Windows.Forms.ComboBox comboBoxCharts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.CheckBox checkBoxLog;
        private System.Windows.Forms.ComboBox comboBoxSortBy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxStartIndex;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonExportMovies;
        private System.Windows.Forms.Label labelTotalMovies;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxYear2;
        private System.Windows.Forms.TextBox textBoxLastIndex;
        private System.Windows.Forms.Button buttonOpenMovieList;
        private System.Windows.Forms.Button buttonExecuteAction;
        private System.Windows.Forms.Button buttonExportActors;
        private System.Windows.Forms.Button buttonExportDirectors;
        private System.Windows.Forms.Button buttonOpenDirectorsList;
        private System.Windows.Forms.Button buttonOpenActorsList;
        private System.Windows.Forms.Label labelTotalDirectors;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelTotalActors;
        private System.Windows.Forms.Label label11;
    }
}

