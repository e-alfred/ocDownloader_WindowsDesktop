using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ocDownloader.Libs
{
    public static class Tools
    {
        public static String MakeOCUrl (String Method)
        {
            return "ocs/v1.php/apps/ocdownloader/api/" + Method + "?format=json";
        }

        public static Int32 GetStatusCode(System.Net.HttpStatusCode StatusCode)
        {
            try
            {
                return (Int32)Enum.Parse(Type.GetType("ocDownloader.Models.HttpStatusCode"), StatusCode.ToString(), true);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static String GetHttpStatusCodeMessage(Int32 StatusCode)
        {
            Dictionary<Int32, String> CodeMessages = new Dictionary<Int32, String>() {
                { 0, "Unknown error" },
                { 200, "OK" },
                { 204, "No content. This message indicates that the request has been successfully processed and that the response is intentionally blank" },
                { 300, "Ambiguous request. This message indicates that the requested information has multiple representations" },
                { 301, "Moved. This message indicates that the requested information has been moved to the URI specified in the Location header" },
                { 302, "Redirect. This message indicates that the requested information is located at the URI specified in the Location header" },
                { 305, "Use proxy. This message indicates that the request should use the proxy server at the URI specified in the Location header" },
                { 400, "Bad request. This message indicates that the request could not be understood by the server" },
                { 401, "Unauthorized. This message indicates that the requested resource requires authentication" },
                { 403, "Forbidden. This message indicates that the server refuses to fulfill the request" },
                { 404, "Not found. This message indicates that the requested resource does not exist on the server" },
                { 407, "Proxy authentication required. This message indicates that the requested proxy requires authentication" },
                { 408, "Request timeout. This message indicates that the client did not send a request within the time the server was expecting the request" },
                { 500, "Internal server error. This message indicates that a generic error has occurred on the server" },
                { 501, "Not implemented. This message indicates that the server does not support the requested function" },
                { 502, "Bad gateway. This message indicates that an intermediate proxy server received a bad response from another proxy or the origin server" },
                { 503, "Service unavailable. This message indicates that the server is temporarily unavailable, usually due to high load or maintenance" },
                { 504, "Gateway timeout. This message indicates that an intermediate proxy server timed out while waiting for a response from another proxy or the origin server" },
                { 505, "Http version not supported. This message indicates that the requested HTTP version is not supported by the server" }
            };

            return CodeMessages[StatusCode];
        }

        public static String GetDownloadTypeImageName(String Protocol)
        {
            switch (Protocol)
            {
                case "HTTP":
                case "HTTPS":
                case "FTP":
                case "FTPS":
                    return "http_ftp.png";
                case "YT Video":
                    return "yt_video.png";
                case "YT Audio":
                    return "yt_audio.png";
                case "BitTorrent":
                    return "bt.png";
                default:
                    return "unknown.png";
            }
        }

        /// <summary>
        /// Encrypt password with the user own key
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        /*public static Byte[] EncryptPassword(Byte[] Password, Byte[] Key)
        {
            Byte[] EncryptedBytes = null;
            Byte[] SaltBytes = new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream MS = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    Rfc2898DeriveBytes GenKey = new Rfc2898DeriveBytes(Key, SaltBytes, 1000);
                    AES.Key = GenKey.GetBytes(AES.KeySize / 8);
                    AES.IV = GenKey.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (CryptoStream CS = new CryptoStream(MS, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        CS.Write(Password, 0, Password.Length);
                        CS.Close();
                    }
                    EncryptedBytes = MS.ToArray();
                }
            }

            return EncryptedBytes;
        }*/

        /// <summary>
        /// Decrypt the password with the user own key
        /// </summary>
        /// <param name="EncryptPassword"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        /*public static Byte[] DecryptPassword(Byte[] EncryptPassword, Byte[] Key)
        {
            Byte[] DecryptedBytes = null;
            Byte[] SaltBytes = new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream MS = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    Rfc2898DeriveBytes GenKey = new Rfc2898DeriveBytes(Key, SaltBytes, 1000);
                    AES.Key = GenKey.GetBytes(AES.KeySize / 8);
                    AES.IV = GenKey.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (CryptoStream CS = new CryptoStream(MS, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        CS.Write(EncryptPassword, 0, EncryptPassword.Length);
                        CS.Close();
                    }
                    DecryptedBytes = MS.ToArray();
                }
            }

            return DecryptedBytes;
        }*/
    }
}
