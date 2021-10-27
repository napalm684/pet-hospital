using System.ComponentModel;

namespace BluePaw.Shared
{
    public class CreatePatientRequest
    {
        [DisplayName("Pet Name")]
        public string Name { get; set; }
        
        public string Species { get; set; }

        [DisplayName("Owner Name")]
        public string OwnerName { get; set; }

        [DisplayName("Owner Phone")]
        public string OwnerPhone { get; set; }
    }
}