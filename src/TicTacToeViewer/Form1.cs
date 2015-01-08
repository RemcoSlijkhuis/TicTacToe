using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeShared;

namespace TicTacToeViewer
{
    public partial class MainForm : Form
    {
        private Game game;
        private Drawer drawer;
        private Stats stats;

        //used to alternate between the starting player. 
        private int StartingPlayerIndex = 1;
     
        private bool isContinuous = false;


        public MainForm()
        {
            InitializeComponent();
        }


        private void OneMoveButton_Click(object sender, EventArgs e)
        {
            CheckGameStatus();
            game.PlayOneMove();
            drawer.Draw(game);
            tbGameState.Text = game.ToString();
        }

        private void CheckGameStatus()
        {
            if (game == null)
            {
                CreateGame();
            }
            if (game.GameResult != 0)
            {
                CreateGame();
            }
         
        }

        private void CreateGame()
        {
            var p1 = (IPlayerEngine)this.comboBox1.SelectedItem;
            var p2 = (IPlayerEngine)this.comboBox2.SelectedItem;
            game = new Game(p1, p2, StartingPlayerIndex);
            SwapStartingPlayer();
        }

        private void SwapStartingPlayer()
        {
            if (StartingPlayerIndex == 1)
            {
                StartingPlayerIndex = 2;
            }
            else
            {
                StartingPlayerIndex = 1;
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // bind the Engines to the dropdowns
            foreach (var engine in EngineProvider.EngineInstances) { this.comboBox1.Items.Add(engine); }
            foreach (var engine in EngineProvider.EngineInstances) { this.comboBox2.Items.Add(engine); }

            this.comboBox1.SelectedIndex = this.comboBox1.Items.Count - 1;
            this.comboBox2.SelectedIndex = this.comboBox1.Items.Count - 1;

            this.drawer = new Drawer(this.panel1);
            this.stats = new Stats();
        }

        private void OneGameButton_Click(object sender, EventArgs e)
        {
            CheckGameStatus();
            while (game.GameResult == 0) {
                game.PlayOneMove(); 
            }
            drawer.Draw(game);
            tbGameState.Text = game.ToString();
        }

        private void ShowStats()
        {
            GameCountLabel.Text = stats.NumberOfGames.ToString();
            Player1WinsLabel.Text = stats.Player1Wins.ToString();
            Player2WinsLabel.Text = stats.Player2Wins.ToString();
            if (stats.NumberOfGames > 0)
            {
                Player1PercentageLabel.Text = ((int)((1.0 * stats.Player1Wins / stats.NumberOfGames) * 100)).ToString();
                Player2PercentageLabel.Text = ((int)((1.0 * stats.Player2Wins / stats.NumberOfGames) * 100)).ToString();
                Player1Duration.Text = ((int)((stats.Player1Duration / stats.NumberOfGames))).ToString();
                Player2Duration.Text = ((int)((stats.Player2Duration / stats.NumberOfGames))).ToString();
            }
            DrawLabel.Text = stats.Draws.ToString();

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
           
            var p1 = (IPlayerEngine)this.comboBox1.SelectedItem;
            var p2 = (IPlayerEngine)this.comboBox2.SelectedItem;
            var sp = this.StartingPlayerIndex;
            SwapStartingPlayer();

            StartButton.Enabled = false;
            isContinuous = true;
            Task.Run(() =>
            {
              
                var localgame = new Game(p1, p2, sp);
                long player1time = 0;
                long player2time = 0;
                var stopWatch = new System.Diagnostics.Stopwatch();
                //Notify engines of game start.
                p1.BeforeGame();
                p2.BeforeGame();
                //Play the game. 
                while (localgame.GameResult == 0)
                {
                    stopWatch.Restart();
                    localgame.PlayOneMove();
                    stopWatch.Stop();
                    //NB : bewust andere player controleren, omdat ná de move de currentplayer al gewisseld is.
                    if (localgame.CurrentPlayerIndex == 1)
                    {
                        player2time += stopWatch.ElapsedTicks;
                    }
                    else
                    {
                        player1time += stopWatch.ElapsedTicks;
                    }
                }
                //Notify engines of game end.
                p1.AfterGame(1, localgame.Board, localgame.Moves);
                p2.AfterGame(2, localgame.Board, localgame.Moves);

                BeginInvoke(new Action(() =>
                {
                    stats.AddResult(localgame.GameResult, player1time,player2time);
                    this.ShowStats();

                    tbGameState.AppendText(localgame.ToString() + Environment.NewLine);

                    //alleen om de 10 potjes het scherm updaten
                    if (stats.NumberOfGames % 10 == 0)
                    {
                        this.drawer.Draw(localgame);
                    }
                    
                    if (isContinuous) { StartButton_Click(null, null); }
                }));

            });
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            isContinuous = false;
            StartButton.Enabled = true;
        }

        private void ResetStatsButton_Click(object sender, EventArgs e)
        {
            stats.Clear();
            ShowStats();
        }

        private void DrawGameStateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var p1 = (IPlayerEngine) comboBox1.SelectedItem;
                var p2 = (IPlayerEngine) comboBox2.SelectedItem;
                var gameState = tbGameState.Text.Trim();

                game = Game.FromString(gameState, p1, p2);
                drawer.Draw(game);

                var newGameState = game.ToString();

                if (newGameState != gameState)
                    MessageBox.Show("DEBUG -- Gamestate is not the same!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Could not load game state: {0}", ex.Message));
            }
        }
    }
}
