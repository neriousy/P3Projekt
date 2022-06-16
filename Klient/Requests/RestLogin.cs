using System;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Dziennik_Szkolny
{
    class RestLogin
    {

        public string makeRequest(string login, string haslo)
        {
            string message;
            string caption = "Error";
            string strResponseValue = string.Empty;
            string url = "https://localhost:44307/api/Students/PostGetStudent";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var postData = "email=" + Uri.EscapeDataString(login);
            postData += "&passwd=" + Uri.EscapeDataString(haslo);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            HttpWebResponse response = null;

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    message = "Niepoprawne dane logowania";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    strResponseValue = "-1";
                }
            }
            catch (Exception ex)
            {
                message = "error: " + ex.Message.ToString();
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                strResponseValue = "-1";
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
