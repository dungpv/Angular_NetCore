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
    public class DMNgachController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMNgachController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmNgachByCapHoc(string maCapHoc)
        {
            var query  = from p in _context.DmNgach
                                select new { p };
            if (maCapHoc == SysCapHoc.MamNon)
                query = query.Where(x => x.p.IsMN == 1);
            if (maCapHoc == SysCapHoc.C1)
                query = query.Where(x => x.p.IsC1 == 1);
            if (maCapHoc == SysCapHoc.C2)
                query = query.Where(x => x.p.IsC2 == 1);
            if (maCapHoc == SysCapHoc.C3)
                query = query.Where(x => x.p.IsC3 == 1);
            if (maCapHoc == SysCapHoc.GDTX)
                query = query.Where(x => x.p.IsGDTX == 1);
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"DMNgach with maCapHoc: {maCapHoc} is not found"));
            var dmNgachVms = await query.Select(u => new DMNgachVm()
            {
                Ma = u.p.Ma,
                MaNgach = u.p.MaNgach,
                Ten = u.p.Ten,
                LoaiNgach = u.p.LoaiNgach,
                ThuTu = u.p.ThuTu.Value,

            }).ToListAsync();

            return Ok(dmNgachVms);
        }
        
    }
}