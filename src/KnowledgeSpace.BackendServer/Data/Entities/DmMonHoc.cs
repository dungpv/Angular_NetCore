using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("DmMonHoc")]
    public class DmMonHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Ma { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Ten { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaCapHoc { get; set; }
        public DmCapHoc DmCapHoc { get; set; }

        public int MaNamHoc { get; set; }
        public NamHoc NamHoc { get; set; }

        public int KieuMonHoc { get; set; }
        public int IsMonTC { get; set; }
        public int ThuTu { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        [Column(TypeName = "numeric(18,0)")]
        public decimal NguoiSua { get; set; }
        public DateTime NgaySua { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string MaMon { get; set; }

    }
}