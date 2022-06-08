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
using System.Diagnostics;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Net;
using System.Net.Sockets;
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

        async private void loginButton_Click(object sender, RoutedEventArgs e)
        {            
        /*    Socket clientSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            EndPoint serverAddress = new IPEndPoint(IPAddress.Loopback, 7777);
            await Task.Factory.StartNew(() => clientSocket.Connect(serverAddress));
            NetworkStream strumienPolaczenia = new NetworkStream(clientSocket);
            StreamWriter strumienZapisu = new StreamWriter(strumienPolaczenia);
            StreamReader strumienOdczytu = new StreamReader(strumienPolaczenia);
            (string, string) wiadomoscWys = (userInputtedUsername.Text, userInputtedPassword.Password.ToString());
            await Task.Factory.StartNew(() => { strumienZapisu.WriteLine(wiadomoscWys); });
            await Task.Factory.StartNew(strumienZapisu.Flush);
            (int, char) wiadomoscOdb = await Task<(int, char)>.Factory.StartNew(strumienOdczytu.ReadLine);
            strumienZapisu.Close();
            strumienOdczytu.Close();
            strumienPolaczenia.Close();
            clientSocket.Close();

            if(wiadomoscOdb.Item1 != -1)
            {
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Hide();
                main.Show();
            }
            else
            {
                string message = "Wprowadzono niepoprawne dane, sprobuj ponownie!";
                string title = "Blad";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(message, title, button);
            }*/
        }
    }
}
