using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.CSDL
{
    public class LopCreateRequest
    {
        public decimal Id { get; set; }
        public string Ma { get; set; }
        public int MaNamHoc { get; set; }
        public string MaSoGD { get; set; }
        public string Ten { get; set; }
        public decimal? IdPhongGD { get; set; }
        public string MaPhongGD { get; set; }
        public decimal? IdTruong { get; set; }
        public string MaTruong { get; set; }
        public string MaKhoi { get; set; }
        public string MaNhomTuoiMN { get; set; }
        public string MaCapHoc { get; set; }
        public string MaSoBuoiHocTrenTuan { get; set; }
        public int? ThuTu { get; set; }
        public int? TrangThai { get; set; }
        public int? IsLopGhep { get; set; }
        public decimal? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public decimal? NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }
    }
}
