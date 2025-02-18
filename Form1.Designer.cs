namespace JefimHolmgrenBreakout
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelGameArea = new System.Windows.Forms.Panel();
            this.pbPaddel = new System.Windows.Forms.PictureBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.panelStart = new System.Windows.Forms.Panel();
            this.rTBPlayerName = new System.Windows.Forms.RichTextBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.panelGameOver = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblGameOverName = new System.Windows.Forms.Label();
            this.rTBHighScoreList = new System.Windows.Forms.RichTextBox();
            this.lblScoreboard = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.lblInGameTime = new System.Windows.Forms.Label();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.lblGameOver = new System.Windows.Forms.Label();
            this.timerGameLoop = new System.Windows.Forms.Timer(this.components);
            this.timerInGameTime = new System.Windows.Forms.Timer(this.components);
            this.panelGameArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPaddel)).BeginInit();
            this.panelStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelGameOver.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGameArea
            // 
            this.panelGameArea.BackColor = System.Drawing.Color.Transparent;
            this.panelGameArea.Controls.Add(this.pbPaddel);
            this.panelGameArea.Controls.Add(this.lblScore);
            this.panelGameArea.ForeColor = System.Drawing.Color.Transparent;
            this.panelGameArea.Location = new System.Drawing.Point(0, 0);
            this.panelGameArea.Name = "panelGameArea";
            this.panelGameArea.Size = new System.Drawing.Size(510, 559);
            this.panelGameArea.TabIndex = 0;
            this.panelGameArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelGameArea_MouseMove);
            // 
            // pbPaddel
            // 
            this.pbPaddel.BackColor = System.Drawing.Color.Transparent;
            this.pbPaddel.Image = global::JefimHolmgrenBreakout.Properties.Resources.paddle;
            this.pbPaddel.Location = new System.Drawing.Point(184, 472);
            this.pbPaddel.Name = "pbPaddel";
            this.pbPaddel.Size = new System.Drawing.Size(96, 32);
            this.pbPaddel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPaddel.TabIndex = 2;
            this.pbPaddel.TabStop = false;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.Black;
            this.lblScore.Location = new System.Drawing.Point(212, 519);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(80, 24);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Score: 0";
            this.lblScore.Visible = false;
            // 
            // panelStart
            // 
            this.panelStart.BackColor = System.Drawing.Color.Transparent;
            this.panelStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelStart.Controls.Add(this.rTBPlayerName);
            this.panelStart.Controls.Add(this.lblPlayerName);
            this.panelStart.Controls.Add(this.pictureBox1);
            this.panelStart.Controls.Add(this.btnStartGame);
            this.panelStart.ForeColor = System.Drawing.Color.Transparent;
            this.panelStart.Location = new System.Drawing.Point(0, 0);
            this.panelStart.Name = "panelStart";
            this.panelStart.Size = new System.Drawing.Size(507, 507);
            this.panelStart.TabIndex = 4;
            // 
            // rTBPlayerName
            // 
            this.rTBPlayerName.BackColor = System.Drawing.Color.DimGray;
            this.rTBPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTBPlayerName.ForeColor = System.Drawing.Color.White;
            this.rTBPlayerName.Location = new System.Drawing.Point(125, 260);
            this.rTBPlayerName.MaxLength = 20;
            this.rTBPlayerName.Multiline = false;
            this.rTBPlayerName.Name = "rTBPlayerName";
            this.rTBPlayerName.Size = new System.Drawing.Size(242, 48);
            this.rTBPlayerName.TabIndex = 6;
            this.rTBPlayerName.Text = "";
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.BackColor = System.Drawing.Color.White;
            this.lblPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.Black;
            this.lblPlayerName.Location = new System.Drawing.Point(211, 235);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(68, 25);
            this.lblPlayerName.TabIndex = 3;
            this.lblPlayerName.Text = "Name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(62, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(379, 79);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnStartGame
            // 
            this.btnStartGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartGame.ForeColor = System.Drawing.Color.Transparent;
            this.btnStartGame.Image = ((System.Drawing.Image)(resources.GetObject("btnStartGame.Image")));
            this.btnStartGame.Location = new System.Drawing.Point(125, 139);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(242, 71);
            this.btnStartGame.TabIndex = 1;
            this.btnStartGame.Text = " ";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // panelGameOver
            // 
            this.panelGameOver.BackColor = System.Drawing.Color.White;
            this.panelGameOver.Controls.Add(this.pictureBox2);
            this.panelGameOver.Controls.Add(this.lblGameOverName);
            this.panelGameOver.Controls.Add(this.rTBHighScoreList);
            this.panelGameOver.Controls.Add(this.lblScoreboard);
            this.panelGameOver.Controls.Add(this.btnRestart);
            this.panelGameOver.Controls.Add(this.lblInGameTime);
            this.panelGameOver.Controls.Add(this.lblHighScore);
            this.panelGameOver.Controls.Add(this.lblGameOver);
            this.panelGameOver.ForeColor = System.Drawing.Color.Black;
            this.panelGameOver.Location = new System.Drawing.Point(62, 21);
            this.panelGameOver.Name = "panelGameOver";
            this.panelGameOver.Size = new System.Drawing.Size(379, 456);
            this.panelGameOver.TabIndex = 2;
            this.panelGameOver.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(379, 79);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lblGameOverName
            // 
            this.lblGameOverName.AutoSize = true;
            this.lblGameOverName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOverName.Location = new System.Drawing.Point(118, 123);
            this.lblGameOverName.Name = "lblGameOverName";
            this.lblGameOverName.Size = new System.Drawing.Size(122, 24);
            this.lblGameOverName.TabIndex = 7;
            this.lblGameOverName.Text = "Name: Name";
            // 
            // rTBHighScoreList
            // 
            this.rTBHighScoreList.Location = new System.Drawing.Point(69, 241);
            this.rTBHighScoreList.Name = "rTBHighScoreList";
            this.rTBHighScoreList.Size = new System.Drawing.Size(241, 139);
            this.rTBHighScoreList.TabIndex = 6;
            this.rTBHighScoreList.Text = "";
            // 
            // lblScoreboard
            // 
            this.lblScoreboard.AutoSize = true;
            this.lblScoreboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreboard.Location = new System.Drawing.Point(130, 212);
            this.lblScoreboard.Name = "lblScoreboard";
            this.lblScoreboard.Size = new System.Drawing.Size(109, 24);
            this.lblScoreboard.TabIndex = 5;
            this.lblScoreboard.Text = "Scoreboard";
            // 
            // btnRestart
            // 
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.Image = global::JefimHolmgrenBreakout.Properties.Resources.start_button;
            this.btnRestart.Location = new System.Drawing.Point(68, 379);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(242, 71);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // lblInGameTime
            // 
            this.lblInGameTime.AutoSize = true;
            this.lblInGameTime.BackColor = System.Drawing.Color.White;
            this.lblInGameTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInGameTime.ForeColor = System.Drawing.Color.Black;
            this.lblInGameTime.Location = new System.Drawing.Point(118, 181);
            this.lblInGameTime.Name = "lblInGameTime";
            this.lblInGameTime.Size = new System.Drawing.Size(108, 24);
            this.lblInGameTime.TabIndex = 2;
            this.lblInGameTime.Text = "Time: 00:00";
            // 
            // lblHighScore
            // 
            this.lblHighScore.AutoSize = true;
            this.lblHighScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighScore.Location = new System.Drawing.Point(118, 151);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(125, 24);
            this.lblHighScore.TabIndex = 1;
            this.lblHighScore.Text = "High Score: 0";
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOver.Location = new System.Drawing.Point(94, 84);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(190, 37);
            this.lblGameOver.TabIndex = 0;
            this.lblGameOver.Text = "Game Over!";
            // 
            // timerGameLoop
            // 
            this.timerGameLoop.Enabled = true;
            this.timerGameLoop.Interval = 1;
            this.timerGameLoop.Tick += new System.EventHandler(this.timerGameLoop_Tick);
            // 
            // timerInGameTime
            // 
            this.timerInGameTime.Enabled = true;
            this.timerInGameTime.Interval = 1000;
            this.timerInGameTime.Tick += new System.EventHandler(this.timerInGameTime_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 561);
            this.Controls.Add(this.panelGameOver);
            this.Controls.Add(this.panelStart);
            this.Controls.Add(this.panelGameArea);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelGameArea.ResumeLayout(false);
            this.panelGameArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPaddel)).EndInit();
            this.panelStart.ResumeLayout(false);
            this.panelStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelGameOver.ResumeLayout(false);
            this.panelGameOver.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGameArea;
        private System.Windows.Forms.PictureBox pbPaddel;
        private System.Windows.Forms.Timer timerGameLoop;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblInGameTime;
        private System.Windows.Forms.Timer timerInGameTime;
        private System.Windows.Forms.Panel panelStart;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Panel panelGameOver;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblScoreboard;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.RichTextBox rTBPlayerName;
        private System.Windows.Forms.RichTextBox rTBHighScoreList;
        private System.Windows.Forms.Label lblGameOverName;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

