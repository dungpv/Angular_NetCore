using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("DmTinh")]
    public class DmTinh
    {
        [Key]
        [MaxLength(20)]
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Ma { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Ten { get; set; }

        public int ThuTu { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiSua { get; set; }
        public DateTime NgaySua { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Cap { get; set; }

        public ICollection<DmHuyen> DmHuyen { get; set; }
    }
}