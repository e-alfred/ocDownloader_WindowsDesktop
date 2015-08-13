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
