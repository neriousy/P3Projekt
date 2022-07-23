using System;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;


namespace Dziennik_Szkolny
{

    /// <summary>
    /// Klasa wyświetlająca panel logowania dla ucznia/rodzica
    /// </summary>

    public partial class LoginWindow : Window
    {

        /// <summary>
        /// Konstruktor klasy LoginWindow
        /// </summary>

        public LoginWindow()
        {
            InitializeComponent();
        }

        // Utworzenie potrzebych pół
        public bool darkThemeTurnedOn { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        /// <summary>
        /// Metoda wywołująca się po naciśnieciu przycisku exit, powodująca zamknięcie aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnieciu przycisku zmiany motywu, powodująca zmianę kolorystyki aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu lewym przyciskiem myszki
        /// </summary>
        /// <param name="e"></param>

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu przycisku Zaloguj się, powodująca sprawdzenie danych logowania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // Utworzenie potrzebnych zmienncyh
            string message;
            string caption = "Error";            
            RestLogin login = new RestLogin(); // Utworzenie obiektu klasy RestLogin
            try
            {
                string result = await login.makeRequestLogin(userInputtedUsername.Text, userInputtedPassword.Password.ToString(), "https://localhost:44307/api/Students/PostGetStudent"); // Wysłanie zapytania o poprawnościu danych ucznia
                if (result != "-1") // Gdy dane pasują do danych jednego z uczniów
                {
                    MainWindow mainWindow = new MainWindow(); // Utworzenie obiektu klasy MainWindow
                    this.Hide(); // Ukrycie bieżącego okienka
                    mainWindow.Show(); // Wyświetlenie okienka MainWindow
                    mainWindow.startStud(result); // Wywołanie metody "startStud"
                }
                else // W innym przypadku
                {
                    result = await login.makeRequestLogin(userInputtedUsername.Text, userInputtedPassword.Password.ToString(), "https://localhost:44307/api/Parents/PostGetParent"); // Wysłanie zapytania o poprawnościu danych rodzica
                    if (result != "-1") // Gdy dane pasują do danych jednego z rodziców
                    {
                        MainWindow mainWindow = new MainWindow(); // Utworzenie obiektu klasy MainWindow
                        this.Hide(); // Ukrycie bieżącego okienka
                        mainWindow.Show(); // Wyświetlenie okienka MainWindow
                        mainWindow.startPar(result); // Wywołanie metody "startStud"
                    }
                    else // W innym przypadku
                    {
                        result = await login.makeRequestLogin(userInputtedUsername.Text, userInputtedPassword.Password.ToString(), "https://localhost:44307/api/Teachers/PostGetTeacher"); // Wysłanie zapytania o poprawnościu danych nauczyciela
                        if (result != "-1")
                        {
                            MainWindow mainWindow = new MainWindow(); // Utworzenie obiektu klasy MainWindow
                            this.Hide(); // Ukrycie bieżącego okienka
                            mainWindow.Show(); // Wyświetlenie okienka MainWindow
                                               //mainWindow.startStud(result); // Wywołanie metody "startStud"
                        }
                        else // Wprowadzone dane nie pasują co żadnego użytkownika w bazie danych
                        {
                            // Wyświetlenie odpowiedniego MessageBoxa
                            message = "Niepoprawne dane logowania";
                            MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = "error: " + ex.Message.ToString(); // Utworzenie wiadomości błędu
                MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error); // Wyświetlenie okienka błędu
            }
        }
    }
}
