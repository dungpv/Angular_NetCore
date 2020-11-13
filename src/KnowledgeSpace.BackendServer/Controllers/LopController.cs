using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Authorization;
using KnowledgeSpace.BackendServer.Constants;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Helpers;
using KnowledgeSpace.ViewModels;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeSpace.BackendServer.Controllers.Main
{
    public class LopController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LopController> _logger;

        public LopController(ApplicationDbContext context, ILogger<LopController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ApiValidationFilter]
        public async Task<IActionResult> PostLop([FromBody] LopCreateRequest request)
        {
            _logger.LogInformation("Begin PostLop API");

            var dbLop = await _context.Lop.FindAsync(request.Id);
            if (dbLop != null)
                return BadRequest(new ApiBadRequestResponse($"Lop with id {request.Id} is existed."));

            var LopDetail = new Lop() { };
            request.MaNamHoc = 2019;
            if (request.MaCapHoc == SysCapHoc.MamNon || request.MaCapHoc == SysCapHoc.C1 || request.MaCapHoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (!string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; 

                LopDetail.MaPhongGD = request.MaPhongGD;
                LopDetail.IdPhongGD = idPhongGd;
            }
            else
            {
                LopDetail.MaPhongGD = null;
                LopDetail.IdPhongGD = null;
            }
            decimal? idTruong = null;
            if (!string.IsNullOrEmpty(request.MaTruong))
            {
                idTruong = _context.Truong.Where(t => t.Ma == request.MaTruong && t.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;
                LopDetail.IdTruong = idTruong.Value;
            }    


            LopDetail.Ma = request.Ten;
            LopDetail.MaNamHoc = request.MaNamHoc;
            LopDetail.MaSoGD = request.MaSoGD;
            LopDetail.Ten = request.Ten;

            LopDetail.MaTruong = request.MaTruong;
            LopDetail.MaKhoi = request.MaKhoi;
            LopDetail.MaNhomTuoiMN = request.MaNhomTuoiMN;
            LopDetail.MaCapHoc = request.MaCapHoc;
            LopDetail.MaSoBuoiHocTrenTuan = request.MaSoBuoiHocTrenTuan;
            LopDetail.IsLopGhep = request.IsLopGhep;
            LopDetail.ThuTu = request.ThuTu;
            LopDetail.TrangThai = request.TrangThai;
            LopDetail.NgayTao = DateTime.Now;

            _context.Lop.Add(LopDetail);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PostLop API - Success");

                return CreatedAtAction(nameof(GetById), new { id = LopDetail.Id }, request);
            }
            else
            {
                _logger.LogInformation("End PostLop API - Failed");

                return BadRequest(new ApiBadRequestResponse("Create Lop is failed"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLop(decimal id, [FromBody] LopCreateRequest request)
        {
            _logger.LogInformation("Begin PutLop API");
            var Lop = await _context.Lop.FindAsync(id);
            if (Lop == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found Lop with id {id}"));
            request.MaNamHoc = 2019;
            if (request.MaCapHoc == SysCapHoc.MamNon || request.MaCapHoc == SysCapHoc.C1 || request.MaCapHoc == SysCapHoc.C2)
            {
                decimal? idPhongGd = null;
                if (!string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                Lop.MaPhongGD = request.MaPhongGD;
                Lop.IdPhongGD = idPhongGd;
            }
            else
            {
                Lop.MaPhongGD = null;
                Lop.IdPhongGD = null;
            }
            decimal? idTruong = null;
            if (!string.IsNullOrEmpty(request.MaTruong))
            {
                idTruong = _context.Truong.Where(t => t.Ma == request.MaTruong && t.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;
                Lop.IdTruong = idTruong.Value;
            }
            Lop.Ma = request.Ten;
            Lop.MaNamHoc = request.MaNamHoc;
            Lop.MaSoGD = request.MaSoGD;
            Lop.Ten = request.Ten;

            Lop.MaTruong = request.MaTruong;
            Lop.MaKhoi = request.MaKhoi;
            Lop.MaNhomTuoiMN = request.MaNhomTuoiMN;
            Lop.MaCapHoc = request.MaCapHoc;
            Lop.MaSoBuoiHocTrenTuan = request.MaSoBuoiHocTrenTuan;
            Lop.IsLopGhep = request.IsLopGhep;
            Lop.ThuTu = request.ThuTu;
            Lop.TrangThai = request.TrangThai;
            Lop.NgaySua = DateTime.Now;

            _context.Lop.Update(Lop);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PutLop API - Success");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("End PutLop API - Failed");
                return BadRequest(new ApiBadRequestResponse("Update Lop is failed"));
            }

        }
        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLopByTruongByNamHoc(decimal? idTruong, int maNamHoc)
        {
            var query = from Lop in _context.Lop
                        join truong in _context.Truong on Lop.IdTruong equals truong.Id
                        select new { Lop, truong };
            if (idTruong.HasValue)
            {
                query = query.Where(x => x.truong.Id == idTruong);
            }
            query = query.Where(x => x.Lop.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"Lop with idTruong: {idTruong} is not found"));
            var LopVm = await query.Select(u => new LopVm()
            {
                Ma = u.Lop.Ma,
                Ten = u.Lop.Ten,
                MaSoGD = u.Lop.MaSoGD,
                TenSoGD = (_context.SoGD.Where(x => x.Ma == u.Lop.MaSoGD).FirstOrDefault().Ten),
                MaPhongGD = u.truong.MaPhongGD,
                IdPhongGD = u.truong.IdPhongGD.Value,
                TenPhongGD = (_context.PhongGD.Where(x => x.Id == u.truong.IdPhongGD).FirstOrDefault().Ten),
                IdTruong = u.truong.Id,
                MaTruong = u.truong.Ma,
                TenTruong = (_context.Truong.Where(x => x.Ma == u.truong.Ma && x.MaNamHoc == u.truong.MaNamHoc).FirstOrDefault().Ten),
                MaKhoi = u.Lop.MaKhoi,
                TenKhoi = (_context.DmKhoi.Where(x => x.Ma == u.Lop.MaKhoi).FirstOrDefault().Ten),
                MaNhomTuoiMN = u.Lop.MaNhomTuoiMN,
                TenNhomTuoiMN = (_context.DmNhomTuoiMN.Where(x => x.Ma == u.Lop.MaNhomTuoiMN).FirstOrDefault().Ten),
                MaSoBuoiHocTrenTuan = u.Lop.MaSoBuoiHocTrenTuan,
                TenSoBuoiHocTrenTuan = (_context.DmSoBuoiHocTrenTuan.Where(x => x.Ma == u.Lop.MaSoBuoiHocTrenTuan).FirstOrDefault().Ten),
                MaCapHoc = u.Lop.MaCapHoc,
                ThuTu = u.Lop.ThuTu.Value,
                TrangThai = u.Lop.TrangThai,
                IsLopGhep = u.Lop.IsLopGhep.Value,

            }).ToListAsync();

            return Ok(LopVm);
        }
        // URL: GET: http://localhost:5001/api/Lop/?quer
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLopPaging(string filter, int pageIndex, int pageSize, string maSoGD, string maTruong, int maNamHoc, string maCapHoc)
        {
            var query = from Lop in _context.Lop
                        join truong in _context.Truong on Lop.IdTruong equals truong.Id
                        select new { Lop, truong };
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Lop.Ten.Contains(filter));
            }
            if (!string.IsNullOrEmpty(maSoGD))
            {
                query = query.Where(x => x.truong.MaSoGD == maSoGD);
            }
            if (!string.IsNullOrEmpty(maCapHoc))
            {
                query = query.Where(x => x.Lop.MaCapHoc == maCapHoc);
            }
            if (!string.IsNullOrEmpty(maTruong))
            {
                query = query.Where(x => x.truong.Ma == maTruong);
            }
            query = query.Where(x => x.Lop.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"Lop with maTruong: {maTruong} is not found"));
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(u => new LopVm()
            {
                Id = u.Lop.Id,
                Ma = u.Lop.Ma,
                Ten = u.Lop.Ten,
                MaSoGD = u.Lop.MaSoGD,
                TenSoGD = !string.IsNullOrEmpty(u.Lop.MaSoGD) ? (_context.SoGD.Where(x => x.Ma == u.Lop.MaSoGD).FirstOrDefault().Ten) : "",
                MaPhongGD = u.truong.MaPhongGD,
                IdPhongGD = u.truong.IdPhongGD.Value,
                TenPhongGD = !string.IsNullOrEmpty(u.truong.MaPhongGD) ? (_context.PhongGD.Where(x => x.Id == u.truong.IdPhongGD).FirstOrDefault().Ten) : "",
                IdTruong = u.truong.Id,
                MaTruong = u.truong.Ma,
                TenTruong = !string.IsNullOrEmpty(u.truong.Ma) ? (_context.Truong.Where(x => x.Ma == u.truong.Ma && x.MaNamHoc == u.truong.MaNamHoc).FirstOrDefault().Ten) : "",
                MaKhoi = u.Lop.MaKhoi,
                TenKhoi = !string.IsNullOrEmpty(u.Lop.MaKhoi) ? (_context.DmKhoi.Where(x => x.Ma == u.Lop.MaKhoi).FirstOrDefault().Ten) : "",
                MaNhomTuoiMN = u.Lop.MaNhomTuoiMN,
                TenNhomTuoiMN = !string.IsNullOrEmpty(u.Lop.MaNhomTuoiMN) ? (_context.DmNhomTuoiMN.Where(x => x.Ma == u.Lop.MaNhomTuoiMN).FirstOrDefault().Ten) : "",
                MaSoBuoiHocTrenTuan = u.Lop.MaSoBuoiHocTrenTuan,
                TenSoBuoiHocTrenTuan = !string.IsNullOrEmpty(u.Lop.MaSoBuoiHocTrenTuan) ? (_context.DmSoBuoiHocTrenTuan.Where(x => x.Ma == u.Lop.MaSoBuoiHocTrenTuan).FirstOrDefault().Ten) : "",
                MaCapHoc = u.Lop.MaCapHoc,
                TenCapHoc = !string.IsNullOrEmpty(u.Lop.MaCapHoc) ? (_context.DmCapHoc.Where(x => x.Ma == u.Lop.MaCapHoc).FirstOrDefault().Ten) : "",
                ThuTu = u.Lop.ThuTu.Value,
                TrangThai = u.Lop.TrangThai,
                IsLopGhep = u.Lop.IsLopGhep.HasValue ? u.Lop.IsLopGhep.Value : 0,
                TextTrangThai = u.Lop.TrangThai == 1 ? "Hiệu lực" : "Không hiệu lực",
                TextLopGhep = u.Lop.IsLopGhep.HasValue ? (u.Lop.IsLopGhep.Value == 1 ? "Lớp ghép" : "") : "",

            }).ToListAsync();

            var pagination = new Pagination<LopVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(decimal id)
        {
            var Lop = await _context.Lop.FindAsync(id);
            if (Lop == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found Lop with id {id}"));

            var LopVm = new LopVm()
            {
                Id = Lop.Id,
                Ma = Lop.Ma,
                Ten = Lop.Ten,
                MaSoGD = Lop.MaSoGD,
                //MaPhongGD = Lop.MaPhongGD,
                //IdPhongGD = Lop.IdPhongGD.Value,
                IdTruong = Lop.IdTruong,
                MaTruong = Lop.MaTruong,
                MaKhoi = Lop.MaKhoi,
                MaNhomTuoiMN = Lop.MaNhomTuoiMN,
                MaSoBuoiHocTrenTuan = Lop.MaSoBuoiHocTrenTuan,
                MaCapHoc = Lop.MaCapHoc,
                ThuTu = Lop.ThuTu.Value,
                TrangThai = Lop.TrangThai,
                IsLopGhep = Lop.IsLopGhep.Value,
            };
            return Ok(LopVm);
        }
        // URL: DELETE: http://localhost:5001/api/lop/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_LOP, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteLop(decimal id)
        {
            var lop = await _context.Lop.FindAsync(id);
            if (lop == null)
                return NotFound();

            _context.Lop.Remove(lop);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var lopVm = new LopVm()
                {
                    Id = lop.Id,
                    Ma = lop.Ma,
                    Ten = lop.Ten,
                    MaCapHoc = lop.MaCapHoc,
                };
                return Ok(lopVm);
            }
            return BadRequest();
        }
    }
}