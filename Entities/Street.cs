using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AspNetCoreDatatable.Entities
{
    [Table("Street")]
    public partial class Street
    {
        [Key]
        public int StreetId { get; set; }
        [StringLength(200)]
        public string StreetName { get; set; }
        [Column("StreetName_MM")]
        [StringLength(200)]
        public string StreetNameMm { get; set; }
        public int TownshipId { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
    }
}
