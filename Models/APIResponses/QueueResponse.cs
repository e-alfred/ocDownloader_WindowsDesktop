using System;

namespace ocDownloader.Models.APIResponses
{
    public class Status
    {
        public String Value { get; set; }
        public String Seeding { get; set; }
    }

    public class Progress
    {
        public String Message { get; set; }
        public String ProgressString { get; set; }
        public String NumSeeders { get; set; }
        public String UploadLength { get; set; }
        public String Ratio { get; set; }
    }

    public class Download
    {
        public String GID { get; set; }
        public Int32 PROGRESSVAL { get; set; }
        public Progress PROGRESS { get; set; }
        public Status STATUS { get; set; }
        public Int32 STATUSID { get; set; }
        public String SPEED { get; set; }
        public String FILENAME { get; set; }
        public String PROTO { get; set; }
        public Boolean ISTORRENT { get; set; }
    }

    public class DataQueue
    {
        public Boolean ERROR { get; set; }
        public Download[] QUEUE { get; set; }
        public Counter COUNTER { get; set; }
        public String MESSAGE { get; set; }
    }

    public class OCSQueue
    {
        public META meta { get; set; }
        public DataQueue data { get; set; }
    }

    public class QueueResponse
    {
        public OCSQueue ocs { get; set; }
    }
}
