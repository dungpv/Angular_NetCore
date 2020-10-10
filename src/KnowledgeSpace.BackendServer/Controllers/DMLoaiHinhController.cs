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
    public class DMLoaiHinhController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMLoaiHinhController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmLoaiHinhByNamHocByCapHoc(int maNamHoc, string maCapHoc)
        {
            var query  = from p in _context.DmLoaiHinh
                                select new { p };
            if (maNamHoc > 0)
            {
                query = query.Where(x => x.p.MaNamHoc == maNamHoc);
            }
            if (maCapHoc == SysCapHoc.MamNon)
                query = query.Where(x => x.p.IsMN == 1);
            if (maCapHoc == SysCapHoc.C1)
                query = query.Where(x => x.p.IsC1 == 1);
            if (maCapHoc == SysCapHoc.C2)
                query = query.Where(x => x.p.IsC2 == 1);
            if (maCapHoc == SysCapHoc.C3)
                query = query.Where(x => x.p.IsC3 == 1);
            if (maCapHoc == SysCapHoc.GDTX)
                query = query.Where(x => x.p.IsGdtx == 1);

            if (query == null)
                return NotFound(new ApiNotFoundResponse($"DMLoaiHinh with maCapHoc: {maCapHoc} is not found"));

            var dmLoaiHinhVms = await query.Select(u => new DMLoaiHinhVm()
            {
                MaNamHoc = u.p.MaNamHoc,
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                ThuTu = u.p.ThuTu,

            }).ToListAsync();

            return Ok(dmLoaiHinhVms);
        }
        
    }
}