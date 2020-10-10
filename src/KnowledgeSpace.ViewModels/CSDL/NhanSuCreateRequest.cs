using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.CSDL
{
    public class NhanSuCreateRequest
    {
        public decimal Id { get; set; }
        public int MaNamHoc { get; set; }
        public string MaSoGD { get; set; }
        public decimal? IdPhongGD { get; set; }
        public string MaPhongGD { get; set; }
        public decimal? IdTruong { get; set; }
        public string MaTruong { get; set; }
        public string MaCapHoc { get; set; }
        public string Ma { get; set; }
        public string HoTen { get; set; }
        public string MaGioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string MaDanToc { get; set; }
        public string MaTrangThai { get; set; }
        public string MaQuocTich { get; set; }
        public string MaTonGiao { get; set; }
        public string MaNhomCanBo { get; set; }
        public string MaLoaiCanBo { get; set; }
        public string MaNgach { get; set; }
        public string MaTrinhDoChuyenMon { get; set; }
        public int ThuTu { get; set; }
        public decimal? NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal? NguoiSua { get; set; }
        public DateTime NgaySua { get; set; }
    }
}
