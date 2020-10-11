using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.CSDL
{
    public class PhongGDCreateRequest
    {
        public decimal Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MaSoGD { get; set; }
        public string MaHuyen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public int? ThuTu { get; set; }
        public int MaNamHoc { get; set; }
        public string MaVung { get; set; }
    }
}
