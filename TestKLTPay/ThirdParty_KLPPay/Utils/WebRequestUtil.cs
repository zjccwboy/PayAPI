using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ThirdParty_KLPPay.Utils
{
    public class WebRequestUtil
    {
        public static string PostJson(string url, string postString)
        {
            try
            {
                if (url.StartsWith("https"))
                    ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidationCallback;

                var postData = Encoding.UTF8.GetBytes(postString);
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add("Content-Type", "application/json");
                var responseData = webClient.UploadData(url, "POST", postData);
                string result = Encoding.UTF8.GetString(responseData);
                return result;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static async Task<string> PostJsonAsync(string url, string postString)
        {
            try
            {
                if (url.StartsWith("https"))
                    ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidationCallback;

                var postData = Encoding.UTF8.GetBytes(postString);
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add("Content-Type", "application/json");
                var responseData = await webClient.UploadDataTaskAsync(url, "POST", postData);
                string result = Encoding.UTF8.GetString(responseData);
                return result;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
    }
}
