using System;

namespace ocDownloader.Models.APIResponses
{
    public class Version
    {
        public Boolean RESULT { get; set; }
    }

    public class OCSVersion
    {
        public META meta { get; set; }
        public Version data { get; set; }
    }

    public class VersionResponse
    {
        public OCSVersion ocs { get; set; }
    }
}