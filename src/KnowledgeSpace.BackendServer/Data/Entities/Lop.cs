using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("Lop")]
    public class Lop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "numeric(18,0)")]
        public decimal Id { get; set; }

        [MaxLength(20)]
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Ma { get; set; }

        [Required]
        public int MaNamHoc { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string MaSoGD { get; set; }
        public SoGD SoGD { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Ten { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? IdPhongGD { get; set; }
        public PhongGD PhongGD { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string MaPhongGD { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal IdTruong { get; set; }
        public Truong Truong { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string MaTruong { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaCapHoc { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string MaKhoi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaNhomTuoiMN { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string MaSoBuoiHocTrenTuan { get; set; }
        public int? IsLopGhep { get; set; }
        public int TrangThai { get; set; }
        public int? ThuTu { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiSua { get; set; }
        public DateTime NgaySua { get; set; }

    }
}