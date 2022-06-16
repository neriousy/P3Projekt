using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;


namespace Dziennik_Szkolny
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        public bool darkThemeTurnedOn { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        public void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void changeTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (theme.GetBaseTheme() == BaseTheme.Dark)
            {
                darkThemeTurnedOn = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                darkThemeTurnedOn = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            
        }


        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            
            RestLogin client = new RestLogin();
            if(client.makeRequest(userInputtedUsername.Text, userInputtedPassword.Password.ToString()).ToString() != "-1")
            {
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.Show();
            }
            
            
        }

    }
}
