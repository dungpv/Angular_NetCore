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
    public class DMLoaiCanBoController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMLoaiCanBoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmLoaiCanBoByNhomCanBoByCapHoc(string maCapHoc, string maNhomCanBo)
        {
            var query  = from p in _context.DmLoaiCanBo
                                select new { p };
            if (!string.IsNullOrEmpty(maNhomCanBo))
            {
                query = query.Where(x => x.p.MaNhomCanBo == maNhomCanBo);
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
                return NotFound(new ApiNotFoundResponse($"DMLoaiCanBo with maCapHoc: {maCapHoc} is not found"));
            var dmLoaiCanBoVms = await query.Select(u => new DMLoaiCanBoVm()
            {
                MaNhomCanBo = u.p.MaNhomCanBo,
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                ThuTu = u.p.ThuTu,

            }).ToListAsync();

            return Ok(dmLoaiCanBoVms);
        }
        
    }
}