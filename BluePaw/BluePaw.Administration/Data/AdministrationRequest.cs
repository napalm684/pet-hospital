﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BluePaw.Administration.Data
{
    [Table("administration_requests")]
    public class AdministrationRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        [Column("request")]
        public string Request { get; set; }

        [Column("completed")]
        public bool Completed { get; set; }
    }
}
