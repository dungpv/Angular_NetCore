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
    public class DMXaController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMXaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmXaByTinhByHuyenByNamHoc(string maTinh, string maHuyen, int maNamHoc)
        {
            var query = from x in _context.DmXa
                        join h in _context.DmHuyen on x.IdHuyen equals h.Id
                        join t in _context.DmTinh on h.MaTinh equals t.Ma
                        select new { h, t, x };
            if (!string.IsNullOrEmpty(maTinh))
            {
                query = query.Where(u => u.x.MaTinh == maTinh);
            }
            if (!string.IsNullOrEmpty(maHuyen))
            {
                query = query.Where(u => u.x.MaHuyen == maHuyen);
            }

            query = query.Where(u => u.h.MaNamHoc == maNamHoc);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"DMXa with maTinh: {maTinh} is not found"));
            var dmXaVms = await query.Select(u => new DMXaVm()
            {
                MaNamHoc = u.x.MaNamHoc,
                MaTinh = u.x.MaTinh,
                TenTinh = u.t.Ten,
                MaHuyen = u.h.Ma,
                TenHuyen = u.h.Ten,
                Ma = u.x.Ma,
                Ten = u.x.Ten,
                Cap = u.x.Cap,
                ThuTu = u.x.ThuTu.Value,

            }).ToListAsync();

            return Ok(dmXaVms);
        }
        
    }
}