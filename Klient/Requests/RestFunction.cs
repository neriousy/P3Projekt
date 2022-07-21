using System;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace Dziennik_Szkolny
{
    class RestFunction
    {
        public async Task<string> makeRequestFunction(string uuid, string url)
        {
            string message;
            string caption = "Error";
            var strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var postData = "uuid=" + Uri.EscapeDataString(uuid);
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
            return strResponseValue;
        }
    }
}
