using FindWayGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FindWayGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string NOT_REGISTERED_STRING = "Not registered";

        public int? PlayerId { get; set; }

        public GameInfo Info { get; set; }

        public MainWindow(string nickname)
        {
            InitializeComponent();

            if (nickname != null)
            {
                this.lblNickname.Content = nickname;
            }
            else
            {
                this.lblNickname.Content = NOT_REGISTERED_STRING;
            }
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == this.btn00)
            {
                btn.Content = this.Resources["tick"];
                Info.IsWon = true;

                // User decides what to do with the result
                CongratWindow cong = new CongratWindow(PlayerId, Info);
                cong.ShowDialog();

                newGame();
            }
        }

        private void clearAllCells()
        {
            for (int i = 0; i < this.gameGrid.Children.Count; i++)
            {
                (gameGrid.Children[i] as Button).Content = this.Resources["questionMark"];
            }
        }

        private void newGame()
        {
            Info = new GameInfo();
            clearAllCells();
        }
    }
}
