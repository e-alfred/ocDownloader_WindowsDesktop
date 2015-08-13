using System;

namespace ocDownloader.Models.APIResponses
{
    public class Download
    {
        public String GID { get; set; }
        public Double PROGRESSVAL { get; set; }
        public Progress PROGRESS { get; set; }
        public Status STATUS { get; set; }
        public Int32 STATUSID { get; set; }
        public String SPEED { get; set; }
        public String FILENAME { get; set; }
        public String PROTO { get; set; }
        public Boolean ISTORRENT { get; set; }
    }
}
