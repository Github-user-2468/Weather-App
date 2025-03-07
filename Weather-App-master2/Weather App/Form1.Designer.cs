namespace Weather_App
{
    partial class Form1
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
            this.Citylabel = new System.Windows.Forms.Label();
            this.CityTextBox = new System.Windows.Forms.TextBox();
            this.StateComboBox = new System.Windows.Forms.ComboBox();
            this.Statelabel = new System.Windows.Forms.Label();
            this.Searchbutton = new System.Windows.Forms.Button();
            this.ShortForcastLabel = new System.Windows.Forms.Label();
            this.templabel1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SunsetLabel = new System.Windows.Forms.Label();
            this.SunriseLabel = new System.Windows.Forms.Label();
            this.humlabel = new System.Windows.Forms.Label();
            this.pertilabel = new System.Windows.Forms.Label();
            this.windlabel1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.detailForecastlbl = new System.Windows.Forms.Label();
            this.ColorBox = new System.Windows.Forms.Label();
            this.Fahrenheitbtn = new System.Windows.Forms.Button();
            this.Celsiusbtn = new System.Windows.Forms.Button();
            this.WeatherIcon = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.RadarPictureBox = new System.Windows.Forms.PictureBox();
            this.RadarBTN = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WeatherIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadarPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Citylabel
            // 
            this.Citylabel.AutoSize = true;
            this.Citylabel.BackColor = System.Drawing.Color.Transparent;
            this.Citylabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Citylabel.Location = new System.Drawing.Point(13, 13);
            this.Citylabel.Name = "Citylabel";
            this.Citylabel.Size = new System.Drawing.Size(39, 17);
            this.Citylabel.TabIndex = 0;
            this.Citylabel.Text = "City: ";
            // 
            // CityTextBox
            // 
            this.CityTextBox.Location = new System.Drawing.Point(57, 13);
            this.CityTextBox.Name = "CityTextBox";
            this.CityTextBox.Size = new System.Drawing.Size(100, 20);
            this.CityTextBox.TabIndex = 1;
            // 
            // StateComboBox
            // 
            this.StateComboBox.FormattingEnabled = true;
            this.StateComboBox.Location = new System.Drawing.Point(233, 14);
            this.StateComboBox.Name = "StateComboBox";
            this.StateComboBox.Size = new System.Drawing.Size(121, 21);
            this.StateComboBox.TabIndex = 2;
            // 
            // Statelabel
            // 
            this.Statelabel.AutoSize = true;
            this.Statelabel.BackColor = System.Drawing.Color.Transparent;
            this.Statelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Statelabel.Location = new System.Drawing.Point(179, 14);
            this.Statelabel.Name = "Statelabel";
            this.Statelabel.Size = new System.Drawing.Size(45, 17);
            this.Statelabel.TabIndex = 3;
            this.Statelabel.Text = "State:";
            // 
            // Searchbutton
            // 
            this.Searchbutton.BackColor = System.Drawing.Color.Transparent;
            this.Searchbutton.Location = new System.Drawing.Point(386, 12);
            this.Searchbutton.Name = "Searchbutton";
            this.Searchbutton.Size = new System.Drawing.Size(75, 23);
            this.Searchbutton.TabIndex = 4;
            this.Searchbutton.Text = "Search";
            this.Searchbutton.UseVisualStyleBackColor = false;
            this.Searchbutton.Click += new System.EventHandler(this.Searchbutton_Click);
            // 
            // ShortForcastLabel
            // 
            this.ShortForcastLabel.AutoSize = true;
            this.ShortForcastLabel.BackColor = System.Drawing.Color.Transparent;
            this.ShortForcastLabel.Location = new System.Drawing.Point(27, 122);
            this.ShortForcastLabel.Name = "ShortForcastLabel";
            this.ShortForcastLabel.Size = new System.Drawing.Size(35, 13);
            this.ShortForcastLabel.TabIndex = 7;
            this.ShortForcastLabel.Text = "label1";
            // 
            // templabel1
            // 
            this.templabel1.AutoSize = true;
            this.templabel1.BackColor = System.Drawing.Color.Transparent;
            this.templabel1.Font = new System.Drawing.Font("Calibri", 32.75F);
            this.templabel1.Location = new System.Drawing.Point(21, 57);
            this.templabel1.Name = "templabel1";
            this.templabel1.Size = new System.Drawing.Size(131, 54);
            this.templabel1.TabIndex = 13;
            this.templabel1.Text = "label1";
            this.templabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.SunsetLabel);
            this.groupBox1.Controls.Add(this.SunriseLabel);
            this.groupBox1.Controls.Add(this.humlabel);
            this.groupBox1.Controls.Add(this.pertilabel);
            this.groupBox1.Controls.Add(this.windlabel1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(646, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 133);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conditions";
            // 
            // SunsetLabel
            // 
            this.SunsetLabel.AutoSize = true;
            this.SunsetLabel.Location = new System.Drawing.Point(5, 110);
            this.SunsetLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SunsetLabel.Name = "SunsetLabel";
            this.SunsetLabel.Size = new System.Drawing.Size(35, 13);
            this.SunsetLabel.TabIndex = 4;
            this.SunsetLabel.Text = "label2";
            // 
            // SunriseLabel
            // 
            this.SunriseLabel.AutoSize = true;
            this.SunriseLabel.Location = new System.Drawing.Point(5, 90);
            this.SunriseLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SunriseLabel.Name = "SunriseLabel";
            this.SunriseLabel.Size = new System.Drawing.Size(35, 13);
            this.SunriseLabel.TabIndex = 3;
            this.SunriseLabel.Text = "label1";
            // 
            // humlabel
            // 
            this.humlabel.AutoSize = true;
            this.humlabel.Location = new System.Drawing.Point(5, 67);
            this.humlabel.Name = "humlabel";
            this.humlabel.Size = new System.Drawing.Size(35, 13);
            this.humlabel.TabIndex = 2;
            this.humlabel.Text = "label3";
            // 
            // pertilabel
            // 
            this.pertilabel.AutoSize = true;
            this.pertilabel.Location = new System.Drawing.Point(5, 43);
            this.pertilabel.Name = "pertilabel";
            this.pertilabel.Size = new System.Drawing.Size(35, 13);
            this.pertilabel.TabIndex = 1;
            this.pertilabel.Text = "label2";
            // 
            // windlabel1
            // 
            this.windlabel1.AutoSize = true;
            this.windlabel1.Location = new System.Drawing.Point(7, 20);
            this.windlabel1.Name = "windlabel1";
            this.windlabel1.Size = new System.Drawing.Size(35, 13);
            this.windlabel1.TabIndex = 0;
            this.windlabel1.Text = "label1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(8, 231);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(992, 192);
            this.flowLayoutPanel1.TabIndex = 21;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // detailForecastlbl
            // 
            this.detailForecastlbl.BackColor = System.Drawing.Color.Transparent;
            this.detailForecastlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailForecastlbl.Location = new System.Drawing.Point(27, 157);
            this.detailForecastlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.detailForecastlbl.Name = "detailForecastlbl";
            this.detailForecastlbl.Size = new System.Drawing.Size(329, 51);
            this.detailForecastlbl.TabIndex = 23;
            this.detailForecastlbl.Text = "Weather Details";
            // 
            // ColorBox
            // 
            this.ColorBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ColorBox.Location = new System.Drawing.Point(-3, -1);
            this.ColorBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ColorBox.Name = "ColorBox";
            this.ColorBox.Size = new System.Drawing.Size(1012, 44);
            this.ColorBox.TabIndex = 24;
            // 
            // Fahrenheitbtn
            // 
            this.Fahrenheitbtn.BackColor = System.Drawing.Color.White;
            this.Fahrenheitbtn.Location = new System.Drawing.Point(880, 11);
            this.Fahrenheitbtn.Margin = new System.Windows.Forms.Padding(2);
            this.Fahrenheitbtn.Name = "Fahrenheitbtn";
            this.Fahrenheitbtn.Size = new System.Drawing.Size(29, 23);
            this.Fahrenheitbtn.TabIndex = 25;
            this.Fahrenheitbtn.Text = "F";
            this.Fahrenheitbtn.UseVisualStyleBackColor = false;
            this.Fahrenheitbtn.Click += new System.EventHandler(this.Fahrenheitbtn_Click);
            // 
            // Celsiusbtn
            // 
            this.Celsiusbtn.Location = new System.Drawing.Point(924, 10);
            this.Celsiusbtn.Margin = new System.Windows.Forms.Padding(2);
            this.Celsiusbtn.Name = "Celsiusbtn";
            this.Celsiusbtn.Size = new System.Drawing.Size(29, 24);
            this.Celsiusbtn.TabIndex = 26;
            this.Celsiusbtn.Text = "C";
            this.Celsiusbtn.UseVisualStyleBackColor = true;
            this.Celsiusbtn.Click += new System.EventHandler(this.Celsiusbtn_Click);
            // 
            // WeatherIcon
            // 
            this.WeatherIcon.BackColor = System.Drawing.Color.Transparent;
            this.WeatherIcon.Location = new System.Drawing.Point(145, 75);
            this.WeatherIcon.Margin = new System.Windows.Forms.Padding(2);
            this.WeatherIcon.Name = "WeatherIcon";
            this.WeatherIcon.Size = new System.Drawing.Size(67, 60);
            this.WeatherIcon.TabIndex = 27;
            this.WeatherIcon.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(8, 439);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(992, 136);
            this.flowLayoutPanel2.TabIndex = 28;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // RadarPictureBox
            // 
            this.RadarPictureBox.Location = new System.Drawing.Point(448, 65);
            this.RadarPictureBox.Name = "RadarPictureBox";
            this.RadarPictureBox.Size = new System.Drawing.Size(166, 124);
            this.RadarPictureBox.TabIndex = 29;
            this.RadarPictureBox.TabStop = false;
            // 
            // RadarBTN
            // 
            this.RadarBTN.Location = new System.Drawing.Point(488, 202);
            this.RadarBTN.Name = "RadarBTN";
            this.RadarBTN.Size = new System.Drawing.Size(75, 23);
            this.RadarBTN.TabIndex = 30;
            this.RadarBTN.Text = "View Radar";
            this.RadarBTN.UseVisualStyleBackColor = true;
            this.RadarBTN.Click += new System.EventHandler(this.RadarBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1009, 587);
            this.Controls.Add(this.RadarBTN);
            this.Controls.Add(this.RadarPictureBox);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.WeatherIcon);
            this.Controls.Add(this.Celsiusbtn);
            this.Controls.Add(this.Fahrenheitbtn);
            this.Controls.Add(this.detailForecastlbl);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.templabel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ShortForcastLabel);
            this.Controls.Add(this.Searchbutton);
            this.Controls.Add(this.Statelabel);
            this.Controls.Add(this.StateComboBox);
            this.Controls.Add(this.CityTextBox);
            this.Controls.Add(this.Citylabel);
            this.Controls.Add(this.ColorBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.BackgroundImageChanged += new System.EventHandler(this.Searchbutton_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WeatherIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadarPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Citylabel;
        private System.Windows.Forms.TextBox CityTextBox;
        private System.Windows.Forms.ComboBox StateComboBox;
        private System.Windows.Forms.Button Searchbutton;
        private System.Windows.Forms.Label ShortForcastLabel;
        private System.Windows.Forms.Label templabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label humlabel;
        private System.Windows.Forms.Label pertilabel;
        private System.Windows.Forms.Label windlabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label SunsetLabel;
        private System.Windows.Forms.Label SunriseLabel;
        private System.Windows.Forms.Label detailForecastlbl;
        private System.Windows.Forms.Label ColorBox;
        private System.Windows.Forms.Label Statelabel;
        private System.Windows.Forms.Button Fahrenheitbtn;
        private System.Windows.Forms.Button Celsiusbtn;
        private System.Windows.Forms.PictureBox WeatherIcon;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox RadarPictureBox;
        private System.Windows.Forms.Button RadarBTN;
    }
}

