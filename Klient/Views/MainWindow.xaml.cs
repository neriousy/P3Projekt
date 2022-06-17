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
using MaterialDesignThemes;

using System.Text.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dziennik_Szkolny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Students? stud;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void hideMenuButton_Click(object sender, RoutedEventArgs e)
        {
            hideMenuButton.Visibility = Visibility.Collapsed;
            showMenuButton.Visibility = Visibility.Visible;
        }

        private void showMenuButton_Click(object sender, RoutedEventArgs e)
        {
            hideMenuButton.Visibility = Visibility.Visible;
            showMenuButton.Visibility = Visibility.Collapsed;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Hide();
            login.Show();
        }

        public void start(string data)
        {
            stud =  JsonSerializer.Deserialize<Students?>(data);
            var jgrades = JObject.Parse(data);
            ICollection<Grades> oceny = JsonConvert.DeserializeObject<ICollection<Grades>>(jgrades["grades"].ToString());
            dataOfUser.Text = $"{stud?.Dane}";
            //body.Text = oceny.Count.ToString();
            foreach(Grades o in oceny)
            {
                if(o.Teacher_id != o.Student_id)
                {
                    body.Text += o.Teacher_id + " " + o.Student_id + " ";
                }
                
            }
        }
    }
}