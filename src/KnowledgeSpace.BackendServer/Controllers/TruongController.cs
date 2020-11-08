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
        [ClaimRequirement(FunctionCode.CONTENT_TRUONG, CommandCode.CREATE)]
        public async Task<IActionResult> PostTruong([FromBody] TruongCreateRequest request)
        {
            _logger.LogInformation("Begin PostTruong API");

            var dbTruong = await _context.Truong.FindAsync(request.Id);
            if (dbTruong != null)
                return BadRequest(new ApiBadRequestResponse($"Truong with id {request.Id} is existed."));
            request.MaNamHoc = SystemConstants.NamHoc.MaNamHoc;
            decimal? idHuyen = null;
            if(!string.IsNullOrEmpty(request.MaHuyen))
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
                if (!string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                TruongDetail.MaPhongGD = request.MaPhongGD;
                TruongDetail.IdPhongGD = idPhongGd;
            }
            else
            {
                TruongDetail.MaPhongGD = null;
                TruongDetail.IdPhongGD = null;
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
        [ClaimRequirement(FunctionCode.CONTENT_TRUONG, CommandCode.UPDATE)]
        public async Task<IActionResult> PutTruong(decimal id, [FromBody] TruongCreateRequest request)
        {
            _logger.LogInformation("Begin PutTruong API");
            var Truong = await _context.Truong.FindAsync(id);
            if (Truong == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found Truong with id {id}"));
            request.MaNamHoc = SystemConstants.NamHoc.MaNamHoc;
            decimal? idHuyen = null;
            if (!string.IsNullOrEmpty(request.MaHuyen))
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
                if (!string.IsNullOrEmpty(request.MaPhongGD))
                    idPhongGd = _context.PhongGD.Where(p => p.Ma == request.MaPhongGD && p.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id; ;

                Truong.MaPhongGD = Truong.MaPhongGD;
                Truong.IdPhongGD = idPhongGd;
            }
            else
            {
                Truong.MaPhongGD = null;
                Truong.IdPhongGD = null;
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
            Truong.NgaySua = DateTime.Now;

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
        [AllowAnonymous]
        public async Task<IActionResult> GetTruongBySoGDByNamHoc(string maSoGD, int maNamHoc, string maCapHoc)
        {
            var query = from t in _context.Truong
                        join s in _context.SoGD on t.MaSoGD equals s.Ma
                        join h in _context.DmHuyen on new { A = t.MaHuyen, B = t.MaNamHoc } equals new { A = h.Ma, B = h.MaNamHoc }
                        join v in _context.DmVung on new { A = t.MaVung } equals new { A = v.Ma }
                        select new { t, s, h, v };
            if (!string.IsNullOrEmpty(maSoGD))
            {
                query = query.Where(x => x.t.MaSoGD == maSoGD);
            }
            if (maCapHoc == SysCapHoc.MamNon)
                query = query.Where(x => x.t.IsCapMN == 1);
            if (maCapHoc == SysCapHoc.C1)
                query = query.Where(x => x.t.IsCapTH == 1);
            if (maCapHoc == SysCapHoc.C2)
                query = query.Where(x => x.t.IsCapTHCS == 1);
            if (maCapHoc == SysCapHoc.C3)
                query = query.Where(x => x.t.IsCapTHPT == 1);
            if (maCapHoc == SysCapHoc.GDTX)
                query = query.Where(x => x.t.IsCapGDTX == 1);

            query = query.Where(x => x.t.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"Truong with maSoGD: {maSoGD} is not found"));
            var TruongVm = await query.Select(u => new TruongVm()
            {
                Id = u.t.Id,
                Ma = u.t.Ma,
                Ten = u.t.Ten,
                MaSoGD = u.t.Ma,
                TenSoGD = u.s.Ten,
                MaPhongGD = u.t.MaPhongGD,
                IdPhongGD = u.t.IdPhongGD.HasValue ? u.t.IdPhongGD.Value : 0,
                TenPhongGD = !string.IsNullOrEmpty(u.t.MaPhongGD) ? (_context.PhongGD.Where(x => x.Id == u.t.IdPhongGD).FirstOrDefault().Ten) : "",
                MaTinh = u.t.MaTinh,
                TenTinh = !string.IsNullOrEmpty(u.t.MaTinh) ? (_context.DmTinh.Where(x => x.Ma == u.t.MaTinh).FirstOrDefault().Ten) : "",
                MaLoaiHinh = u.t.MaLoaiHinh,
                TenLoaiHinh = !string.IsNullOrEmpty(u.t.MaLoaiHinh) ? (_context.DmLoaiHinh.Where(x => x.Ma == u.t.MaLoaiHinh && x.MaNamHoc == u.t.MaNamHoc).FirstOrDefault().Ten) : "",
                MaLoaiTruong = u.t.MaLoaiTruong,
                TenLoaiTruong = !string.IsNullOrEmpty(u.t.MaLoaiTruong) ? (_context.DmLoaiTruong.Where(x => x.Ma == u.t.MaLoaiTruong).FirstOrDefault().Ten) : "",
                MaNhomCapHoc = u.t.MaNhomCapHoc,
                TenNhomCapHoc = !string.IsNullOrEmpty(u.t.MaNhomCapHoc) ? (_context.DmNhomCapHoc.Where(x => x.Ma == u.t.MaNhomCapHoc).FirstOrDefault().Ten) : "",
                MaCapHoc = !string.IsNullOrEmpty(u.t.MaNhomCapHoc) ? (_context.DmNhomCapHoc.Where(x => x.Ma == u.t.MaNhomCapHoc).FirstOrDefault().Cap) : "",
                MaHuyen = u.h.Ma,
                TenHuyen = u.h.Ten,
                DsCapHoc = u.t.DSCapHoc,
                DiaChi = u.t.DiaChi,
                DienThoai = u.t.DienThoai,
                Email = u.t.Email,
                Fax = u.t.Fax,
                Website = u.t.Website,
                ThuTu = u.t.ThuTu.HasValue ? u.t.ThuTu.Value : 0,
                IsCapMN = u.t.IsCapMN,
                IsCapTH = u.t.IsCapTH,
                IsCapTHCS = u.t.IsCapTHCS,
                IsCapTHPT = u.t.IsCapTHPT,
                IsCapGDTX = u.t.IsCapGDTX,

            }).ToListAsync();

            return Ok(TruongVm);
        }
        // URL: GET: http://localhost:5001/api/PhongGd/?quer
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTruongPaging(string filter, int pageIndex, int pageSize, string maSoGD, int maNamHoc, string maCapHoc)
        {
            var query = from t in _context.Truong
                        join s in _context.SoGD on t.MaSoGD equals s.Ma
                        join h in _context.DmHuyen on new { A = t.MaHuyen, B = t.MaNamHoc } equals new { A = h.Ma, B = h.MaNamHoc }
                        join v in _context.DmVung on new { A = t.MaVung } equals new { A = v.Ma }
                        select new { t, s, h, v };
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.t.Ten.Contains(filter));
            }
            if (!string.IsNullOrEmpty(maSoGD))
            {
                query = query.Where(x => x.t.MaSoGD == maSoGD);
            }
            if(maCapHoc == SysCapHoc.MamNon)
                query = query.Where(x => x.t.IsCapMN == 1);
            if (maCapHoc == SysCapHoc.C1)
                query = query.Where(x => x.t.IsCapTH == 1);
            if (maCapHoc == SysCapHoc.C2)
                query = query.Where(x => x.t.IsCapTHCS == 1);
            if (maCapHoc == SysCapHoc.C3)
                query = query.Where(x => x.t.IsCapTHPT == 1);
            if (maCapHoc == SysCapHoc.GDTX)
                query = query.Where(x => x.t.IsCapGDTX == 1);

            query = query.Where(x => x.t.MaNamHoc == maNamHoc);
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new TruongVm()
                {
                    Id = u.t.Id,
                    Ma = u.t.Ma,
                    Ten = u.t.Ten,
                    MaSoGD = u.t.Ma,
                    TenSoGD = u.s.Ten,
                    MaPhongGD = u.t.MaPhongGD,
                    IdPhongGD = u.t.IdPhongGD.HasValue ? u.t.IdPhongGD.Value : 0,
                    TenPhongGD = !string.IsNullOrEmpty(u.t.MaPhongGD) ?  (_context.PhongGD.Where(x => x.Id == u.t.IdPhongGD).FirstOrDefault().Ten) : "",
                    MaTinh = u.t.MaTinh,
                    TenTinh = !string.IsNullOrEmpty(u.t.MaTinh) ?  (_context.DmTinh.Where(x => x.Ma == u.t.MaTinh).FirstOrDefault().Ten) : "",
                    MaLoaiHinh = u.t.MaLoaiHinh,
                    TenLoaiHinh = !string.IsNullOrEmpty(u.t.MaLoaiHinh) ? (_context.DmLoaiHinh.Where(x => x.Ma == u.t.MaLoaiHinh && x.MaNamHoc == u.t.MaNamHoc).FirstOrDefault().Ten) : "",
                    MaLoaiTruong = u.t.MaLoaiTruong,
                    TenLoaiTruong = !string.IsNullOrEmpty(u.t.MaLoaiTruong) ? (_context.DmLoaiTruong.Where(x => x.Ma == u.t.MaLoaiTruong).FirstOrDefault().Ten) : "",
                    MaNhomCapHoc = u.t.MaNhomCapHoc,
                    TenNhomCapHoc = !string.IsNullOrEmpty(u.t.MaNhomCapHoc) ? (_context.DmNhomCapHoc.Where(x => x.Ma == u.t.MaNhomCapHoc).FirstOrDefault().Ten) : "",
                    MaCapHoc = !string.IsNullOrEmpty(u.t.MaNhomCapHoc) ? (_context.DmNhomCapHoc.Where(x => x.Ma == u.t.MaNhomCapHoc).FirstOrDefault().Cap) : "",
                    MaHuyen = u.h.Ma,
                    TenHuyen = u.h.Ten,
                    DsCapHoc = u.t.DSCapHoc,
                    DiaChi = u.t.DiaChi,
                    DienThoai = u.t.DienThoai,
                    Email = u.t.Email,
                    Fax = u.t.Fax,
                    Website = u.t.Website,
                    ThuTu = u.t.ThuTu.HasValue ? u.t.ThuTu.Value : 0,
                    IsCapMN = u.t.IsCapMN,
                    IsCapTH = u.t.IsCapTH,
                    IsCapTHCS = u.t.IsCapTHCS,
                    IsCapTHPT = u.t.IsCapTHPT,
                    IsCapGDTX = u.t.IsCapGDTX,
                })
                .ToListAsync();

            var pagination = new Pagination<TruongVm>
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
            var Truong = await _context.Truong.FindAsync(id);
            if (Truong == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found Truong with id {id}"));

            var TruongVm = new TruongVm()
            {
                Id = Truong.Id,
                Ma = Truong.Ma,
                Ten = Truong.Ten,
                MaSoGD = Truong.MaSoGD,
                MaPhongGD = Truong.MaPhongGD,
                IdPhongGD = Truong.IdPhongGD,
                MaTinh = Truong.MaTinh,
                MaLoaiHinh = Truong.MaLoaiHinh,
                MaLoaiTruong = Truong.MaLoaiTruong,
                MaNhomCapHoc = Truong.MaNhomCapHoc,
                MaCapHoc = !string.IsNullOrEmpty(Truong.MaNhomCapHoc) ? (_context.DmNhomCapHoc.Where(x => x.Ma == Truong.MaNhomCapHoc).FirstOrDefault().Cap) : "",
                MaHuyen = Truong.MaHuyen,
                DsCapHoc = Truong.DSCapHoc,
                DiaChi = Truong.DiaChi,
                DienThoai = Truong.DienThoai,
                Email = Truong.Email,
                Fax = Truong.Fax,
                Website = Truong.Website,
                ThuTu = Truong.ThuTu.HasValue ? Truong.ThuTu.Value : 0,
                IsCapMN = Truong.IsCapMN.HasValue ? Truong.IsCapMN : null,
                IsCapTH = Truong.IsCapTH.HasValue ? Truong.IsCapTH : null,
                IsCapTHCS =  Truong.IsCapTHCS.HasValue ?Truong.IsCapTHCS : null,
                IsCapTHPT =  Truong.IsCapTHPT.HasValue ?Truong.IsCapTHPT : null,
                IsCapGDTX = Truong.IsCapGDTX.HasValue ? Truong.IsCapGDTX : null,
            };
            return Ok(TruongVm);
        }
        // URL: DELETE: http://localhost:5001/api/Category/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_TRUONG, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteTruong(decimal id)
        {
            var truong = await _context.Truong.FindAsync(id);
            if (truong == null)
                return NotFound();

            _context.Truong.Remove(truong);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var truongVm = new TruongVm()
                {
                    Id = truong.Id,
                    Ma = truong.Ma,
                    Ten = truong.Ten,
                    MaTinh = truong.MaTinh,
                };
                return Ok(truongVm);
            }
            return BadRequest();
        }
    }
}