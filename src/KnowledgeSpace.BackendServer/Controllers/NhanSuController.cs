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
    public class NhanSuController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NhanSuController> _logger;

        public NhanSuController(ApplicationDbContext context, ILogger<NhanSuController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ApiValidationFilter]
        public async Task<IActionResult> PostNhanSu([FromBody] NhanSuCreateRequest request)
        {
            _logger.LogInformation("Begin PostNhanSu API");

            var dbLop = await _context.NhanSu.FindAsync(request.Id);
            if (dbLop != null)
                return BadRequest(new ApiBadRequestResponse($"NhanSu with id {request.Id} is existed."));

            var NhanSuDetail = new NhanSu() { };

            if (request.MaCapHoc == SysCapHoc.MamNon || request.MaCapHoc == SysCapHoc.C1 || request.MaCapHoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                NhanSuDetail.MaPhongGD = request.MaPhongGD;
                NhanSuDetail.IdPhongGD = idPhongGd;
            }

            NhanSuDetail.MaNamHoc = request.MaNamHoc;
            NhanSuDetail.MaSoGD = request.MaSoGD;
            NhanSuDetail.IdTruong = request.IdTruong.Value;
            NhanSuDetail.MaTruong = request.MaTruong;
            NhanSuDetail.MaCapHoc = request.MaCapHoc;
            NhanSuDetail.Ma = request.Ma;
            NhanSuDetail.HoTen = request.HoTen;
            NhanSuDetail.MaGioiTinh = request.MaGioiTinh;
            NhanSuDetail.NgaySinh = request.NgaySinh.Value;
            NhanSuDetail.MaDanToc = request.MaDanToc;
            NhanSuDetail.MaTrangThai = request.MaTrangThai;
            NhanSuDetail.MaQuocTich = request.MaQuocTich;
            NhanSuDetail.MaLoaiCanBo = request.MaLoaiCanBo;
            NhanSuDetail.MaNhomCanBo = request.MaNhomCanBo;
            NhanSuDetail.MaNgach = request.MaNgach;
            NhanSuDetail.MaTrinhDoChuyenMon = request.MaTrinhDoChuyenMon;
            NhanSuDetail.ThuTu = request.ThuTu;
            NhanSuDetail.NgayTao = DateTime.Now;
            NhanSuDetail.NguoiTao = request.NguoiTao.Value;

            _context.NhanSu.Add(NhanSuDetail);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PostNhanSu API - Success");

                return CreatedAtAction(nameof(GetById), new { id = NhanSuDetail.Id }, request);
            }
            else
            {
                _logger.LogInformation("End PostNhanSu API - Failed");

                return BadRequest(new ApiBadRequestResponse("Create NhanSu is failed"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanSu(string id, [FromBody] NhanSuCreateRequest request)
        {
            _logger.LogInformation("Begin PutNhanSu API");
            var NhanSu = await _context.NhanSu.FindAsync(id);
            if (NhanSu == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found NhanSu with id {id}"));

            if (request.MaCapHoc == SysCapHoc.MamNon || request.MaCapHoc == SysCapHoc.C1 || request.MaCapHoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                NhanSu.MaPhongGD = request.MaPhongGD;
                NhanSu.IdPhongGD = idPhongGd;
            }

            NhanSu.MaNamHoc = request.MaNamHoc;
            NhanSu.MaSoGD = request.MaSoGD;
            NhanSu.IdTruong = request.IdTruong.Value;
            NhanSu.MaTruong = request.MaTruong;
            NhanSu.MaCapHoc = request.MaCapHoc;
            NhanSu.Ma = request.Ma;
            NhanSu.HoTen = request.HoTen;
            NhanSu.MaGioiTinh = request.MaGioiTinh;
            NhanSu.NgaySinh = request.NgaySinh.Value;
            NhanSu.MaDanToc = request.MaDanToc;
            NhanSu.MaTrangThai = request.MaTrangThai;
            NhanSu.MaQuocTich = request.MaQuocTich;
            NhanSu.MaLoaiCanBo = request.MaLoaiCanBo;
            NhanSu.MaNhomCanBo = request.MaNhomCanBo;
            NhanSu.MaNgach = request.MaNgach;
            NhanSu.MaTrinhDoChuyenMon = request.MaTrinhDoChuyenMon;
            NhanSu.NgaySua = DateTime.Now;
            NhanSu.NguoiSua = request.NguoiSua.Value;

            _context.NhanSu.Update(NhanSu);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PutNhanSu API - Success");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("End PutNhanSu API - Failed");
                return BadRequest(new ApiBadRequestResponse("Update NhanSu is failed"));
            }

        }
        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetNhanSuByTruongByNamHoc(decimal? idTruong, int maNamHoc)
        {
            var query = from ns in _context.NhanSu
                        join truong in _context.Truong on ns.IdTruong equals truong.Id
                        select new { ns, truong };
            if (idTruong.HasValue)
            {
                query = query.Where(x => x.truong.Id == idTruong);
            }
            query = query.Where(x => x.ns.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"NhanSu with idTruong: {idTruong} is not found"));
            var LopVm = await query.Select(u => new NhanSuVm()
            {
                Ma =u.ns.Ma,
                HoTen =u.ns.HoTen,
                MaSoGD =u.ns.MaSoGD,
                TenSoGD = (_context.SoGD.Where(x => x.Ma ==u.ns.MaSoGD).FirstOrDefault().Ten),
                MaPhongGD = u.truong.MaPhongGD,
                IdPhongGD = u.truong.IdPhongGD.Value,
                TenPhongGD = (_context.PhongGD.Where(x => x.Id == u.truong.IdPhongGD).FirstOrDefault().Ten),
                IdTruong = u.truong.Id,
                MaTruong = u.truong.Ma,
                TenTruong = (_context.Truong.Where(x => x.Ma == u.truong.Ma && x.MaNamHoc == u.truong.MaNamHoc).FirstOrDefault().Ten),
                MaGioiTinh =u.ns.MaGioiTinh,
                TenGioiTinh =u.ns.MaGioiTinh,
                MaDanToc =u.ns.MaDanToc,
                TenDanToc = (_context.DmDanToc.Where(x => x.Ma ==u.ns.MaDanToc).FirstOrDefault().Ten),
                MaTrangThai =u.ns.MaTrangThai,
                TenTrangThai = (_context.DmTrangThaiCanBo.Where(x => x.Ma ==u.ns.MaTrangThai).FirstOrDefault().Ten),
                MaQuocTich =u.ns.MaQuocTich,
                TenQuocTich = (_context.DmNuoc.Where(x => x.Ma ==u.ns.MaQuocTich).FirstOrDefault().Ten),
                MaTonGiao =u.ns.MaTonGiao,
                TenTonGiao = (_context.DMTonGiao.Where(x => x.Ma ==u.ns.MaTonGiao).FirstOrDefault().Ten),
                MaNhomCanBo =u.ns.MaNhomCanBo,
                TenNhomCanBo = (_context.DmNhomCanBo.Where(x => x.Ma ==u.ns.MaNhomCanBo).FirstOrDefault().Ten),
                MaNgach = u.ns.MaNgach,
                TenNgach = (_context.DmNgach.Where(x => x.Ma == u.ns.MaNgach).FirstOrDefault().Ten),
                MaLoaiCanBo = u.ns.MaLoaiCanBo,
                TenLoaiCanBo = (_context.DmLoaiCanBo.Where(x => x.Ma == u.ns.MaLoaiCanBo).FirstOrDefault().Ten),
                MaTrinhDoChuyenMon= u.ns.MaTrinhDoChuyenMon,
                TenTrinhDoChuyenMon = (_context.DmTrinhDoChuyenMonNghiepVu.Where(x => x.Ma == u.ns.MaTrinhDoChuyenMon).FirstOrDefault().Ten),
                MaCapHoc =u.ns.MaCapHoc,
                NgaySinh =u.ns.NgaySinh,
                ThuTu =u.ns.ThuTu.Value,

            }).ToListAsync();

            return Ok(LopVm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var NhanSu = await _context.NhanSu.FindAsync(id);
            if (NhanSu == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found NhanSu with id {id}"));

            var NhanSuVm = new NhanSuVm()
            {
                Ma = NhanSu.Ma,
                HoTen = NhanSu.HoTen,
                MaSoGD = NhanSu.MaSoGD,
                TenSoGD = (_context.SoGD.Where(x => x.Ma == NhanSu.MaSoGD).FirstOrDefault().Ten),
                MaPhongGD = NhanSu.MaPhongGD,
                IdPhongGD = NhanSu.IdPhongGD.Value,
                TenPhongGD = (_context.PhongGD.Where(x => x.Id == NhanSu.IdPhongGD).FirstOrDefault().Ten),
                IdTruong = NhanSu.Id,
                MaTruong = NhanSu.Ma,
                TenTruong = (_context.Truong.Where(x => x.Ma == NhanSu.Ma && x.MaNamHoc == NhanSu.MaNamHoc).FirstOrDefault().Ten),
                MaGioiTinh = NhanSu.MaGioiTinh,
                TenGioiTinh = NhanSu.MaGioiTinh,
                MaDanToc = NhanSu.MaDanToc,
                TenDanToc = (_context.DmDanToc.Where(x => x.Ma == NhanSu.MaDanToc).FirstOrDefault().Ten),
                MaTrangThai = NhanSu.MaTrangThai,
                TenTrangThai = (_context.DmTrangThaiCanBo.Where(x => x.Ma == NhanSu.MaTrangThai).FirstOrDefault().Ten),

                MaQuocTich = NhanSu.MaQuocTich,
                TenQuocTich = (_context.DmNuoc.Where(x => x.Ma == NhanSu.MaQuocTich).FirstOrDefault().Ten),
                MaTonGiao = NhanSu.MaTonGiao,
                TenTonGiao = (_context.DMTonGiao.Where(x => x.Ma == NhanSu.MaTonGiao).FirstOrDefault().Ten),
                MaNhomCanBo = NhanSu.MaNhomCanBo,
                TenNhomCanBo = (_context.DmNhomCanBo.Where(x => x.Ma ==NhanSu.MaNhomCanBo).FirstOrDefault().Ten),
                MaNgach =NhanSu.MaNgach,
                TenNgach = (_context.DmNgach.Where(x => x.Ma ==NhanSu.MaNgach).FirstOrDefault().Ten),
                MaLoaiCanBo =NhanSu.MaLoaiCanBo,
                TenLoaiCanBo = (_context.DmLoaiCanBo.Where(x => x.Ma ==NhanSu.MaLoaiCanBo).FirstOrDefault().Ten),
                MaTrinhDoChuyenMon =NhanSu.MaTrinhDoChuyenMon,
                TenTrinhDoChuyenMon = (_context.DmTrinhDoChuyenMonNghiepVu.Where(x => x.Ma ==NhanSu.MaTrinhDoChuyenMon).FirstOrDefault().Ten),
                MaCapHoc = NhanSu.MaCapHoc,
                NgaySinh = NhanSu.NgaySinh,
                ThuTu = NhanSu.ThuTu.Value,
            };
            return Ok(NhanSuVm);
        }

    }
}