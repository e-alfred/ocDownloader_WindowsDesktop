using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using ocDownloader.Models;

namespace ocDownloader.Libs
{
    public class UserProfile
    {
        private String AppDataFolder;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public UserProfile ()
        {
            AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ocDownloader");
        }

        /// <summary>
        /// Save connection in a connection file (XML)
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        public void SaveConnection(OCConnection NewConnection)
        {
            XmlSerializer Writer = new XmlSerializer(typeof(OCConnection));
            StreamWriter SaveFile = new StreamWriter(Path.Combine(this.AppDataFolder, (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString() + ".occonnection"));

            Writer.Serialize(SaveFile, NewConnection);
            SaveFile.Close();
        }

        public List<OCConnection> GetAllUserOCConnections()
        {
            XmlSerializer Reader = new XmlSerializer(typeof(OCConnection));
            List<OCConnection> OCConnections = new List<OCConnection>();

            foreach (String Filename in Directory.GetFiles(this.AppDataFolder, "*.occonnection"))
            {
                StreamReader ReadFile = new StreamReader(Filename);

                OCConnection Data = new OCConnection();
                Data = (OCConnection)Reader.Deserialize(ReadFile);
                OCConnections.Add(Data);
            }

            return OCConnections;
        }

        /// <summary>
        /// Prepare user folder
        /// </summary>
        public void PrepareAppDataFolder()
        {
            if (!Directory.Exists(this.AppDataFolder))
            {
                Directory.CreateDirectory(this.AppDataFolder);
            }
        }
    }
}
