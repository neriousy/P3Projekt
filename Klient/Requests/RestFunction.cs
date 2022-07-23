using System;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Dziennik_Szkolny
{

    /// <summary>
    /// Klasa pozwalająca wykonywanie podstawowych zapytań do serwera
    /// </summary>

    class RestFunction
    {

        /// <summary>
        /// Metoda wysyłająca podstawowe zapytania do serwera
        /// </summary>
        /// <param name="uuid">uuid potrzebne do danego zapytania</param>
        /// <param name="url">url do potrzebnego zapytania</param>
        /// <returns>Ciąg znaków wynikowych otrzymanych przez serwer lub ujemnej wartości w przypadku napotkanego błędu</returns>

        public async Task<string> makeRequestFunction(string uuid, string url)
        {
            // Utworzenie potrzebnych zmienncyh
            string message;
            string caption = "Error";
            var strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // Utworzenie identyfikatora HttpWebRequest URI (Uniform Resource Identifier) 
            var postData = "uuid=" + Uri.EscapeDataString(uuid); // Wypełenie danych do wysłania w postaci formualarza
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
                    strResponseValue = "-2";
                }
                else
                {
                    strResponseValue = "-1";
                }
                
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
