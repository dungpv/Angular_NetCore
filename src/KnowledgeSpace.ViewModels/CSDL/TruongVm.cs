using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.CSDL
{
    public class TruongVm
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
        public string MaTinh { get; set; }
        public string TenTinh { get; set; }
        public string MaHuyen { get; set; }
        public string TenHuyen { get; set; }
        public string MaLoaiHinh { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MaLoaiTruong { get; set; }
        public string TenLoaiTruong { get; set; }
        public string MaNhomCapHoc { get; set; }
        public string TenNhomCapHoc { get; set; }
        public string DsCapHoc { get; set; }
        public string MaCapHoc { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public int? ThuTu { get; set; }
        public int? IsCapMN { get; set; }
        public int? IsCapTH { get; set; }
        public int? IsCapTHCS { get; set; }
        public int? IsCapTHPT { get; set; }
        public int? IsCapGDTX { get; set; }

    }
}
