using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("HocSinh")]
    public class HocSinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "numeric(18,0)")]
        public decimal Id { get; set; }

        [Required]
        public int MaNamHoc { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string MaSoGD { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? IdPhongGD { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string MaPhongGD { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal IdTruong { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string MaTruong { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaCapHoc { get; set; }

        [MaxLength(20)]
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Ma { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string HoTen { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaGioiTinh { get; set; }

        [Column(TypeName = "Date")]
        public DateTime NgaySinh { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaDanToc { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaTrangThai { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaLyDoThoiHoc { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaQuocTich { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaTonGiao { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaSoBuoiHocTrenTuan { get; set; }

        public int? ThuTu { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiSua { get; set; }

        public DateTime? NgaySua { get; set; }

    }
}