﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("DmDanToc")]
    public class DmDanToc
    {
        [Key]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Ma { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Ten { get; set; }

        [MaxLength(250)]
        [Column(TypeName = "nvarchar(250)")]
        public string TenGoiKhac { get; set; }

        public int? ThuTu { get; set; }
        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }
    }
}