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
                IdHuyen = idHuyen,
                MaHuyen = request.MaHuyen,
                DiaChi = request.DiaChi,
                DienThoai = request.DienThoai,
                Email = request.Email,
                Fax = request.Fax,
                Website = request.Website,
                ThuTu = request.ThuTu,
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
        public async Task<IActionResult> PutPhongGD(string id, [FromBody] PhongGDCreateRequest request)
        {
            _logger.LogInformation("Begin PutPhongGD API");
            var phongGD = await _context.PhongGD.FindAsync(id);
            if (phongGD == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found PhongGD with id {id}"));

            decimal idHuyen = _context.DmHuyen.Where(h => h.Ma == request.MaHuyen && h.MaNamHoc == request.MaNamHoc).FirstOrDefault().Id;

            phongGD.Ma = request.Ma;
            phongGD.Ten = request.Ten;
            phongGD.MaSoGD = request.MaSoGD;
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
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                MaSoGD = u.s.Ma,
                TenSoGD = u.s.Ten,
                MaHuyen = u.h.Ma,
                TenHuyen = u.h.Ten,
                DiaChi = u.p.DiaChi,
                DienThoai = u.p.DienThoai,
                Email = u.p.Email,
                Fax = u.p.Fax,
                Website = u.p.Website,
                TenVung = u.v.Ten,
                ThuTu = u.s.ThuTu,

            }).ToListAsync();

            return Ok(PhongGDVm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var phongGD = await _context.PhongGD.FindAsync(id);
            if (phongGD == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found function with id {id}"));

            var phongGDVm = new PhongGDVm()
            {
                Ma = phongGD.Ma,
                Ten = phongGD.Ten,
                MaSoGD = phongGD.Ma,
                TenSoGD = phongGD.Ten,
                MaHuyen = phongGD.Ma,
                TenHuyen = phongGD.Ten,
                DiaChi = phongGD.DiaChi,
                DienThoai = phongGD.DienThoai,
                Email = phongGD.Email,
                Fax = phongGD.Fax,
                Website = phongGD.Website,
                TenVung = phongGD.Ten,
                ThuTu = phongGD.ThuTu.Value,
            };
            return Ok(phongGDVm);
        }

    }
}