using System;

namespace ocDownloader.Models.APIResponses
{
    public class META
    {
        public String status { get; set; }
        public Int32 statuscode { get; set; }
        public String message { get; set; }
    }

    public class Counter
    {
        public Int32 ALL { get; set; }
        public Int32 COMPLETES { get; set; }
        public Int32 ACTIVES { get; set; }
        public Int32 WAITINGS { get; set; }
        public Int32 STOPPED { get; set; }
        public Int32 REMOVED { get; set; }
    }
}
