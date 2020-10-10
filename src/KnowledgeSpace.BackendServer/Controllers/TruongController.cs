using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Helpers;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class TruongController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TruongController> _logger;

        public TruongController(ApplicationDbContext context, ILogger<TruongController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ApiValidationFilter]
        public async Task<IActionResult> PostTruong([FromBody] TruongCreateRequest request)
        {
            _logger.LogInformation("Begin PostTruong API");

            var dbTruong = await _context.Truong.FindAsync(request.Id);
            if (dbTruong != null)
                return BadRequest(new ApiBadRequestResponse($"Truong with id {request.Id} is existed."));

            decimal? idHuyen = null;
            if(string.IsNullOrEmpty(request.MaHuyen))
                 idHuyen = _context.DmHuyen.Where(h => h.Ma == request.MaHuyen && h.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;

            var TruongDetail = new Truong() { };

            //check cấp dựa vào mã nhóm cấp
            string ds_cap = _context.DmNhomCapHoc.Where(x => x.Ma == request.MaNhomCapHoc).FirstOrDefault().DSCap;
            string cap_hoc = _context.DmNhomCapHoc.Where(x => x.Ma == request.MaNhomCapHoc).FirstOrDefault().Cap;
            TruongDetail.DSCapHoc = ds_cap;
            if (ds_cap.Contains(SysCapHoc.MamNon))
                TruongDetail.IsCapMN = 1;
            else TruongDetail.IsCapMN = null;
            if (ds_cap.Contains(SysCapHoc.C1))
                TruongDetail.IsCapTH = 1;
            else TruongDetail.IsCapTH = null;
            if (ds_cap.Contains(SysCapHoc.C2))
                TruongDetail.IsCapTHCS = 1;
            else TruongDetail.IsCapTHCS = null;
            if (ds_cap.Contains(SysCapHoc.C3))
                TruongDetail.IsCapTHPT = 1;
            else TruongDetail.IsCapTHPT = null;
            if (ds_cap.Contains(SysCapHoc.GDTX))
                TruongDetail.IsCapGDTX = 1;
            else TruongDetail.IsCapGDTX = null;

            if (cap_hoc == SysCapHoc.MamNon || cap_hoc == SysCapHoc.C1 || cap_hoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                TruongDetail.MaPhongGD = request.MaPhongGD;
                TruongDetail.IdPhongGD = idPhongGd;
            }

            TruongDetail.Ma = request.Ma;
            TruongDetail.MaNamHoc = request.MaNamHoc;
            TruongDetail.MaSoGD = request.MaSoGD;
            TruongDetail.Ten = request.Ten;
            TruongDetail.MaTinh = request.MaTinh;
            TruongDetail.MaHuyen = request.MaHuyen;
            TruongDetail.IdHuyen = idHuyen.Value;
            TruongDetail.MaLoaiHinh = request.MaLoaiHinh;
            TruongDetail.MaLoaiTruong = request.MaLoaiTruong;
            TruongDetail.MaNhomCapHoc = request.MaNhomCapHoc;
            TruongDetail.DSCapHoc = request.DsCapHoc;
            TruongDetail.DiaChi = request.DiaChi;
            TruongDetail.DienThoai = request.DienThoai;
            TruongDetail.Email = request.Email;
            TruongDetail.Fax = request.Fax;
            TruongDetail.Website = request.Website;
            TruongDetail.ThuTu = request.ThuTu;
            TruongDetail.TrangThai = request.TrangThai;
            TruongDetail.MaVung = request.MaVung;
            TruongDetail.NgayTao = DateTime.Now;
            TruongDetail.NguoiTao = request.NguoiTao.Value;

            _context.Truong.Add(TruongDetail);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PostTruong API - Success");

                return CreatedAtAction(nameof(GetById), new { id = TruongDetail.Id }, request);
            }
            else
            {
                _logger.LogInformation("End PostTruong API - Failed");

                return BadRequest(new ApiBadRequestResponse("Create Truong is failed"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTruong(string id, [FromBody] TruongCreateRequest request)
        {
            _logger.LogInformation("Begin PutTruong API");
            var Truong = await _context.Truong.FindAsync(id);
            if (Truong == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found Truong with id {id}"));

            decimal? idHuyen = null;
            if (string.IsNullOrEmpty(request.MaHuyen))
                idHuyen = _context.DmHuyen.Where(h => h.Ma == request.MaHuyen && h.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;

            //check cấp dựa vào mã nhóm cấp
            string ds_cap = _context.DmNhomCapHoc.Where(x => x.Ma == request.MaNhomCapHoc).FirstOrDefault().DSCap;
            string cap_hoc = _context.DmNhomCapHoc.Where(x => x.Ma == request.MaNhomCapHoc).FirstOrDefault().Cap;
            Truong.DSCapHoc = ds_cap;
            if (ds_cap.Contains(SysCapHoc.MamNon))
                Truong.IsCapMN = 1;
            else Truong.IsCapMN = null;
            if (ds_cap.Contains(SysCapHoc.C1))
                Truong.IsCapTH = 1;
            else Truong.IsCapTH = null;
            if (ds_cap.Contains(SysCapHoc.C2))
                Truong.IsCapTHCS = 1;
            else Truong.IsCapTHCS = null;
            if (ds_cap.Contains(SysCapHoc.C3))
                Truong.IsCapTHPT = 1;
            else Truong.IsCapTHPT = null;
            if (ds_cap.Contains(SysCapHoc.GDTX))
                Truong.IsCapGDTX = 1;
            else Truong.IsCapGDTX = null;

            if (cap_hoc == SysCapHoc.MamNon || cap_hoc == SysCapHoc.C1 || cap_hoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                Truong.MaPhongGD = Truong.MaPhongGD;
                Truong.IdPhongGD = idPhongGd;
            }

            Truong.Ma = request.Ma;
            Truong.MaNamHoc = request.MaNamHoc;
            Truong.MaSoGD = request.MaSoGD;
            Truong.Ten = request.Ten;
            Truong.MaTinh = request.MaTinh;
            Truong.MaHuyen = request.MaHuyen;
            Truong.IdHuyen = idHuyen.Value;
            Truong.MaLoaiHinh = request.MaLoaiHinh;
            Truong.MaLoaiTruong = request.MaLoaiTruong;
            Truong.MaNhomCapHoc = request.MaNhomCapHoc;
            Truong.DSCapHoc = request.DsCapHoc;
            Truong.DiaChi = request.DiaChi;
            Truong.DienThoai = request.DienThoai;
            Truong.Email = request.Email;
            Truong.Fax = request.Fax;
            Truong.Website = request.Website;
            Truong.ThuTu = request.ThuTu;
            Truong.TrangThai = request.TrangThai;
            Truong.MaVung = request.MaVung;
            Truong.NgaySua = DateTime.Now;
            Truong.NguoiSua = request.NguoiTao.Value;

            _context.Truong.Update(Truong);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PutTruong API - Success");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("End PutTruong API - Failed");
                return BadRequest(new ApiBadRequestResponse("Update Truong is failed"));
            }

        }
        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetTruongBySoGDByNamHoc(string maSoGD, int maNamHoc)
        {
            var query = from p in _context.Truong
                        join s in _context.SoGD on p.MaSoGD equals s.Ma
                        join h in _context.DmHuyen on new { A= p.MaHuyen,B= p.MaNamHoc } equals new {A= h.Ma, B=h.MaNamHoc }
                        join v in _context.DmVung on new { A = p.MaVung } equals new { A = v.Ma }
                        select new { p, s, h, v };
            if (!string.IsNullOrEmpty(maSoGD))
            {
                query = query.Where(x => x.p.MaSoGD == maSoGD);
            }
            query = query.Where(x => x.p.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"Truong with maSoGD: {maSoGD} is not found"));
            var TruongVm = await query.Select(u => new TruongVm()
            {
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                MaSoGD = u.s.Ma,
                TenSoGD = u.s.Ten,
                MaPhongGD = u.p.MaPhongGD,
                IdPhongGD = u.p.IdPhongGD.Value,
                TenPhongGD = (_context.PhongGD.Where(x => x.Id == u.p.IdPhongGD).FirstOrDefault().Ten),
                MaTinh = u.p.MaTinh,
                TenTinh = (_context.DmTinh.Where(x => x.Ma == u.p.MaTinh).FirstOrDefault().Ten),
                MaLoaiHinh = u.p.MaLoaiHinh,
                TenLoaiHinh = (_context.DmLoaiHinh.Where(x => x.Ma == u.p.MaLoaiHinh && x.MaNamHoc == u.p.MaNamHoc).FirstOrDefault().Ten),
                MaLoaiTruong = u.p.MaLoaiTruong,
                TenLoaiTruong = (_context.DmLoaiTruong.Where(x => x.Ma == u.p.MaLoaiTruong).FirstOrDefault().Ten),
                MaNhomCapHoc = u.p.MaNhomCapHoc,
                TenNhomCapHoc = (_context.DmNhomCapHoc.Where(x => x.Ma == u.p.MaNhomCapHoc).FirstOrDefault().Ten),
                MaHuyen = u.h.Ma,
                TenHuyen = u.h.Ten,
                DsCapHoc = u.p.DSCapHoc,
                DiaChi = u.p.DiaChi,
                DienThoai = u.p.DienThoai,
                Email = u.p.Email,
                Fax = u.p.Fax,
                Website = u.p.Website,
                TenVung = u.v.Ten,
                ThuTu = u.p.ThuTu.Value,

            }).ToListAsync();

            return Ok(TruongVm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var Truong = await _context.Truong.FindAsync(id);
            if (Truong == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found Truong with id {id}"));

            var TruongVm = new TruongVm()
            {
                Ma = Truong.Ma,
                Ten = Truong.Ten,
                MaSoGD = Truong.MaSoGD,
                TenSoGD = (_context.SoGD.Where(x => x.Ma == Truong.MaPhongGD).FirstOrDefault().Ten),
                MaPhongGD = Truong.MaPhongGD,
                IdPhongGD = Truong.IdPhongGD.Value,
                TenPhongGD = (_context.PhongGD.Where(x => x.Id == Truong.IdPhongGD).FirstOrDefault().Ten),
                MaTinh = Truong.MaTinh,
                TenTinh = (_context.DmTinh.Where(x => x.Ma == Truong.MaTinh).FirstOrDefault().Ten),
                MaLoaiHinh = Truong.MaLoaiHinh,
                TenLoaiHinh = (_context.DmLoaiHinh.Where(x => x.Ma == Truong.MaLoaiHinh && x.MaNamHoc == Truong.MaNamHoc).FirstOrDefault().Ten),
                MaLoaiTruong = Truong.MaLoaiTruong,
                TenLoaiTruong = (_context.DmLoaiTruong.Where(x => x.Ma == Truong.MaLoaiTruong).FirstOrDefault().Ten),
                MaNhomCapHoc = Truong.MaNhomCapHoc,
                TenNhomCapHoc = (_context.DmNhomCapHoc.Where(x => x.Ma == Truong.MaNhomCapHoc).FirstOrDefault().Ten),
                MaHuyen = Truong.MaHuyen,
                TenHuyen = (_context.DmHuyen.Where(x => x.Ma == Truong.MaHuyen && x.MaNamHoc == Truong.MaNamHoc).FirstOrDefault().Ten),
                DsCapHoc = Truong.DSCapHoc,
                DiaChi = Truong.DiaChi,
                DienThoai = Truong.DienThoai,
                Email = Truong.Email,
                Fax = Truong.Fax,
                Website = Truong.Website,
                TenVung = (_context.DmVung.Where(x => x.Ma == Truong.MaVung).FirstOrDefault().Ten),
                ThuTu = Truong.ThuTu.Value,
            };
            return Ok(TruongVm);
        }

    }
}