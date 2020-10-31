using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Helpers;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class DMHuyenController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMHuyenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmHuyenByTinhByNamHoc(string maTinh, int maNamHoc)
        {
            var query = from h in _context.DmHuyen
                        join t in _context.DmTinh on h.MaTinh equals t.Ma
                        select new { h, t };
            if (!string.IsNullOrEmpty(maTinh))
            {
                query = query.Where(x => x.h.MaTinh == maTinh);
            }
            
            query = query.Where(x => x.h.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"DMHuyen with maTinh: {maTinh} is not found"));

            var dmHuyenVms = await query.Select(u => new DMHuyenVm()
            {
                MaNamHoc = u.h.MaNamHoc,
                MaTinh = u.h.MaTinh,
                TenTinh = u.t.Ten,
                Ma = u.h.Ma,
                Ten = u.h.Ten,
                Cap = u.h.Cap,
                ThuTu = u.h.ThuTu.HasValue ? u.h.ThuTu.Value : 0,

            }).ToListAsync();

            return Ok(dmHuyenVms);
        }
        // URL: GET: http://localhost:5001/api/DmHuyen/?quer
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmHuyenByMa(string ma, int maNamHoc)
        {
            var dmHuyen = _context.DmHuyen.Where(x => x.Ma == ma && x.MaNamHoc == maNamHoc);

            var dmHuyenVm = await dmHuyen.Select(u => new DMHuyenVm()
            {
                MaNamHoc = u.MaNamHoc,
                MaTinh = u.MaTinh,
                TenTinh = u.Ten,
                Ma = u.Ma,
                Ten = u.Ten,
                Cap = u.Cap,
                ThuTu = u.ThuTu.HasValue ? u.ThuTu.Value : 0,
            }).ToListAsync();
            return Ok(dmHuyenVm);
        }
    }
}