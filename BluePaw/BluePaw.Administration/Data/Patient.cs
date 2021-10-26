using System.ComponentModel.DataAnnotations.Schema;

namespace BluePaw.Administration.Data
{
    [Table("patients")]
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("species")]
        public string Species { get; set; }

        [Column("owner_name")]
        public string OwnerName { get; set; }

        [Column("owner_phone")]
        public string OwnerPhone { get; set; }
    }
}