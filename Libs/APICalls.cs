using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using ocDownloader.Models;

namespace ocDownloader.Libs
{
    public static class APICalls
    {
        /// <summary>
        /// Check ocDownloader version
        /// </summary>
        /// <param name="URL">URL of the ownCloud environment</param>
        /// <param name="Username">Username</param>
        /// <param name="Password">Decrypted password</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> CheckVersion(String URL, String Username, String Password)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(URL);
                Client.DefaultRequestHeaders.Add("OCS-APIREQUEST", "true");
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(Username + ":" + Password)));

                var HttpContent = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<String, String>("AddonVersion", "1.2.3")
                });

                return await Client.PostAsync(ocDownloader.Libs.Tools.MakeOCUrl("version"), HttpContent);
            }
        }

        public static async Task<HttpResponseMessage> GetQueue(OCConnection ConnectionData)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(ConnectionData.OCUrl);
                Client.DefaultRequestHeaders.Add("OCS-APIREQUEST", "true");
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(ConnectionData.OCUsername + ":" + ConnectionData.OCPassword)));

                return await Client.GetAsync(Tools.MakeOCUrl("queue/get"));
            }
        }
    }
}
