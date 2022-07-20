using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Drawing;

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

        public async void startStud(string data)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            stud = await JsonSerializer.DeserializeAsync<Students>(stream);
            var jgrades = JObject.Parse(data);
            ICollection<Grades> oceny = JsonConvert.DeserializeObject<ICollection<Grades>>(jgrades["grades"].ToString());
            dataOfUser.Text = $"{stud.dane}";
            body.Text = "xd";
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
        public async void startPar(string data)
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

        private void Plan_Selected(object sender, RoutedEventArgs e)
        {
            body.Text = "XDDD";
        }

        private async void Oceny_Selected(object sender, RoutedEventArgs e)
        {

            RestFunction grades = new RestFunction();
            string result = await grades.makeRequestFunction(stud.student_id.ToString(), "https://localhost:44307/api/Grades/GetGradesStudent");
            if (result != "-2")
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(result);
                MemoryStream stream = new MemoryStream(byteArray);
                var jgrades = JObject.Parse(result);
                ICollection<Grades> oceny = JsonConvert.DeserializeObject<ICollection<Grades>>(jgrades.ToString());
                body.FontSize = 16;
                body.Text = "Nazwa przedmiotu:\tOceny:\n";
                foreach (Grades o in oceny)
                {
                    body.Text += "\n" + o.Subject_id + "\t";
                    foreach (Grades oc in oceny)
                    {
                        if(o.Subject_id == oc.Subject_id)
                        {
                            body.Text += oc.Grade + ", ";
                        }
                    }
                    body.Text += "\n";
                    for (int i = 0; i < body.ActualWidth / 7; i++)
                    {
                        body.Text += "-";
                    }
                }
            }
            else
            {
                body.FontSize = 20;
                body.Text = "Brak ocen";
            }
        }

        private void Szczegoly_Selected(object sender, RoutedEventArgs e)
        {

        }

        private async void Uwagi_Selected(object sender, RoutedEventArgs e)
        {
            RestFunction warn = new RestFunction();
            string result = await warn.makeRequestFunction(stud.student_id.ToString(), "https://localhost:44307/api/Warnings/GetStudentWarnings");
            if(result != "-2")
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(result);
                MemoryStream stream = new MemoryStream(byteArray);
                var jwarns = JObject.Parse(result);
                ICollection<Warnings> uwagi = JsonConvert.DeserializeObject<ICollection<Warnings>>(jwarns.ToString());
                body.FontSize = 16;
                body.Text = "";
                foreach (Warnings w in uwagi)
                {
                    body.Text += "\n\n" + "Opis: " + w.desc + "\nNauczyciel wystawiający: " + w.teacher_id + "\nData wystawienia: " + w.date + "\n\n";
                    for (int i = 0; i < body.ActualWidth / 7; i++)
                    {
                        body.Text += "-";
                    }
                }
            }
            else
            {
                body.FontSize = 20;
                body.Text = "Brak uwag";
            }
        }

        private void Obecnosci_Selected(object sender, RoutedEventArgs e)
        {

        }    }
}