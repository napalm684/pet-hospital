using System.ComponentModel.DataAnnotations.Schema;

namespace BluePaw.Administration.Data
{
    [Table("patients")]
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Species { get; set; }

        public string OwnerName { get; set; }

        public string OwnerPhone { get; set; }
    }
}