﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("DmMonDayGV")]
    public class DmMonDayGV
    {
        [Key]
        [Column(TypeName = "nvarchar(20)")]
        public string Ma { get; set; }

        [MaxLength(20)]
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string MaMonHoc { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Ten { get; set; }

        public int? ThuTu { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiSua { get; set; }

        public DateTime? NgaySua { get; set; }

        public int? IsMN { get; set; }
        public int? IsC1 { get; set; }
        public int? IsC2 { get; set; }
        public int? IsC3 { get; set; }
        public int? IsGdtx { get; set; }
    }
}