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
using System.Windows.Shapes;

namespace FindWayGame
{
    /// <summary>
    /// Interaction logic for CongratWindow.xaml
    /// </summary>
    public partial class CongratWindow : Window
    {
        public GameInfo Game { get; set; }

        public CongratWindow(int? id, GameInfo info)
        {
            InitializeComponent();

            Game = info;
            if (id.HasValue)
            {
                using (GameContext ctx = new GameContext())
                {
                    var player = ctx.Players.Find(id.Value);
                    player.Games.Add(info);
                    ctx.SaveChanges();
                }

                this.btnRegister.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.btnRegister.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow
            {
                Game = this.Game
            };
            register.Show();
            this.Close();
        }
    }
}
