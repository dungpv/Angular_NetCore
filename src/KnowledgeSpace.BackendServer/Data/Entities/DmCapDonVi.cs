using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeSpace.BackendServer.Data.Entities
{
    [Table("DmCapDonVi")]
    public class DmCapDonVi 
    {
        [Key]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Ma { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Ten { get; set; }

        public int? ThuTu { get; set; }

    }
}