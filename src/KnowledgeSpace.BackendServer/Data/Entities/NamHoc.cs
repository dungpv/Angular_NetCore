using KnowledgeSpace.BackendServer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("NamHoc")]
    public class NamHoc
    {
        [Key]
        [MaxLength(20)]
        public int Ma { get; set; }

        [MaxLength(250)]
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Ten { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiSua { get; set; }
        public DateTime NgaySua { get; set; }

    }
}