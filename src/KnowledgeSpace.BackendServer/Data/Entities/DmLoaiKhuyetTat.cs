using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("DmLoaiKhuyetTat")]
    public class DmLoaiKhuyetTat
    {
        [Key]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string MA { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string TEN { get; set; }

        public int THU_TU { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal NGUOI_TAO { get; set; }

        public DateTime NGAY_TAO { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal NGUOI_SUA { get; set; }

        public DateTime NGAY_SUA { get; set; }
    }
}