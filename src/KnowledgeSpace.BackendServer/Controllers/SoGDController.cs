using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Helpers;
using KnowledgeSpace.ViewModels;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using KnowledgeSpace.BackendServer.Data.Entities;

namespace KnowledgeSpace.BackendServer.Controllers
{

    public class SoGDController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SoGDController> _logger;
        public SoGDController(ApplicationDbContext context, ILogger<SoGDController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSoGD()
        {
            var query = from s in _context.SoGD
                        join c in _context.DmCumThiDua on s.MaCumThiDua equals c.Ma
                        join t in _context.DmTinh on s.MaTinh equals t.Ma
                        select new { s, c, t };

            var soGDVms = await query.Select(u => new SoGdVm()
            {
                Ma = u.s.Ma,
                Ten = u.s.Ten,
                MaTinh = u.s.MaTinh,
                TenTinh = u.t.Ten,
                DiaChi = u.s.DiaChi,
                DienThoai = u.s.DienThoai,
                Email = u.s.Email,
                Fax = u.s.Fax,
                Website = u.s.Website,
                TenCumThiDua = u.c.Ten,
                ThuTu = u.s.ThuTu.HasValue ? u.s.ThuTu.Value : 0,

            }).ToListAsync();

            return Ok(soGDVms);
        }

        // URL: GET: http://localhost:5001/api/Sogd/?quer
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSoGDPaging(string filter, int pageIndex, int pageSize)
        {
            var query = from s in _context.SoGD
                        join c in _context.DmCumThiDua on s.MaCumThiDua equals c.Ma
                        join t in _context.DmTinh on s.MaTinh equals t.Ma
                        select new { s, c, t };
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.s.Ten.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new SoGdVm()
                {
                    Ma = u.s.Ma,
                    Ten = u.s.Ten,
                    MaTinh = u.s.MaTinh,
                    TenTinh = u.t.Ten,
                    DiaChi = u.s.DiaChi,
                    DienThoai = u.s.DienThoai,
                    Email = u.s.Email,
                    Fax = u.s.Fax,
                    Website = u.s.Website,
                    TenCumThiDua = u.c.Ten,
                    ThuTu = u.s.ThuTu.HasValue ? u.s.ThuTu.Value : 0,
                })
                .ToListAsync();

            var pagination = new Pagination<SoGdVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        [HttpPost]
        [ApiValidationFilter]
        public async Task<IActionResult> PostSoGD([FromBody] SoGDCreateRequest request)
        {
            _logger.LogInformation("Begin PostSoGD API");

            var dbSoGD = await _context.SoGD.FindAsync(request.Id);
            if (dbSoGD != null)
                return BadRequest(new ApiBadRequestResponse($"SoGD with id {request.Id} is existed."));

            var SoGD = new SoGD()
            {
                Ma = request.Ma,
                Ten = request.Ten,
                MaTinh = request.MaTinh,
                DiaChi = request.DiaChi,
                DienThoai = request.DienThoai,
                Email = request.Email,
                Fax = request.Fax,
                Website = request.Website,
                ThuTu = request.ThuTu.Value,
                MaCumThiDua = request.MaCumThiDua,
            };
            _context.SoGD.Add(SoGD);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PostSoGD API - Success");

                return CreatedAtAction(nameof(GetByMa), new { ma = SoGD.Ma }, request);
            }
            else
            {
                _logger.LogInformation("End PostSoGD API - Failed");

                return BadRequest(new ApiBadRequestResponse("Create SoGD is failed"));
            }
        }

        [HttpPut("{Ma}")]
        public async Task<IActionResult> PutSoGD(string ma, [FromBody] SoGDCreateRequest request)
        {
            _logger.LogInformation("Begin PutSoGD API");
            var SoGD = await _context.SoGD.FindAsync(ma);
            if (SoGD == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found SoGD with ma {ma}"));

            SoGD.Ma = request.Ma;
            SoGD.Ten = request.Ten;
            SoGD.MaTinh = request.MaTinh;
            SoGD.DiaChi = request.DiaChi;
            SoGD.DienThoai = request.DienThoai;
            SoGD.Email = request.Email;
            SoGD.Fax = request.Fax;
            SoGD.Website = request.Website;
            SoGD.ThuTu = request.ThuTu;
            SoGD.MaCumThiDua = request.MaCumThiDua;

            _context.SoGD.Update(SoGD);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                _logger.LogInformation("End PutSoGD API - Success");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("End PutSoGD API - Failed");
                return BadRequest(new ApiBadRequestResponse("Update SoGD is failed"));
            }

        }
        // URL: GET: http://localhost:5001/api/SoGD/{ma}
        [HttpGet("{ma}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMa(string ma)
        {
            var soGD = await _context.SoGD.FindAsync(ma);
            if (soGD == null)
                return NotFound(new ApiNotFoundResponse($"SoGD with ma: {ma} is not found"));

            var soGDVm = new SoGdVm()
            {
                Ma = soGD.Ma,
                Ten = soGD.Ten,
                MaTinh = soGD.MaTinh,
                DiaChi = soGD.DiaChi,
                DienThoai = soGD.DienThoai,
                Email = soGD.Email,
                Fax = soGD.Fax,
                Website = soGD.Website,
                MaCumThiDua = soGD.MaCumThiDua,
                ThuTu = soGD.ThuTu.HasValue ? soGD.ThuTu.Value : 0,
            };
            return Ok(soGDVm);

        }
    }
}