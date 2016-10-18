using FindWayGame.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FindWayGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string NOT_REGISTERED_STRING = "Not registered";
        private Point currentPosition;
        private int? playerId;

        public int? PlayerId
        {
            get
            {
                return playerId;
            }
            set
            {
                if (!value.HasValue)
                {
                    playerId = null;
                    this.lblNickname.Content = NOT_REGISTERED_STRING;
                    return;
                }

                using (GameContext ctx = new GameContext())
                {
                    Player player = ctx.Players.Find(value.Value);
                    if (player == null)
                    {
                        playerId = null;
                        this.lblNickname.Content = NOT_REGISTERED_STRING;
                    }
                    else
                    {
                        playerId = value;
                        this.lblNickname.Content = player.Nickname;
                    }
                } 
            }
        }

        public GameInfo Game { get; set; }

        // Use generateGamefieldMatrix()
        public bool[,] Field { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            newGame();
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            checkMatch(btn);
        }

        private void onLost()
        {
            LoseWindow lost = new LoseWindow();
            lost.ShowDialog();

            Game.Attempts++;
            clearAllCells();
        }

        private void onWin()
        {
            CongratWindow cong = new CongratWindow(AddGameToDb());

            // Player wants to register
            if (cong.ShowDialog() == true)
            {
                RegisterWindow reg = new RegisterWindow(Game);
                reg.ShowDialog();
                PlayerId = reg.JustRegisteredPlayerId;
            }

            newGame();
        }
       
        private bool AddGameToDb()
        {
            if (!PlayerId.HasValue)
            {
                return false;
            }

            using (GameContext ctx = new GameContext())
            {
                ctx.Players.Find(PlayerId.Value).Games.Add(Game);
                ctx.SaveChanges();
            }

            return true;
        }

        private string getNicknameById(int id)
        {
            string nickname = null;
            using (GameContext ctx = new GameContext())
            {
                nickname = ctx.Players.Find(id).Nickname;
            }

            return nickname;
        }

        #region Game set up functions
        private void clearAllCells()
        {
            currentPosition.X = 0;
            currentPosition.Y = 0;

            (gameGrid.Children[0] as Button).Content = Application.Current.FindResource("waterlilyWithFrog");
            (gameGrid.Children[this.gameGrid.Children.Count - 1] as Button).Content = Application.Current.FindResource("waterlilyWithPrincess");
            (gameGrid.Children[this.gameGrid.Children.Count - 1] as Button).IsEnabled = true;

            for (int i = 1; i < this.gameGrid.Children.Count - 1; i++)
            {
                (gameGrid.Children[i] as Button).Content = Application.Current.FindResource("leaf");
                (gameGrid.Children[i] as Button).IsEnabled = true;
            }
        }

        private void newGame()
        {
            Game = new GameInfo();
            clearAllCells();
            generateGamefieldMatrix();
        }

        private void generateGamefieldMatrix()
        {
            int rowCount = this.gameGrid.RowDefinitions.Count;
            int colCount = this.gameGrid.ColumnDefinitions.Count;
            Field = new bool[rowCount, colCount];

            // TODO: make enum
            Random rnd = new Random();
            int row = 0;
            int col = 0;
            while (row < rowCount && col < colCount)
            {
                Field[row, col] = true;

                // The lowest row
                if (row == rowCount - 1)
                {
                    while (++col != colCount)
                    {
                        Field[row, col] = true;
                    }

                    return;
                }

                // The most right col
                if (col == colCount - 1)
                {
                    while (++row != rowCount)
                    {
                        Field[row, col] = true;
                    }

                    return;
                }

                // Go down
                if (rnd.Next(2) == 0)
                {
                    row++;
                }
                // Go right
                else
                {
                    col++;
                }
            }
        }

        private void checkMatch(Button sender)
        {
            int index = 0;
            foreach (var item in this.gameGrid.Children)
            {
                if (sender == item)
                {
                    break;
                }

                index++;
            }

            int y = index < this.gameGrid.RowDefinitions.Count ? index : index % this.gameGrid.RowDefinitions.Count;
            int x = index / this.gameGrid.RowDefinitions.Count;

            if (Math.Abs(y - (int)currentPosition.Y) > 1 || Math.Abs(x - (int)currentPosition.X) > 1)
            {
                MessageBox.Show("You can't jump that far!", this.Title);
                return;
            }

            (this.gameGrid.Children[(int)currentPosition.X * this.gameGrid.RowDefinitions.Count + (int)currentPosition.Y] as Button).Content = Application.Current.FindResource("waterlily");

            currentPosition.Y = y;
            currentPosition.X = x;
            if (Field[(int)currentPosition.Y, (int)currentPosition.X])
            {
                sender.IsEnabled = false;

                if ((int)currentPosition.Y == this.gameGrid.RowDefinitions.Count - 1 &&
                    (int)currentPosition.X == this.gameGrid.ColumnDefinitions.Count - 1)
                {
                    sender.Content = Application.Current.FindResource("waterlilyWithBoth");
                    onWin();
                }
                else
                {
                    sender.Content = Application.Current.FindResource("waterlilyWithFrog");
                }
            }
            else
            {
                sender.Content = Application.Current.FindResource("waves");
                onLost();
            }
        }
        #endregion
    }
}
