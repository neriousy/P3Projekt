using System.Text.Json;
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
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string message;
            string caption = "Error";
            char type;
            RestLoginStudent clientStudent = new RestLoginStudent();
            RestLoginParent clientParent = new RestLoginParent();
            string result = await clientStudent.makeRequestLogin(userInputtedUsername.Text, userInputtedPassword.Password.ToString());
            if (result != "-1")
            {
                type = 's';
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.Show();
                mainWindow.start(result, type);
            }
            /// Wprowadzono niepoprawne dane do logowania
            else
            {
                message = "Niepoprawne dane logowania";
                MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error);
            }


        }

    }
}
