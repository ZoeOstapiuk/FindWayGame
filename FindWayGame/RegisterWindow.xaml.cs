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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public GameInfo Game { get; set; }

        // Used for feedback to main window
        public int? JustRegisteredPlayerId { get; set; }

        public RegisterWindow(GameInfo game)
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (GameContext ctx = new GameContext())
            {
                var player = ctx.Players
                    .FirstOrDefault(p => p.Nickname == this.txtNickname.Text && p.Password == this.txtPassword.Text);
                if (player != null)
                {
                    MessageBox.Show("Such player already exists!", this.Title);
                    return;
                }
                else
                {
                    player = new Player
                    {
                        Nickname = this.txtNickname.Text,
                        Password = this.txtPassword.Text,
                    };
                    player.Games.Add(Game);

                    ctx.Players.Add(player);
                    ctx.SaveChanges();

                    JustRegisteredPlayerId = player.PlayerId;
                }
            }

            this.Close();
        }
    }
}
