using System.Windows;
using FindWayGame.Entities;
using System.Linq;

namespace FindWayGame
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            using (GameContext ctx = new GameContext())
            {
                Player player = ctx.Players
                    .FirstOrDefault(p => p.Nickname == this.txtNickname.Text && p.Password == this.txtPassword.Text);
                
                if (player == null)
                {
                    MessageBox.Show("Nickname or password was wrong!", this.Title);
                }
                else
                {
                    MainWindow main = new MainWindow()
                    {
                        PlayerId = player.PlayerId,
                    };
                    main.Show();
                    this.Close();
                }
            }
        }

        private void btnNewbee_Click(object sender, RoutedEventArgs e)
        {
            // Player and GameInfo are null
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
