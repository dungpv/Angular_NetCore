﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("PhongGD")]
    public class PhongGD
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
        public string MaSoGD { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Ten { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaTinh { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? IdHuyen { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaHuyen { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string DiaChi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string DienThoai { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Fax { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Website { get; set; }

        public int? ThuTu { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal? NguoiSua { get; set; }

        public DateTime? NgaySua { get; set; }

        public int? TrangThai { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaVung { get; set; }

    }
}