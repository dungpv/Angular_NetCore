using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class DMTrinhDoChuyenMonNghiepVuController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMTrinhDoChuyenMonNghiepVuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmTrinhDoChuyenMonNghiepVuByNamHoc(int maNamHoc)
        {
            var query  = from p in _context.DmTrinhDoChuyenMonNghiepVu
                                select new { p };
            if (maNamHoc > 0)
            {
                query = query.Where(x => x.p.MaNamHoc == maNamHoc);
            }

            var dmTrinhDoChuyenMonNghiepVuVms = await query.Select(u => new DMTrinhDoChuyenMonNghiepVuVm()
            {
                MaNamHoc = u.p.MaNamHoc,
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                ThuTu = u.p.ThuTu.Value,

            }).ToListAsync();

            return Ok(dmTrinhDoChuyenMonNghiepVuVms);
        }
        
    }
}