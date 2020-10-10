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
    public class HocSinhController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HocSinhController> _logger;

        public HocSinhController(ApplicationDbContext context, ILogger<HocSinhController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ApiValidationFilter]
        public async Task<IActionResult> PostHocSinh([FromBody] HocSinhCreateRequest request)
        {
            _logger.LogInformation("Begin PostHocSinh API");

            var dbLop = await _context.HocSinh.FindAsync(request.Id);
            if (dbLop != null)
                return BadRequest(new ApiBadRequestResponse($"HocSinh with id {request.Id} is existed."));

            var HocSinhDetail = new HocSinh() { };

            if (request.MaCapHoc == SysCapHoc.MamNon || request.MaCapHoc == SysCapHoc.C1 || request.MaCapHoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;
                HocSinhDetail.MaPhongGD = request.MaPhongGD;
                HocSinhDetail.IdPhongGD = idPhongGd;
            }

            HocSinhDetail.MaNamHoc = request.MaNamHoc;
            HocSinhDetail.MaSoGD = request.MaSoGD;
            HocSinhDetail.IdTruong = request.IdTruong.Value;
            HocSinhDetail.MaTruong = request.MaTruong;
            HocSinhDetail.MaCapHoc = request.MaCapHoc;
            HocSinhDetail.Ma = request.Ma;
            HocSinhDetail.HoTen = request.HoTen;
            HocSinhDetail.MaGioiTinh = request.MaGioiTinh;
            HocSinhDetail.NgaySinh = request.NgaySinh.Value;
            HocSinhDetail.MaDanToc = request.MaDanToc;
            HocSinhDetail.MaTrangThai = request.MaTrangThai;
            HocSinhDetail.MaLyDoThoiHoc = request.MaLyDoThoiHoc;
            HocSinhDetail.MaQuocTich = request.MaQuocTich;
            HocSinhDetail.MaSoBuoiHocTrenTuan = request.MaSoBuoiHocTrenTuan;
            HocSinhDetail.ThuTu = request.ThuTu;
            HocSinhDetail.NgayTao = DateTime.Now;
            HocSinhDetail.NguoiTao = request.NguoiTao.Value;

            _context.HocSinh.Add(HocSinhDetail);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PostHocSinh API - Success");

                return CreatedAtAction(nameof(GetById), new { id = HocSinhDetail.Id }, request);
            }
            else
            {
                _logger.LogInformation("End PostHocSinh API - Failed");

                return BadRequest(new ApiBadRequestResponse("Create HocSinh is failed"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocSinh(string id, [FromBody] HocSinhCreateRequest request)
        {
            _logger.LogInformation("Begin PutHocSinh API");
            var HocSinh = await _context.HocSinh.FindAsync(id);
            if (HocSinh == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found HocSinh with id {id}"));

            if (request.MaCapHoc == SysCapHoc.MamNon || request.MaCapHoc == SysCapHoc.C1 || request.MaCapHoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                HocSinh.MaPhongGD = HocSinh.MaPhongGD;
                HocSinh.IdPhongGD = idPhongGd;
            }

            HocSinh.MaNamHoc = request.MaNamHoc;
            HocSinh.MaSoGD = request.MaSoGD;
            HocSinh.IdTruong = request.IdTruong.Value;
            HocSinh.MaTruong = request.MaTruong;
            HocSinh.MaCapHoc = request.MaCapHoc;
            HocSinh.Ma = request.Ma;
            HocSinh.HoTen = request.HoTen;
            HocSinh.MaGioiTinh = request.MaGioiTinh;
            HocSinh.NgaySinh = request.NgaySinh.Value;
            HocSinh.MaDanToc = request.MaDanToc;
            HocSinh.MaTrangThai = request.MaTrangThai;
            HocSinh.MaLyDoThoiHoc = request.MaLyDoThoiHoc;
            HocSinh.MaQuocTich = request.MaQuocTich;
            HocSinh.MaSoBuoiHocTrenTuan = request.MaSoBuoiHocTrenTuan;
            HocSinh.NgaySua = DateTime.Now;
            HocSinh.NguoiSua = request.NguoiSua.Value;

            _context.HocSinh.Update(HocSinh);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PutHocSinh API - Success");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("End PutHocSinh API - Failed");
                return BadRequest(new ApiBadRequestResponse("Update HocSinh is failed"));
            }

        }
        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetHocSinhByTruongByNamHoc(decimal? idTruong, int maNamHoc)
        {
            var query = from hs in _context.HocSinh
                        join truong in _context.Truong on hs.IdTruong equals truong.Id
                        select new { hs, truong };
            if (idTruong.HasValue)
            {
                query = query.Where(x => x.truong.Id == idTruong);
            }
            query = query.Where(x => x.hs.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"HocSinh with idTruong: {idTruong} is not found"));
            var LopVm = await query.Select(u => new HocSinhVm()
            {
                Ma = u.hs.Ma,
                HoTen = u.hs.HoTen,
                MaSoGD = u.hs.MaSoGD,
                TenSoGD = (_context.SoGD.Where(x => x.Ma == u.hs.MaSoGD).FirstOrDefault().Ten),
                MaPhongGD = u.truong.MaPhongGD,
                IdPhongGD = u.truong.IdPhongGD.Value,
                TenPhongGD = (_context.PhongGD.Where(x => x.Id == u.truong.IdPhongGD).FirstOrDefault().Ten),
                IdTruong = u.truong.Id,
                MaTruong = u.truong.Ma,
                TenTruong = (_context.Truong.Where(x => x.Ma == u.truong.Ma && x.MaNamHoc == u.truong.MaNamHoc).FirstOrDefault().Ten),
                MaGioiTinh = u.hs.MaGioiTinh,
                TenGioiTinh = u.hs.MaGioiTinh == "01" ? "Nam" : "Nữ",
                MaDanToc = u.hs.MaDanToc,
                TenDanToc = (_context.DmDanToc.Where(x => x.Ma == u.hs.MaDanToc).FirstOrDefault().Ten),
                MaTrangThai = u.hs.MaTrangThai,
                TenTrangThai = (_context.DmTrangThaiHocSinh.Where(x => x.Ma == u.hs.MaTrangThai).FirstOrDefault().Ten),
                MaLyDoThoiHoc = u.hs.MaLyDoThoiHoc,
                TenLyDoThoiHoc = (_context.DmLyDoThoiHoc.Where(x => x.Ma == u.hs.MaLyDoThoiHoc).FirstOrDefault().Ten),
                MaQuocTich = u.hs.MaQuocTich,
                TenQuocTich = (_context.DmNuoc.Where(x => x.Ma == u.hs.MaQuocTich).FirstOrDefault().Ten),
                MaTonGiao = u.hs.MaTonGiao,
                TenTonGiao = (_context.DMTonGiao.Where(x => x.Ma == u.hs.MaTonGiao).FirstOrDefault().Ten),
                MaSoBuoiHocTrenTuan = u.hs.MaSoBuoiHocTrenTuan,
                TenSoBuoiHocTrenTuan = (_context.DmSoBuoiHocTrenTuan.Where(x => x.Ma == u.hs.MaSoBuoiHocTrenTuan).FirstOrDefault().Ten),
                MaCapHoc = u.hs.MaCapHoc,
                NgaySinh = u.hs.NgaySinh,
                ThuTu = u.hs.ThuTu.Value,

            }).ToListAsync();

            return Ok(LopVm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var HocSinh = await _context.HocSinh.FindAsync(id);
            if (HocSinh == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found HocSinh with id {id}"));

            var HocSinhVm = new HocSinhVm()
            {
                Ma = HocSinh.Ma,
                HoTen = HocSinh.HoTen,
                MaSoGD = HocSinh.MaSoGD,
                TenSoGD = (_context.SoGD.Where(x => x.Ma == HocSinh.MaSoGD).FirstOrDefault().Ten),
                MaPhongGD = HocSinh.MaPhongGD,
                IdPhongGD = HocSinh.IdPhongGD.Value,
                TenPhongGD = (_context.PhongGD.Where(x => x.Id == HocSinh.IdPhongGD).FirstOrDefault().Ten),
                IdTruong = HocSinh.IdTruong,
                MaTruong = HocSinh.MaTruong,
                TenTruong = (_context.Truong.Where(x => x.Ma == HocSinh.Ma && x.MaNamHoc == HocSinh.MaNamHoc).FirstOrDefault().Ten),
                MaGioiTinh = HocSinh.MaGioiTinh,
                TenGioiTinh = HocSinh.MaGioiTinh == "01" ? "Nam" : "Nữ",
                MaDanToc = HocSinh.MaDanToc,
                TenDanToc = (_context.DmDanToc.Where(x => x.Ma == HocSinh.MaDanToc).FirstOrDefault().Ten),
                MaTrangThai = HocSinh.MaTrangThai,
                TenTrangThai = (_context.DmTrangThaiHocSinh.Where(x => x.Ma == HocSinh.MaTrangThai).FirstOrDefault().Ten),
                MaLyDoThoiHoc = HocSinh.MaLyDoThoiHoc,
                TenLyDoThoiHoc = (_context.DmLyDoThoiHoc.Where(x => x.Ma == HocSinh.MaLyDoThoiHoc).FirstOrDefault().Ten),
                MaQuocTich = HocSinh.MaQuocTich,
                TenQuocTich = (_context.DmNuoc.Where(x => x.Ma == HocSinh.MaQuocTich).FirstOrDefault().Ten),
                MaTonGiao = HocSinh.MaTonGiao,
                TenTonGiao = (_context.DMTonGiao.Where(x => x.Ma == HocSinh.MaTonGiao).FirstOrDefault().Ten),
                MaSoBuoiHocTrenTuan = HocSinh.MaSoBuoiHocTrenTuan,
                TenSoBuoiHocTrenTuan = (_context.DmSoBuoiHocTrenTuan.Where(x => x.Ma == HocSinh.MaSoBuoiHocTrenTuan).FirstOrDefault().Ten),
                MaCapHoc = HocSinh.MaCapHoc,
                NgaySinh = HocSinh.NgaySinh,
                ThuTu = HocSinh.ThuTu.Value,
            };
            return Ok(HocSinhVm);
        }

    }
}