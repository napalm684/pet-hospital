using System.ComponentModel.DataAnnotations.Schema;

namespace BluePaw.Shared
{
    public class CreatePatientRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Species { get; set; }

        public string OwnerName { get; set; }

        public string OwnerPhone { get; set; }
    }
}