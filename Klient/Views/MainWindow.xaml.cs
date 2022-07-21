using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System;
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
        ICollection<Guid> unikalnePrzed;
        ICollection<Grades> oceny;
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
        private void info_Click(object sender, RoutedEventArgs e)
        {
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
            body.FontSize = 25;
            body.Text = "Projekt pt. \"Dziennik szkolny\" stworzony na potrzeby zaliczenia wykładu z przedmiotu Programowanie III (2022)\n\n\nOsoby tworzące:\n · Filip Hejmowski (308166),\n · Kazimierz Daszkiewicz (308159),\n · Bartosz Konkel (308177).";
        }

        public async void startStud(string data)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            stud = await JsonSerializer.DeserializeAsync<Students>(stream);
            dataOfUser.Text = $"{stud.dane}";
            body.FontSize = 25;
            body.Text = "Projekt pt. \"Dziennik szkolny\" stworzony na potrzeby zaliczenia wykładu z przedmiotu Programowanie III (2022)\n\n\nOsoby tworzące:\n · Filip Hejmowski (308166),\n · Kazimierz Daszkiewicz (308159),\n · Bartosz Konkel (308177).";
        }
        public async void startPar(string data)
        {
            RestFunction question = new RestFunction();
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            par = await JsonSerializer.DeserializeAsync<Parents>(stream);
            var jpar = JObject.Parse(data);
            jpar = JObject.Parse(jpar["parents_students"].ToString());
            ICollection<Parents_students> parstud = JsonConvert.DeserializeObject<ICollection<Parents_students>>(jpar.ToString());
            foreach(Parents_students p in parstud)
            {
                string res = await question.makeRequestFunction(p.student_id.ToString(), "https://localhost:44307/api/Students/GetStudentByUuid");
                byteArray = Encoding.UTF8.GetBytes(res);
                stream = new MemoryStream(byteArray);
                stud = await JsonSerializer.DeserializeAsync<Students>(stream);
                dataOfUser.Text = $"{stud.dane}";
            }
            body.FontSize = 25;
            body.Text = "Projekt pt. \"Dziennik szkolny\" stworzony na potrzeby zaliczenia wykładu z przedmiotu Programowanie III (2022)\n\n\nOsoby tworzące:\n · Filip Hejmowski (308166),\n · Kazimierz Daszkiewicz (308159),\n · Bartosz Konkel (308177).";
        }

        private void Plan_Selected(object sender, RoutedEventArgs e)
        {
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            body.Text = "";
        }

        private async void Oceny_Selected(object sender, RoutedEventArgs e)
        {
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Visible;
            bodyShow.Visibility = Visibility.Hidden;
            ocenyShow.Visibility = Visibility.Visible;
            body.Text = "";
            nazwaPrzed.Text = "";
            ocenyWysw.Text = "";
            wybierzPrzedmiot.Items.Clear();
            bool ocena = true;
            string nazwa;
            RestFunction question = new RestFunction();
            string resultGrades = await question.makeRequestFunction(stud.student_id.ToString(), "https://localhost:44307/api/Grades/GetGradesStudent");
            string resultUniqueSubj = await question.makeRequestFunction(stud.class_id.ToString(), "https://localhost:44307/api/LessonPlan/GetUniqueSubjectsByClassId");
            if (resultGrades != "-2")
            {
                var jgrades = JObject.Parse(resultGrades);
                var jUniqueSubj = JObject.Parse(resultUniqueSubj);
                oceny = JsonConvert.DeserializeObject<ICollection<Grades>>(jgrades.ToString());
                unikalnePrzed = JsonConvert.DeserializeObject<ICollection<Guid>>(jUniqueSubj.ToString());
                nazwaPrzed.FontSize = 16;
                ocenyWysw.FontSize = 16;
                nazwaPrzed.Text = "Nazwa przedmiotu:\n"; 
                ocenyWysw.Text = "Oceny:\n";
                foreach (Guid s in unikalnePrzed)
                {
                    nazwa = await question.makeRequestFunction(s.ToString(), "https://localhost:44307/api/Subjects/GetSubjectNameByUuid");
                    nazwaPrzed.Text += "\n" + nazwa;
                    ocenyWysw.Text += "\n";
                    wybierzPrzedmiot.Items.Add(nazwa);
                    foreach (Grades o in oceny)
                    {
                        if (s == o.Subject_id)
                        {
                            ocenyWysw.Text += o.Grade + ", ";
                            ocena = false;
                        }
                    }
                    if (ocena)
                    {
                        ocenyWysw.Text += "Brak ocen";
                    }
                    nazwaPrzed.Text += "\n";
                    ocenyWysw.Text += "\n";
                    for (int i = 0; i < nazwaPrzed.ActualWidth / 7 + 3; i++)
                    {
                        nazwaPrzed.Text += "-";
                    }
                    for (int i = 0; i < ocenyWysw.ActualWidth / 7; i++)
                    {
                        ocenyWysw.Text += "-";
                    }
                    ocena = true;
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
            Wybor_przedmiotu.Visibility = Visibility.Visible;
        }

        private async void Uwagi_Selected(object sender, RoutedEventArgs e)
        {
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
            body.Text = "";
            RestFunction question = new RestFunction();
            string result = await question.makeRequestFunction(stud.student_id.ToString(), "https://localhost:44307/api/Warnings/GetStudentWarnings");
            if(result != "-2")
            {
                var jwarns = JObject.Parse(result);
                ICollection<Warnings> uwagi = JsonConvert.DeserializeObject<ICollection<Warnings>>(jwarns.ToString());
                body.FontSize = 16;
                body.Text = "";
                foreach (Warnings w in uwagi)
                {
                    body.Text += "\n\n" + "Opis: " + w.desc + "\nNauczyciel wystawiający: " + await question.makeRequestFunction(w.teacher_id.ToString(), "https://localhost:44307/api/Teachers/GetTeacherNameByUuid") + "\nData wystawienia: " + w.date + "\n\n";
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
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
        }

        private void wyswOceny_Click(object sender, RoutedEventArgs e)
        {
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Hidden;
            ocenyShow.Visibility = Visibility.Visible;

        }

        private void wyswSzczegoly_Click(object sender, RoutedEventArgs e)
        {
            Wybor_przedmiotu.Visibility = Visibility.Visible;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
        }

        private async void wybierzPrzedmiot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            bool ocena = true;
            int i = 0;
            RestFunction question = new RestFunction();
            body.FontSize = 16;
            body.Text = "\n\n";
            foreach(Guid s in unikalnePrzed)
            {
                if(wybierzPrzedmiot.SelectedIndex == i)
                {
                    foreach (Grades o in oceny)
                    {
                        if (s == o.Subject_id)
                        {
                            body.Text += "\n\n" + "Ocena: " + o.Grade + "\nWaga: " + o.Weight + "\nWystawiona: " + await question.makeRequestFunction(o.Teacher_id.ToString(), "https://localhost:44307/api/Teachers/GetTeacherNameByUuid") + ", " + o.Date + "\n\n";
                            for (int j = 0; j < body.ActualWidth / 7; j++)
                            {
                                body.Text += "-";
                            }
                            ocena = false;
                        }
                    }
                    if (ocena)
                    {
                        body.FontSize = 20;
                        body.Text += "Brak ocen";
                    }
                }
                i++;
            }
        }
    }
}