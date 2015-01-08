namespace TicTacToeViewer
{
    partial class MainForm
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
            this.OneMoveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OneGameButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.GameCountLabel = new System.Windows.Forms.Label();
            this.Player1WinsLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Player2WinsLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DrawLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Player2PercentageLabel = new System.Windows.Forms.Label();
            this.Player1PercentageLabel = new System.Windows.Forms.Label();
            this.ResetStatsButton = new System.Windows.Forms.Button();
            this.Player1Duration = new System.Windows.Forms.Label();
            this.Player2Duration = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbGameState = new System.Windows.Forms.TextBox();
            this.DrawGameStateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OneMoveButton
            // 
            this.OneMoveButton.Location = new System.Drawing.Point(418, 95);
            this.OneMoveButton.Name = "OneMoveButton";
            this.OneMoveButton.Size = new System.Drawing.Size(121, 23);
            this.OneMoveButton.TabIndex = 0;
            this.OneMoveButton.Text = "One move";
            this.OneMoveButton.UseVisualStyleBackColor = true;
            this.OneMoveButton.Click += new System.EventHandler(this.OneMoveButton_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 400);
            this.panel1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(418, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(274, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(418, 68);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(274, 21);
            this.comboBox2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(418, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Player1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(418, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Player2";
            // 
            // OneGameButton
            // 
            this.OneGameButton.Location = new System.Drawing.Point(418, 124);
            this.OneGameButton.Name = "OneGameButton";
            this.OneGameButton.Size = new System.Drawing.Size(121, 23);
            this.OneGameButton.TabIndex = 6;
            this.OneGameButton.Text = "One game";
            this.OneGameButton.UseVisualStyleBackColor = true;
            this.OneGameButton.Click += new System.EventHandler(this.OneGameButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(418, 153);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(121, 23);
            this.StartButton.TabIndex = 7;
            this.StartButton.Text = "Start continuous";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(418, 182);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(121, 23);
            this.StopButton.TabIndex = 8;
            this.StopButton.Text = "Stop continuous";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(418, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Number games";
            // 
            // GameCountLabel
            // 
            this.GameCountLabel.AutoSize = true;
            this.GameCountLabel.Location = new System.Drawing.Point(514, 237);
            this.GameCountLabel.Name = "GameCountLabel";
            this.GameCountLabel.Size = new System.Drawing.Size(35, 13);
            this.GameCountLabel.TabIndex = 10;
            this.GameCountLabel.Text = "label4";
            // 
            // Player1WinsLabel
            // 
            this.Player1WinsLabel.AutoSize = true;
            this.Player1WinsLabel.Location = new System.Drawing.Point(514, 250);
            this.Player1WinsLabel.Name = "Player1WinsLabel";
            this.Player1WinsLabel.Size = new System.Drawing.Size(35, 13);
            this.Player1WinsLabel.TabIndex = 12;
            this.Player1WinsLabel.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(418, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Player 1";
            // 
            // Player2WinsLabel
            // 
            this.Player2WinsLabel.AutoSize = true;
            this.Player2WinsLabel.Location = new System.Drawing.Point(514, 263);
            this.Player2WinsLabel.Name = "Player2WinsLabel";
            this.Player2WinsLabel.Size = new System.Drawing.Size(35, 13);
            this.Player2WinsLabel.TabIndex = 14;
            this.Player2WinsLabel.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(418, 265);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Player 2";
            // 
            // DrawLabel
            // 
            this.DrawLabel.AutoSize = true;
            this.DrawLabel.Location = new System.Drawing.Point(514, 276);
            this.DrawLabel.Name = "DrawLabel";
            this.DrawLabel.Size = new System.Drawing.Size(35, 13);
            this.DrawLabel.TabIndex = 16;
            this.DrawLabel.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(418, 278);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Draws";
            // 
            // Player2PercentageLabel
            // 
            this.Player2PercentageLabel.AutoSize = true;
            this.Player2PercentageLabel.Location = new System.Drawing.Point(565, 265);
            this.Player2PercentageLabel.Name = "Player2PercentageLabel";
            this.Player2PercentageLabel.Size = new System.Drawing.Size(35, 13);
            this.Player2PercentageLabel.TabIndex = 18;
            this.Player2PercentageLabel.Text = "label7";
            // 
            // Player1PercentageLabel
            // 
            this.Player1PercentageLabel.AutoSize = true;
            this.Player1PercentageLabel.Location = new System.Drawing.Point(565, 252);
            this.Player1PercentageLabel.Name = "Player1PercentageLabel";
            this.Player1PercentageLabel.Size = new System.Drawing.Size(35, 13);
            this.Player1PercentageLabel.TabIndex = 17;
            this.Player1PercentageLabel.Text = "label5";
            // 
            // ResetStatsButton
            // 
            this.ResetStatsButton.Location = new System.Drawing.Point(418, 211);
            this.ResetStatsButton.Name = "ResetStatsButton";
            this.ResetStatsButton.Size = new System.Drawing.Size(121, 23);
            this.ResetStatsButton.TabIndex = 19;
            this.ResetStatsButton.Text = "Clear statistics";
            this.ResetStatsButton.UseVisualStyleBackColor = true;
            this.ResetStatsButton.Click += new System.EventHandler(this.ResetStatsButton_Click);
            // 
            // Player1Duration
            // 
            this.Player1Duration.AutoSize = true;
            this.Player1Duration.Location = new System.Drawing.Point(634, 252);
            this.Player1Duration.Name = "Player1Duration";
            this.Player1Duration.Size = new System.Drawing.Size(35, 13);
            this.Player1Duration.TabIndex = 20;
            this.Player1Duration.Text = "label5";
            // 
            // Player2Duration
            // 
            this.Player2Duration.AutoSize = true;
            this.Player2Duration.Location = new System.Drawing.Point(634, 265);
            this.Player2Duration.Name = "Player2Duration";
            this.Player2Duration.Size = new System.Drawing.Size(35, 13);
            this.Player2Duration.TabIndex = 21;
            this.Player2Duration.Text = "label7";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(565, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Perc Wins";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(634, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Avg Duration";
            // 
            // tbGameState
            // 
            this.tbGameState.Location = new System.Drawing.Point(418, 314);
            this.tbGameState.Multiline = true;
            this.tbGameState.Name = "tbGameState";
            this.tbGameState.Size = new System.Drawing.Size(297, 98);
            this.tbGameState.TabIndex = 24;
            // 
            // DrawGameStateButton
            // 
            this.DrawGameStateButton.Location = new System.Drawing.Point(571, 95);
            this.DrawGameStateButton.Name = "DrawGameStateButton";
            this.DrawGameStateButton.Size = new System.Drawing.Size(121, 23);
            this.DrawGameStateButton.TabIndex = 25;
            this.DrawGameStateButton.Text = "Draw state string";
            this.DrawGameStateButton.UseVisualStyleBackColor = true;
            this.DrawGameStateButton.Click += new System.EventHandler(this.DrawGameStateButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 441);
            this.Controls.Add(this.DrawGameStateButton);
            this.Controls.Add(this.tbGameState);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Player2Duration);
            this.Controls.Add(this.Player1Duration);
            this.Controls.Add(this.ResetStatsButton);
            this.Controls.Add(this.Player2PercentageLabel);
            this.Controls.Add(this.Player1PercentageLabel);
            this.Controls.Add(this.DrawLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Player2WinsLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Player1WinsLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.GameCountLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.OneGameButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.OneMoveButton);
            this.Name = "MainForm";
            this.Text = "TicTacToe Arena";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OneMoveButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OneGameButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label GameCountLabel;
        private System.Windows.Forms.Label Player1WinsLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Player2WinsLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label DrawLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label Player2PercentageLabel;
        private System.Windows.Forms.Label Player1PercentageLabel;
        private System.Windows.Forms.Button ResetStatsButton;
        private System.Windows.Forms.Label Player1Duration;
        private System.Windows.Forms.Label Player2Duration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbGameState;
        private System.Windows.Forms.Button DrawGameStateButton;
    }
}

