using System;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace Dziennik_Szkolny
{
    class RestLoginTeacher
    {
        /// <summary>
        /// Sprawdzenienie danych logowania
        /// </summary>
        /// <param name="login">email</param>
        /// <param name="haslo">password</param>
        /// <returns>W przypadku powodzenia : Struktura danych użytkownika
        ///          W przypadku niepowodzenia : -1</returns>
        public async Task<string> makeRequestLogin(string login, string haslo)
        {
            string message;
            string caption = "Error";
            var strResponseValue = string.Empty;
            string url = "https://localhost:44307/api/Teachers/PostGetTeacher";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var postData = "email=" + Uri.EscapeDataString(login);
            postData += "&password=" + Uri.EscapeDataString(haslo);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            HttpWebResponse response = null;

            try
            {
                using (var stream = await request.GetRequestStreamAsync())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)(await request.GetResponseAsync());

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = "error: " + ex.Message.ToString();
                if (message != "error: The remote server returned an error: (404) Not Found.")
                {
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;
        }
    }
}
