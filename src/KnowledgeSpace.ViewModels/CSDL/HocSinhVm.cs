using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.CSDL
{
    public class HocSinhVm
    {
        public decimal Id { get; set; }
        public int MaNamHoc { get; set; }
        public string MaSoGD { get; set; }
        public string TenSoGD { get; set; }
        public decimal IdPhongGD { get; set; }
        public string MaPhongGD { get; set; }
        public string TenPhongGD { get; set; }
        public decimal? IdTruong { get; set; }
        public string MaTruong { get; set; }
        public string TenTruong { get; set; }
        public string MaCapHoc { get; set; }
        public string Ma { get; set; }
        public string HoTen { get; set; }

        public string MaGioiTinh { get; set; }
        public string TenGioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string MaDanToc { get; set; }
        public string TenDanToc { get; set; }
        public string MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public string MaLyDoThoiHoc { get; set; }
        public string TenLyDoThoiHoc { get; set; }
        public string MaQuocTich { get; set; }
        public string TenQuocTich { get; set; }
        public string MaTonGiao { get; set; }
        public string TenTonGiao { get; set; }
        public string MaSoBuoiHocTrenTuan { get; set; }
        public string TenSoBuoiHocTrenTuan { get; set; }
        public int ThuTu { get; set; }
        public int TrangThai { get; set; }

    }
}
