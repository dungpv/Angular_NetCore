using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.CSDL
{
    public class LopVm
    {
        public decimal Id { get; set; }
        public string Ma { get; set; }
        public int MaNamHoc { get; set; }
        public string MaSoGD { get; set; }
        public string TenSoGD { get; set; }
        public string Ten { get; set; }
        public decimal? IdPhongGD { get; set; }
        public string MaPhongGD { get; set; }
        public string TenPhongGD { get; set; }
        public decimal? IdTruong { get; set; }
        public string MaTruong { get; set; }
        public string TenTruong { get; set; }
        public string MaKhoi { get; set; }
        public string TenKhoi { get; set; }
        public string MaNhomTuoiMN { get; set; }
        public string TenNhomTuoiMN { get; set; }
        public string MaCapHoc { get; set; }
        public string MaSoBuoiHocTrenTuan { get; set; }
        public string TenSoBuoiHocTrenTuan { get; set; }
        public int? ThuTu { get; set; }
        public int? TrangThai { get; set; }
        public int? IsLopGhep { get; set; }

    }
}
