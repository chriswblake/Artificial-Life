namespace Ch_7_Artificial_Life
{
    partial class formMain
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label lblReproduction;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.chartTop = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBottom = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tablePanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.tbPlantsPercent = new System.Windows.Forms.TextBox();
            this.tbHerbivoresPercent = new System.Windows.Forms.TextBox();
            this.tbCarnivoresPercent = new System.Windows.Forms.TextBox();
            this.tbHerbivoresEnergyInit = new System.Windows.Forms.TextBox();
            this.tbHerbivoresEnergyRepr = new System.Windows.Forms.TextBox();
            this.tbCarnivoresEnergyInit = new System.Windows.Forms.TextBox();
            this.tbCarnivoresEnergyRepr = new System.Windows.Forms.TextBox();
            this.btnSimulate = new System.Windows.Forms.Button();
            this.tbSimulationTime = new System.Windows.Forms.TextBox();
            this.chbShowHerbivoreAge = new System.Windows.Forms.CheckBox();
            this.chbShowCarnivoreAge = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            lblReproduction = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBottom)).BeginInit();
            this.tablePanelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            this.tablePanelControls.SetColumnSpan(label1, 2);
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 31);
            label1.TabIndex = 0;
            label1.Text = "World";
            // 
            // lblReproduction
            // 
            lblReproduction.AutoSize = true;
            this.tablePanelControls.SetColumnSpan(lblReproduction, 2);
            lblReproduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblReproduction.Location = new System.Drawing.Point(3, 162);
            lblReproduction.Name = "lblReproduction";
            lblReproduction.Size = new System.Drawing.Size(106, 31);
            lblReproduction.TabIndex = 1;
            lblReproduction.Text = "Energy";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 68);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(141, 25);
            label3.TabIndex = 2;
            label3.Text = "% Herbivores";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(3, 105);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(141, 25);
            label4.TabIndex = 3;
            label4.Text = "% Carnivores";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(3, 31);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(97, 25);
            label5.TabIndex = 4;
            label5.Text = "% Plants";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 193);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(156, 25);
            label2.TabIndex = 8;
            label2.Text = "Herbivores Init.";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(3, 230);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(174, 25);
            label6.TabIndex = 10;
            label6.Text = "Herbivores Repr.";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(3, 267);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(156, 25);
            label7.TabIndex = 12;
            label7.Text = "Carnivores Init.";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(3, 304);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(174, 25);
            label8.TabIndex = 13;
            label8.Text = "Carnivores Repr.";
            // 
            // label9
            // 
            label9.AutoSize = true;
            this.tablePanelControls.SetColumnSpan(label9, 2);
            label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.Location = new System.Drawing.Point(3, 361);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(150, 31);
            label9.TabIndex = 17;
            label9.Text = "Simulation";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(3, 392);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(59, 25);
            label10.TabIndex = 18;
            label10.Text = "Time";
            // 
            // tablePanel
            // 
            this.tablePanel.ColumnCount = 2;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.Controls.Add(this.chartTop, 1, 0);
            this.tablePanel.Controls.Add(this.chartBottom, 1, 1);
            this.tablePanel.Controls.Add(this.tablePanelControls, 0, 0);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 2;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.Size = new System.Drawing.Size(1272, 889);
            this.tablePanel.TabIndex = 0;
            // 
            // chartTop
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTop.ChartAreas.Add(chartArea1);
            this.chartTop.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartTop.Legends.Add(legend1);
            this.chartTop.Location = new System.Drawing.Point(253, 3);
            this.chartTop.Name = "chartTop";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTop.Series.Add(series1);
            this.chartTop.Size = new System.Drawing.Size(1016, 438);
            this.chartTop.TabIndex = 0;
            this.chartTop.TabStop = false;
            this.chartTop.Text = "chart1";
            // 
            // chartBottom
            // 
            chartArea2.Name = "ChartArea1";
            this.chartBottom.ChartAreas.Add(chartArea2);
            this.chartBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartBottom.Legends.Add(legend2);
            this.chartBottom.Location = new System.Drawing.Point(253, 447);
            this.chartBottom.Name = "chartBottom";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartBottom.Series.Add(series2);
            this.chartBottom.Size = new System.Drawing.Size(1016, 439);
            this.chartBottom.TabIndex = 1;
            this.chartBottom.TabStop = false;
            this.chartBottom.Text = "chart1";
            // 
            // tablePanelControls
            // 
            this.tablePanelControls.AutoSize = true;
            this.tablePanelControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablePanelControls.ColumnCount = 2;
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelControls.Controls.Add(label5, 0, 1);
            this.tablePanelControls.Controls.Add(label1, 0, 0);
            this.tablePanelControls.Controls.Add(lblReproduction, 0, 5);
            this.tablePanelControls.Controls.Add(label3, 0, 2);
            this.tablePanelControls.Controls.Add(label4, 0, 3);
            this.tablePanelControls.Controls.Add(this.tbPlantsPercent, 1, 1);
            this.tablePanelControls.Controls.Add(this.tbHerbivoresPercent, 1, 2);
            this.tablePanelControls.Controls.Add(this.tbCarnivoresPercent, 1, 3);
            this.tablePanelControls.Controls.Add(label2, 0, 6);
            this.tablePanelControls.Controls.Add(this.tbHerbivoresEnergyInit, 1, 6);
            this.tablePanelControls.Controls.Add(label6, 0, 7);
            this.tablePanelControls.Controls.Add(this.tbHerbivoresEnergyRepr, 1, 7);
            this.tablePanelControls.Controls.Add(label7, 0, 8);
            this.tablePanelControls.Controls.Add(label8, 0, 9);
            this.tablePanelControls.Controls.Add(this.tbCarnivoresEnergyInit, 1, 8);
            this.tablePanelControls.Controls.Add(this.tbCarnivoresEnergyRepr, 1, 9);
            this.tablePanelControls.Controls.Add(this.btnSimulate, 0, 15);
            this.tablePanelControls.Controls.Add(label9, 0, 11);
            this.tablePanelControls.Controls.Add(label10, 0, 12);
            this.tablePanelControls.Controls.Add(this.tbSimulationTime, 1, 12);
            this.tablePanelControls.Controls.Add(this.chbShowHerbivoreAge, 0, 13);
            this.tablePanelControls.Controls.Add(this.chbShowCarnivoreAge, 0, 14);
            this.tablePanelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanelControls.Location = new System.Drawing.Point(3, 3);
            this.tablePanelControls.Name = "tablePanelControls";
            this.tablePanelControls.RowCount = 17;
            this.tablePanel.SetRowSpan(this.tablePanelControls, 2);
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tablePanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanelControls.Size = new System.Drawing.Size(244, 569);
            this.tablePanelControls.TabIndex = 2;
            // 
            // tbPlantsPercent
            // 
            this.tbPlantsPercent.Location = new System.Drawing.Point(183, 34);
            this.tbPlantsPercent.MaxLength = 2;
            this.tbPlantsPercent.Name = "tbPlantsPercent";
            this.tbPlantsPercent.Size = new System.Drawing.Size(50, 31);
            this.tbPlantsPercent.TabIndex = 1;
            this.tbPlantsPercent.Text = "70";
            this.tbPlantsPercent.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbPlantsPercent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // tbHerbivoresPercent
            // 
            this.tbHerbivoresPercent.Location = new System.Drawing.Point(183, 71);
            this.tbHerbivoresPercent.MaxLength = 2;
            this.tbHerbivoresPercent.Name = "tbHerbivoresPercent";
            this.tbHerbivoresPercent.Size = new System.Drawing.Size(50, 31);
            this.tbHerbivoresPercent.TabIndex = 2;
            this.tbHerbivoresPercent.Text = "80";
            this.tbHerbivoresPercent.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbHerbivoresPercent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // tbCarnivoresPercent
            // 
            this.tbCarnivoresPercent.Location = new System.Drawing.Point(183, 108);
            this.tbCarnivoresPercent.MaxLength = 2;
            this.tbCarnivoresPercent.Name = "tbCarnivoresPercent";
            this.tbCarnivoresPercent.Size = new System.Drawing.Size(50, 31);
            this.tbCarnivoresPercent.TabIndex = 3;
            this.tbCarnivoresPercent.Text = "5";
            this.tbCarnivoresPercent.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbCarnivoresPercent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // tbHerbivoresEnergyInit
            // 
            this.tbHerbivoresEnergyInit.Location = new System.Drawing.Point(183, 196);
            this.tbHerbivoresEnergyInit.MaxLength = 2;
            this.tbHerbivoresEnergyInit.Name = "tbHerbivoresEnergyInit";
            this.tbHerbivoresEnergyInit.Size = new System.Drawing.Size(50, 31);
            this.tbHerbivoresEnergyInit.TabIndex = 4;
            this.tbHerbivoresEnergyInit.Text = "50";
            this.tbHerbivoresEnergyInit.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbHerbivoresEnergyInit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // tbHerbivoresEnergyRepr
            // 
            this.tbHerbivoresEnergyRepr.Location = new System.Drawing.Point(183, 233);
            this.tbHerbivoresEnergyRepr.MaxLength = 2;
            this.tbHerbivoresEnergyRepr.Name = "tbHerbivoresEnergyRepr";
            this.tbHerbivoresEnergyRepr.Size = new System.Drawing.Size(50, 31);
            this.tbHerbivoresEnergyRepr.TabIndex = 5;
            this.tbHerbivoresEnergyRepr.Text = "51";
            this.tbHerbivoresEnergyRepr.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbHerbivoresEnergyRepr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // tbCarnivoresEnergyInit
            // 
            this.tbCarnivoresEnergyInit.Location = new System.Drawing.Point(183, 270);
            this.tbCarnivoresEnergyInit.MaxLength = 2;
            this.tbCarnivoresEnergyInit.Name = "tbCarnivoresEnergyInit";
            this.tbCarnivoresEnergyInit.Size = new System.Drawing.Size(50, 31);
            this.tbCarnivoresEnergyInit.TabIndex = 6;
            this.tbCarnivoresEnergyInit.Text = "50";
            this.tbCarnivoresEnergyInit.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbCarnivoresEnergyInit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // tbCarnivoresEnergyRepr
            // 
            this.tbCarnivoresEnergyRepr.Location = new System.Drawing.Point(183, 307);
            this.tbCarnivoresEnergyRepr.MaxLength = 2;
            this.tbCarnivoresEnergyRepr.Name = "tbCarnivoresEnergyRepr";
            this.tbCarnivoresEnergyRepr.Size = new System.Drawing.Size(50, 31);
            this.tbCarnivoresEnergyRepr.TabIndex = 7;
            this.tbCarnivoresEnergyRepr.Text = "51";
            this.tbCarnivoresEnergyRepr.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbCarnivoresEnergyRepr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // btnSimulate
            // 
            this.btnSimulate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSimulate.Location = new System.Drawing.Point(3, 502);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(174, 44);
            this.btnSimulate.TabIndex = 9;
            this.btnSimulate.Text = "Simulate";
            this.btnSimulate.UseVisualStyleBackColor = true;
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);
            // 
            // tbSimulationTime
            // 
            this.tbSimulationTime.Location = new System.Drawing.Point(183, 395);
            this.tbSimulationTime.MaxLength = 4;
            this.tbSimulationTime.Name = "tbSimulationTime";
            this.tbSimulationTime.Size = new System.Drawing.Size(58, 31);
            this.tbSimulationTime.TabIndex = 8;
            this.tbSimulationTime.Text = "100";
            this.tbSimulationTime.TextChanged += new System.EventHandler(this.tbParameter_TextChanged);
            this.tbSimulationTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbParameter_KeyUp);
            // 
            // chbShowHerbivoreAge
            // 
            this.chbShowHerbivoreAge.AutoSize = true;
            this.tablePanelControls.SetColumnSpan(this.chbShowHerbivoreAge, 2);
            this.chbShowHerbivoreAge.Location = new System.Drawing.Point(3, 432);
            this.chbShowHerbivoreAge.Name = "chbShowHerbivoreAge";
            this.chbShowHerbivoreAge.Size = new System.Drawing.Size(199, 29);
            this.chbShowHerbivoreAge.TabIndex = 19;
            this.chbShowHerbivoreAge.Text = "Show Herb. Age";
            this.chbShowHerbivoreAge.UseVisualStyleBackColor = true;
            this.chbShowHerbivoreAge.CheckedChanged += new System.EventHandler(this.chbShowAge_CheckedChanged);
            // 
            // chbShowCarnivoreAge
            // 
            this.chbShowCarnivoreAge.AutoSize = true;
            this.tablePanelControls.SetColumnSpan(this.chbShowCarnivoreAge, 2);
            this.chbShowCarnivoreAge.Location = new System.Drawing.Point(3, 467);
            this.chbShowCarnivoreAge.Name = "chbShowCarnivoreAge";
            this.chbShowCarnivoreAge.Size = new System.Drawing.Size(199, 29);
            this.chbShowCarnivoreAge.TabIndex = 20;
            this.chbShowCarnivoreAge.Text = "Show Carn. Age";
            this.chbShowCarnivoreAge.UseVisualStyleBackColor = true;
            this.chbShowCarnivoreAge.CheckedChanged += new System.EventHandler(this.chbShowAge_CheckedChanged);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1272, 889);
            this.Controls.Add(this.tablePanel);
            this.Name = "formMain";
            this.ShowIcon = false;
            this.Text = "Ch 7 - Artificial Life";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBottom)).EndInit();
            this.tablePanelControls.ResumeLayout(false);
            this.tablePanelControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTop;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBottom;
        private System.Windows.Forms.TableLayoutPanel tablePanelControls;
        private System.Windows.Forms.TextBox tbPlantsPercent;
        private System.Windows.Forms.TextBox tbHerbivoresPercent;
        private System.Windows.Forms.TextBox tbCarnivoresPercent;
        private System.Windows.Forms.TextBox tbHerbivoresEnergyInit;
        private System.Windows.Forms.TextBox tbHerbivoresEnergyRepr;
        private System.Windows.Forms.TextBox tbCarnivoresEnergyInit;
        private System.Windows.Forms.TextBox tbCarnivoresEnergyRepr;
        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.TextBox tbSimulationTime;
        private System.Windows.Forms.CheckBox chbShowHerbivoreAge;
        private System.Windows.Forms.CheckBox chbShowCarnivoreAge;
    }
}

