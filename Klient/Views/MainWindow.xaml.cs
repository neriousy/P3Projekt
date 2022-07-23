using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Collections.ObjectModel;

namespace Dziennik_Szkolny
{
    /// <summary>
    /// Klasa wyświetlająca panel dla ucznia/rodzica
    /// </summary>
   
    public partial class MainWindow : Window
    {
        // Utworzenie potrzebnych pól klasy
        private Students stud;
        private Parents par;
        // Implementacja dwóch interfejsów do przechowywania przedmiotów i ocen
        ICollection<Guid> unikalnePrzed;
        ICollection<Grades> oceny;

        /// <summary>
        /// Klasa przechowująca kolekcje obiektów do właściwości DataGrid
        /// </summary>

        public class DataObject
        {
            public string Godziny { get; set; }
            public string Poniedzialek { get; set; }
            public string Wtorek { get; set; }
            public string Sroda { get; set; }
            public string Czwartek { get; set; }
            public string Piatek { get; set; }

        }

        /// <summary>
        /// Konstruktor dla klasy MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnieciu przycisku exit, powodująca zamknięcie aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Metoda chowająca boczne menu hamburger 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void hideMenuButton_Click(object sender, RoutedEventArgs e)
        {
            hideMenuButton.Visibility = Visibility.Collapsed;
            showMenuButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Metoda wyświetlająca boczne menu hamburger 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void showMenuButton_Click(object sender, RoutedEventArgs e)
        {
            hideMenuButton.Visibility = Visibility.Visible;
            showMenuButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu lewym przyciskiem myszki
        /// </summary>
        /// <param name="e"></param>


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnieciu przycisku logout, powodująca wylogowanie z aplikacji 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Hide();
            login.Show();
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnieciu przycisku info, powodująca wyświetlenie podstawowcyh informacji o aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void info_Click(object sender, RoutedEventArgs e)
        {
            // Zchowanie i wyświelenie wymaganych kontrolek
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
            planZajec.Visibility = Visibility.Hidden;
            body.FontSize = 25; // Ustawienie czcionki
            body.Text = "Projekt pt. \"Dziennik szkolny\" stworzony na potrzeby zaliczenia wykładu z przedmiotu Programowanie III (2022)\n\n\nOsoby tworzące:\n · Filip Hejmowski (308166),\n · Kazimierz Daszkiewicz (308159),\n · Bartosz Konkel (308177).";
        }

        /// <summary>
        /// Metoda wywoływana po zalowaniu się na konto ucznia
        /// </summary>
        /// <param name="data"></param>

        public async void startStud(string data)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(data); // Kodowanie zestawu znaków do określonego zakresu bajtów
                MemoryStream stream = new MemoryStream(byteArray); // Utworzenie strumienia, którego magazyn zapasowy jest pamięcia
                stud = await JsonSerializer.DeserializeAsync<Students>(stream); // Deserializacja podstawowych informacji o uczniu
                dataOfUser.Text = $"{stud.dane}"; // Wyświetlenie imienia i nazwiska ucznia w prawym górnym rogi
                body.FontSize = 25; // Ustawienie czcionki
                                    // Wyświetlenie podstawowych informacji o aplikacji
                body.Text = "Projekt pt. \"Dziennik szkolny\" stworzony na potrzeby zaliczenia wykładu z przedmiotu Programowanie III (2022)\n\n\nOsoby tworzące:\n · Filip Hejmowski (308166),\n · Kazimierz Daszkiewicz (308159),\n · Bartosz Konkel (308177).";

            }
            catch (Exception ex)
            {
                string caption = "Error"; // Utworzenie nagłówku błędu
                string message = "error: " + ex.Message.ToString(); // Utworzenie wiadomości błędu
                MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error); // Wyświetlenie okienka błędu
            }
        }

        /// <summary>
        /// Metoda wywoływana po zalowaniu się na konto rodzica
        /// </summary>
        /// <param name="data"></param>

        public async void startPar(string data)
        {
            try
            {
                RestFunction question = new RestFunction(); // Utworzenie obiektu klasy RestFunction
                byte[] byteArray = Encoding.UTF8.GetBytes(data); // Kodowanie zestawu znaków do określonego zakresu bajtów
                MemoryStream stream = new MemoryStream(byteArray); // Utworzenie strumienia, którego magazyn zapasowy jest pamięcia
                par = await JsonSerializer.DeserializeAsync<Parents>(stream); // Deserializacja podstawowych informacji o rodzicu
                var jpar = JObject.Parse(data); // Wypełnienie obiektu JObject z ciągu zawierającego dane JSON
                jpar = JObject.Parse(jpar["parents_students"].ToString()); // Wypełnienie obiektu JObject z ciągu zawierającego dane JSON
                ICollection<Parents_students> parstud = JsonConvert.DeserializeObject<ICollection<Parents_students>>(jpar.ToString()); // Deserializacja informacji o rodzicach i ich dzieciach
                foreach (Parents_students p in parstud) // Pętla po wszystkich dzieciach zalogowanego rodzina
                {
                    string res = await question.makeRequestFunction(p.student_id.ToString(), "https://localhost:44307/api/Students/GetStudentByUuid"); // Wysłanie zapytania o danych ucznia
                    byteArray = Encoding.UTF8.GetBytes(res); // Kodowanie zestawu znaków do określonego zakresu bajtów
                    stream = new MemoryStream(byteArray); // Utworzenie strumienia, którego magazyn zapasowy jest pamięcia
                    stud = await JsonSerializer.DeserializeAsync<Students>(stream); // Deserializacja podstawowych informacji o uczniu
                    dataOfUser.Text = $"{stud.dane}"; // Wyświetlenie imienia i nazwiska ucznia w prawym górnym rogi
                }
                body.FontSize = 25; // Ustawienie czcionki
                                    // Wyświetlenie podstawowych informacji o aplikacji
                body.Text = "Projekt pt. \"Dziennik szkolny\" stworzony na potrzeby zaliczenia wykładu z przedmiotu Programowanie III (2022)\n\n\nOsoby tworzące:\n · Filip Hejmowski (308166),\n · Kazimierz Daszkiewicz (308159),\n · Bartosz Konkel (308177).";

            }
            catch (Exception ex)
            {
                string caption = "Error"; // Utworzenie nagłówku błędu
                string message = "error: " + ex.Message.ToString(); // Utworzenie wiadomości błędu
                MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error); // Wyświetlenie okienka błędu
            }
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu przycisku Plan zajęć, która wyświetla plan zajęć danego ucznia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Plan_Selected(object sender, RoutedEventArgs e)
        {
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Hidden;
            ocenyShow.Visibility = Visibility.Hidden;
            planZajec.Visibility = Visibility.Visible;
            body.Text = "";
            var list = new ObservableCollection<DataObject>();
            list.Add(new DataObject() { Godziny = "Godziny zajęć", Poniedzialek = "Poniedziałek", Wtorek = "Wtorek", Sroda = "Środa", Czwartek = "Czwartek", Piatek = "Piątek" });
            list.Add(new DataObject() { Godziny = "8"});
            list.Add(new DataObject() { Godziny = "9"});
            list.Add(new DataObject() { Godziny = "10"});
            list.Add(new DataObject() { Godziny = "11"});
            list.Add(new DataObject() { Godziny = "12"});
            list.Add(new DataObject() { Godziny = "13"});
            list.Add(new DataObject() { Godziny = "14"});
            list.Add(new DataObject() { Godziny = "15"});
            list[2].Poniedzialek = "XDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXDXD";
            this.planZajec.ItemsSource = list;
            planZajec.RowHeight = 65;
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu przycisku Oceny, która wyświetla oceny danego ucznia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void Oceny_Selected(object sender, RoutedEventArgs e)
        {
            // Zchowanie i wyświelenie wymaganych kontrolek
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Visible;
            bodyShow.Visibility = Visibility.Hidden;
            ocenyShow.Visibility = Visibility.Visible;
            planZajec.Visibility = Visibility.Hidden;
            body.Text = ""; // Wyzerowanie TextBlocka "body"
            nazwaPrzed.Text = ""; // Wyzerowanie TextBlocka "nazwaPrzed"
            ocenyWysw.Text = ""; // Wyzerowanie TextBlocka "ocenyWysw"
            wybierzPrzedmiot.Items.Clear(); // Wyzerowanie Comboboxa "wybierzPrzedmiot"
            // Utworzenie potrzebnych zmiennych
            bool ocena = true;
            string nazwa;
            RestFunction question = new RestFunction(); // Utworzenie obiektu klasy RestFunction
            try
            {
                string resultGrades = await question.makeRequestFunction(stud.student_id.ToString(), "https://localhost:44307/api/Grades/GetGradesStudent"); // Wysłanie zapytania o ocenach ucznia
                string resultUniqueSubj = await question.makeRequestFunction(stud.class_id.ToString(), "https://localhost:44307/api/LessonPlan/GetUniqueSubjectsByClassId"); // Wysłanie zapytania o unikalncyh przedmiotach ucznia
                if (resultGrades != "-2") // Jeżeli nie wystąpił błąd
                {
                    var jgrades = JObject.Parse(resultGrades); // Wypełnienie obiektu JObject z ciągu zawierającego dane JSON
                    var jUniqueSubj = JObject.Parse(resultUniqueSubj); // Wypełnienie obiektu JObject z ciągu zawierającego dane JSON
                    oceny = JsonConvert.DeserializeObject<ICollection<Grades>>(jgrades.ToString()); // Deserializacja informacji o ocenach ucznia
                    unikalnePrzed = JsonConvert.DeserializeObject<ICollection<Guid>>(jUniqueSubj.ToString()); // Deserializacja informacji o unikalncyh przedmiotach ucznia
                    nazwaPrzed.FontSize = 16; // Ustawienie czcionki
                    ocenyWysw.FontSize = 16; // Ustawienie czcionki
                    nazwaPrzed.Text = "Nazwa przedmiotu:\n"; // Wypełnienie TextBlocka
                    ocenyWysw.Text = "Oceny:\n"; // Wypełnienie TextBlocka
                    foreach (Guid s in unikalnePrzed) // Pętla po wszystkich unikalncyh przedmiotach ucznia
                    {
                        nazwa = await question.makeRequestFunction(s.ToString(), "https://localhost:44307/api/Subjects/GetSubjectNameByUuid"); // Wysłanie zapytania o nazwie przedmiotu
                        nazwaPrzed.Text += "\n" + nazwa; // Wypełnienie TextBlocka
                        ocenyWysw.Text += "\n"; // Wypełnienie TextBlocka
                        wybierzPrzedmiot.Items.Add(nazwa); // Dodanie przedmiotu do Comboboxa
                        foreach (Grades o in oceny) // Pętla po wszystkich ocenach ucznia
                        {
                            if (s == o.Subject_id) // Jeżeli ocena jest z aktualnie wypisywanego przedmiotu
                            {
                                ocenyWysw.Text += o.Grade + ", "; // Wypełnienie TextBlocka
                                ocena = false; // Ustawienie zmiennej bool na false
                            }
                        }
                        if (ocena) // Jeżeli uczeń nie ma żadnych ocen z danego przedmiotu
                        {
                            ocenyWysw.Text += "Brak ocen"; // Wypełnienie TextBlocka
                        }
                        nazwaPrzed.Text += "\n"; // Wypełnienie TextBlocka
                        ocenyWysw.Text += "\n"; // Wypełnienie TextBlocka
                        for (int i = 0; i < nazwaPrzed.ActualWidth / 7 + 3; i++) // Pętla po szerokoście TextBlocka
                        {
                            nazwaPrzed.Text += "-"; // Wypełnienie TextBlocka znakami "-"
                        }
                        for (int i = 0; i < ocenyWysw.ActualWidth / 7; i++) // Pętla po szerokoście TextBlocka
                        {
                            ocenyWysw.Text += "-"; // Wypełnienie TextBlocka znakami "-"
                        }
                        ocena = true; // Ustawienie zmiennej bool na true
                    }
                }
                else // W przypadku gdy uczeń nie ma żadnych ocen
                {
                    body.FontSize = 20; // Ustawienie czcionki
                    body.Text = "Brak ocen"; // Wypełnienie TextBlocka
                }
            }
            catch (Exception ex)
            {
                string caption = "Error"; // Utworzenie nagłówku błędu
                string message = "error: " + ex.Message.ToString(); // Utworzenie wiadomości błędu
                MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error); // Wyświetlenie okienka błędu
            }
            
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu przycisku Uwagi, która wyświetla uwagi danego ucznia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void Uwagi_Selected(object sender, RoutedEventArgs e)
        {
            // Zchowanie i wyświelenie wymaganych kontrolek
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
            planZajec.Visibility = Visibility.Hidden;
            body.Text = ""; // Wyzerowanie TextBlocka "body"
            RestFunction question = new RestFunction(); // Utworzenie obiektu klasy RestFunction
            try
            {
                string result = await question.makeRequestFunction(stud.student_id.ToString(), "https://localhost:44307/api/Warnings/GetStudentWarnings"); // Wysłanie zapytania o uwagach ucznia
                if (result != "-2") // Jeżeli nie wystąpił błąd
                {
                    var jwarns = JObject.Parse(result); // Wypełnienie obiektu JObject z ciągu zawierającego dane JSON
                    ICollection<Warnings> uwagi = JsonConvert.DeserializeObject<ICollection<Warnings>>(jwarns.ToString()); // Deserializacja informacji o uwagach ucznia
                    body.FontSize = 16; // Ustawienie czcionki
                    body.Text = ""; // Wyzerowanie TextBlocka "body"
                    foreach (Warnings w in uwagi) // Pętla po wszystkich uwagach ucznia
                    {
                        // Wypełenie Textblocka
                        body.Text += "\n\n" + "Opis: " + w.desc + "\nNauczyciel wystawiający: " + await question.makeRequestFunction(w.teacher_id.ToString(), "https://localhost:44307/api/Teachers/GetTeacherNameByUuid") + "\nData wystawienia: " + w.date + "\n\n";
                        for (int i = 0; i < body.ActualWidth / 7; i++) // Pętla po szerokoście TextBlocka
                        {
                            body.Text += "-"; // Wypełnienie TextBlocka znakami "-"
                        }
                    }
                }
                else // W przypadku gdy uczeń nie ma żadnych uwag
                {
                    body.FontSize = 20; // Ustawienie czcionki
                    body.Text = "Brak uwag"; // Wypełnienie TextBlocka
                }
            }
            catch (Exception ex)
            {
                string caption = "Error"; // Utworzenie nagłówku błędu
                string message = "error: " + ex.Message.ToString(); // Utworzenie wiadomości błędu
                MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error); // Wyświetlenie okienka błędu
            }
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu przycisku Obecności, która wyświetla obecności danego ucznia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Obecnosci_Selected(object sender, RoutedEventArgs e)
        {
            // Zchowanie i wyświelenie wymaganych kontrolek
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            Wybór_ocen.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
            planZajec.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///  Metoda wywołująca się po naciśnięciu przycisku Oceny, która wyświetla oceny danego ucznia w pościaci kolumn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void wyswOceny_Click(object sender, RoutedEventArgs e)
        {
            // Zchowanie i wyświelenie wymaganych kontrolek
            Wybor_przedmiotu.Visibility = Visibility.Hidden;
            bodyShow.Visibility = Visibility.Hidden;
            ocenyShow.Visibility = Visibility.Visible;
            planZajec.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Metoda wywołująca się po naciśnięciu przycisku Szczegóły ocen, która wyświetla szczegółowe informacje o ocenach z wybranego przedmiotu danego ucznia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void wyswSzczegoly_Click(object sender, RoutedEventArgs e)
        {
            // Zchowanie i wyświelenie wymaganych kontrolek
            Wybor_przedmiotu.Visibility = Visibility.Visible;
            bodyShow.Visibility = Visibility.Visible;
            ocenyShow.Visibility = Visibility.Hidden;
            planZajec.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///  Metoda wywołująca się po zmianie przedmiotu w Comboboxie przez użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void wybierzPrzedmiot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Utworzenie potrzebnych zmiennych
            bool ocena = true;
            int i = 0;
            RestFunction question = new RestFunction(); // Utworzenie obiektu klasy RestFunction
            body.FontSize = 16; // Ustawienie czcionki
            body.Text = "\n\n"; // Wypełnienie TextBlocku dwoma pustymi liniami
            try
            {
                foreach (Guid s in unikalnePrzed) // Pętla po wszystkich unikalnych przedmiotach ucznia
                {
                    if (wybierzPrzedmiot.SelectedIndex == i) // Jeżeli przedmiot równa się wybranemu przedmiotowi w ComboBoxie
                    {
                        foreach (Grades o in oceny) // Pętla po wszystkich ocenach ucznia
                        {
                            if (s == o.Subject_id) // Jeżeli ocena jest z aktualnie wypisywanego przedmiotu
                            {
                                // Wypełenie TextBlocka informacjami o ocenie z wybranego przedmiotu
                                body.Text += "\n\n" + "Ocena: " + o.Grade + "\nWaga: " + o.Weight + "\nWystawiona: " + await question.makeRequestFunction(o.Teacher_id.ToString(), "https://localhost:44307/api/Teachers/GetTeacherNameByUuid") + ", " + o.Date + "\n\n";
                                for (int j = 0; j < body.ActualWidth / 7; j++) // Pętla po szerokoście TextBlocka
                                {
                                    body.Text += "-"; // Wypełnienie TextBlocka znakami "-"
                                }
                                ocena = false; // Ustawienie zmiennej bool na false
                            }
                        }
                        if (ocena) // Jeżeli z wybranego przedmiotu nie ma ocen
                        {
                            body.FontSize = 20; // Ustawienie czcionki
                            body.Text += "Brak ocen"; // Wypełnienie TextBlocka
                        }
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                string caption = "Error"; // Utworzenie nagłówku błędu
                string message = "error: " + ex.Message.ToString(); // Utworzenie wiadomości błędu
                MessageBox.Show(message, caption, (MessageBoxButton)System.Windows.Forms.MessageBoxButtons.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error); // Wyświetlenie okienka błędu
            }
        }
    }
}