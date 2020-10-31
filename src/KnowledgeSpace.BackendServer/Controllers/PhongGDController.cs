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
    public class PhongGDController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PhongGDController> _logger;

        public PhongGDController(ApplicationDbContext context, ILogger<PhongGDController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ApiValidationFilter]
        public async Task<IActionResult> PostPhongGD([FromBody] PhongGDCreateRequest request)
        {
            _logger.LogInformation("Begin PostPhongGD API");

            var dbPhongGD = await _context.PhongGD.FindAsync(request.Id);
            if (dbPhongGD != null)
                return BadRequest(new ApiBadRequestResponse($"PhongGD with id {request.Id} is existed."));

            decimal idHuyen = _context.DmHuyen.Where(h => h.Ma == request.MaHuyen && h.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;

            var phongGD = new PhongGD()
            {
                Id = request.Id,
                Ma = request.Ma,
                Ten = request.Ten,
                MaSoGD = request.MaSoGD,
                MaTinh = request.MaTinh,
                IdHuyen = idHuyen,
                MaHuyen = request.MaHuyen,
                DiaChi = request.DiaChi,
                DienThoai = request.DienThoai,
                Email = request.Email,
                Fax = request.Fax,
                Website = request.Website,
                ThuTu = request.ThuTu.Value,
                MaNamHoc = request.MaNamHoc,
                MaVung = request.MaVung,
            };
            _context.PhongGD.Add(phongGD);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PostPhongGD API - Success");

                return CreatedAtAction(nameof(GetById), new { id = phongGD.Id }, request);
            }
            else
            {
                _logger.LogInformation("End PostPhongGD API - Failed");

                return BadRequest(new ApiBadRequestResponse("Create PhongGD is failed"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhongGD(decimal id, [FromBody] PhongGDCreateRequest request)
        {
            _logger.LogInformation("Begin PutPhongGD API");
            var phongGD = await _context.PhongGD.FindAsync(id);
            if (phongGD == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found PhongGD with id {id}"));
            request.MaNamHoc = SystemConstants.NamHoc.MaNamHoc;
            decimal idHuyen = _context.DmHuyen.Where(h => h.Ma == request.MaHuyen && h.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;

            phongGD.Ma = request.Ma;
            phongGD.Ten = request.Ten;
            phongGD.MaSoGD = request.MaSoGD;
            phongGD.MaTinh = request.MaTinh;
            phongGD.IdHuyen = idHuyen;
            phongGD.MaHuyen = request.MaHuyen;
            phongGD.DiaChi = request.DiaChi;
            phongGD.DienThoai = request.DienThoai;
            phongGD.Email = request.Email;
            phongGD.Fax = request.Fax;
            phongGD.Website = request.Website;
            phongGD.ThuTu = request.ThuTu;
            phongGD.MaNamHoc = request.MaNamHoc;
            phongGD.MaVung = request.MaVung;

            _context.PhongGD.Update(phongGD);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PutPhongGD API - Success");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("End PutPhongGD API - Failed");
                return BadRequest(new ApiBadRequestResponse("Update PhongGD is failed"));
            }

        }
        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhongGDBySoGDByNamHoc(string maSoGD, int maNamHoc)
        {
            var query = from p in _context.PhongGD
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
                return NotFound(new ApiNotFoundResponse($"PhongGD with maSoGD: {maSoGD} is not found"));
            var PhongGDVm = await query.Select(u => new PhongGDVm()
            {
                Id = u.p.Id,
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                MaSoGD = u.s.Ma,
                TenSoGD = u.s.Ten,
                MaTinh = u.s.MaTinh,
                MaHuyen = u.h.Ma,
                TenHuyen = u.h.Ten,
                DiaChi = u.p.DiaChi,
                DienThoai = u.p.DienThoai,
                Email = u.p.Email,
                Fax = u.p.Fax,
                Website = u.p.Website,
                TenVung = u.v.Ten,
                ThuTu = u.p.ThuTu.HasValue ? u.p.ThuTu.Value : 0,

            }).ToListAsync();

            return Ok(PhongGDVm);
        }
        // URL: GET: http://localhost:5001/api/PhongGd/?quer
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhongGDPaging(string filter, int pageIndex, int pageSize, string maSoGD, int maNamHoc)
        {
            var query = from p in _context.PhongGD
                        join h in _context.DmHuyen on p.MaHuyen equals h.Ma into ph
                        from subhuyen in ph.DefaultIfEmpty()
                        join s in _context.SoGD on p.MaSoGD equals s.Ma
                        select new { p, subhuyen, s };
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.p.Ten.Contains(filter));
            }
            if (!string.IsNullOrEmpty(maSoGD))
            {
                query = query.Where(x => x.s.Ma == maSoGD);
            }
            query = query.Where(x => x.subhuyen.MaNamHoc == maNamHoc);
            query = query.Where(x => x.p.MaNamHoc == maNamHoc);
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new PhongGDVm()
                {
                    Id = u.p.Id,
                    Ma = u.p.Ma,
                    Ten = u.p.Ten,
                    MaTinh = u.p.MaTinh,
                    MaSoGD = u.p.MaSoGD,
                    TenSoGD = u.s.Ten,
                    MaHuyen = u.p.MaHuyen,
                    TenHuyen = u.subhuyen.Ten,
                    DiaChi = u.p.DiaChi,
                    DienThoai = u.p.DienThoai,
                    Email = u.p.Email,
                    Fax = u.p.Fax,
                    Website = u.p.Website,
                    ThuTu = u.p.ThuTu.HasValue ? u.p.ThuTu.Value : 0,
                })
                .ToListAsync();

            var pagination = new Pagination<PhongGDVm>
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
            var phongGD = await _context.PhongGD.FindAsync(id);
            if (phongGD == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found function with id {id}"));

            var phongGDVm = new PhongGDVm()
            {
                Ma = phongGD.Ma,
                Ten = phongGD.Ten,
                MaSoGD = phongGD.MaSoGD,
                MaTinh = phongGD.MaTinh,
                MaHuyen = phongGD.MaHuyen,
                DiaChi = phongGD.DiaChi,
                DienThoai = phongGD.DienThoai,
                Email = phongGD.Email,
                Fax = phongGD.Fax,
                Website = phongGD.Website,
                TenVung = phongGD.Ten,
                ThuTu = phongGD.ThuTu.HasValue ? phongGD.ThuTu.Value : 0,
            };
            return Ok(phongGDVm);
        }
        // URL: DELETE: http://localhost:5001/api/Category/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_PHONGGD, CommandCode.DELETE)]
        public async Task<IActionResult> DeletePhongGd(decimal id)
        {
            var phonggd = await _context.PhongGD.FindAsync(id);
            if (phonggd == null)
                return NotFound();

            _context.PhongGD.Remove(phonggd);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var phonggdvm = new PhongGDVm()
                {   
                    Id= phonggd.Id,
                    Ma = phonggd.Ma,
                    Ten = phonggd.Ten,
                    MaTinh = phonggd.MaTinh,
                };
                return Ok(phonggdvm);
            }
            return BadRequest();
        }

    }
}