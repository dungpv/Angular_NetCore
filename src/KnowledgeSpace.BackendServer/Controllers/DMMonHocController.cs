using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Helpers;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class DMMonHocController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMMonHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmMonHocByNamHocByCapHoc(int maNamHoc, string maCapHoc)
        {
            var query  = from p in _context.DmMonHoc
                                select new { p };
            if (maNamHoc > 0)
            {
                query = query.Where(x => x.p.MaNamHoc == maNamHoc);
            }
            if (!string.IsNullOrEmpty(maCapHoc))
            {
                query = query.Where(x => x.p.MaCapHoc == maCapHoc);
            }
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"DMMonHoc with maCapHoc: {maCapHoc} is not found"));
            var dmMonHocVms = await query.Select(u => new DMMonHocVm()
            {
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                ThuTu = u.p.ThuTu.Value,
                KieuMonHoc = u.p.KieuMonHoc.Value,
                IsMonTc = u.p.IsMonTC,
            }).ToListAsync();

            return Ok(dmMonHocVms);
        }
        
    }
}