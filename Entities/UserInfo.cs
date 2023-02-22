using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AspNetCoreDatatable.Entities
{
    [Table("UserInfo")]
    public partial class UserInfo
    {
        [Key]
        [StringLength(256)]
        public string UserId { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string Balance { get; set; }
        [StringLength(50)]
        public string Credit { get; set; }
        [StringLength(50)]
        public string Wallet { get; set; }
        [StringLength(100)]
        public string Picture { get; set; }
        public int? Age { get; set; }
        [StringLength(100)]
        public string EyeColor { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [StringLength(50)]
        public string Company { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Registered { get; set; }
    }
}
