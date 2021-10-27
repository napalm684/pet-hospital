using System;

namespace BluePaw.Shared
{
    public class Envelope
    {
        public string SendingDepartment { get; set; }
        public string ReceivingDepartment { get; set; }
        public DateTime SentOn { get; set; }
        public PatientRequest Request { get; set; }
    }
}
