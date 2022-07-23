using System;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace Dziennik_Szkolny
{
    class RestLogin
    {
        /// <summary>
        /// Sprawdzenienie danych logowania
        /// </summary>
        /// <param name="login">email</param>
        /// <param name="haslo">password</param>
        /// <returns>W przypadku powodzenia : Struktura danych użytkownika
        ///          W przypadku niepowodzenia : -1</returns>
        public async Task<string> makeRequestLogin(string login, string haslo, string url)
        {
            // Utworzenie potrzebnych zmienncyh
            string message;
            string caption = "Error";
            var strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // Utworzenie identyfikatora HttpWebRequest URI (Uniform Resource Identifier) 
            var postData = "email=" + Uri.EscapeDataString(login); // Wypełenie danych do wysłania w postaci formualarza
            postData += "&password=" + Uri.EscapeDataString(haslo); // Wypełenie danych do wysłania w postaci formualarza
            var data = Encoding.ASCII.GetBytes(postData); // Kodowanie zestaw znaków w sekwencji bajtów
            request.Method = "POST"; // Ustawienie metody zapytania na POST
            request.ContentType = "application/x-www-form-urlencoded"; // Ustawienie sposoby przesyłu danych w postaci jednego formularza
            request.ContentLength = data.Length; // Ustawienie długości przesyłanych danych
            HttpWebResponse response = null; // Utworzenie obiektu "response", służącego do przechowywania odpowiedzi

            try
            {
                using (var stream = await request.GetRequestStreamAsync()) // Pobranie obektu używanego do zapisu asynchronicznie
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)(await request.GetResponseAsync()); // Zwraca odpowiedź na żądanie internetowe asynchronicznie

                if (response.StatusCode == HttpStatusCode.OK) // Sprawdza czy jest poprawny status
                {

                    using (Stream responseStream = response.GetResponseStream()) // Pobranie strumienia używanego do odczytywania treści odpowiedzi z serwera
                    {
                        using (StreamReader reader = new StreamReader(responseStream)) // Utworzenia obiektu StreamReader
                        {
                            strResponseValue = await reader.ReadToEndAsync(); // Odczytanie wszystkich znaków z bieżącej pozycji na końcu strumienia asynchronicznie
                        }
                    }
                }
            }
            // W przypadku wystąpienia błędu
            catch (Exception ex)
            {
                message = "error: " + ex.Message.ToString(); // Utworzenie wiadomości błędu
                if (message != "error: The remote server returned an error: (404) Not Found.") // Gdy jest różna od nie występowania w bazie
                {
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error); // Wyświetlenie okienka błędu

                }
                strResponseValue = "-1";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue; // Zwrócenie odpowiedzi
        }
    }
}   
