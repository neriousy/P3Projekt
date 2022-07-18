using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
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
        private Students stud;
        private Parents par;
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

        public async void startStud(string data, char type)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            stud = await JsonSerializer.DeserializeAsync<Students>(stream);
            var jgrades = JObject.Parse(data);
            ICollection<Grades> oceny = JsonConvert.DeserializeObject<ICollection<Grades>>(jgrades["grades"].ToString());
            dataOfUser.Text = $"{stud.dane}";
            body.Text = "";
            //DataTable grades = new DataTable();
            //grades.Columns.Add("Nazwa", typeof(string));
            //grades.Columns.Add("Ocena", typeof(int));
            //foreach (Grades o in oceny)
            {
                //grades.Rows.Add(o.Subject_id, o.Ocena);
            }
            //body.Text = grades.Columns[0].ToString() + "\t\t\t\t" + grades.Columns[1].ToString();
            //body.Text = grades.Rows[0].ToString();
        }
        public async void startPar(string data, char type)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            par = await JsonSerializer.DeserializeAsync<Parents>(stream);
            var jpar = JObject.Parse(data);
            //jpar = JObject.Parse(jpar["parents_students"].ToString());
            //var jstud = JObject.Parse(jpar["student"].ToString());
            //ICollection<Students> student = JsonConvert.DeserializeObject<ICollection<Students>>(jstud["student"].ToString());
            //stud = JsonConvert.DeserializeObject<Students>(jstud["student"].ToString());
            //dataOfUser.Text = $"{stud.dane}";
            //body.Text = $"{jstud.ToString()}";
            //DataTable grades = new DataTable();
            //grades.Columns.Add("Nazwa", typeof(string));
            //grades.Columns.Add("Ocena", typeof(int));
            //foreach (Grades o in oceny)
            {
                //grades.Rows.Add(o.Subject_id, o.Ocena);
            }
            //body.Text = grades.Columns[0].ToString() + "\t\t\t\t" + grades.Columns[1].ToString();
            //body.Text = grades.Rows[0].ToString();
        }
    }
}