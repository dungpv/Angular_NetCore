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
    public class DMNhomCapHocController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMNhomCapHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmNhomCapHocByCapHoc(string maCapHoc)
        {
            var query  = from p in _context.DmNhomCapHoc
                                select new { p };
            if (!string.IsNullOrEmpty(maCapHoc))
            {
                query = query.Where(x => x.p.Cap == maCapHoc);
            }
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"DMNhomCapHoc with maCapHoc: {maCapHoc} is not found"));
            var dmNhomCapHocVms = await query.Select(u => new DMNhomCapHocVm()
            {
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                DsCap = u.p.DSCap,
                Cap = u.p.Cap,

            }).ToListAsync();

            return Ok(dmNhomCapHocVms);
        }
        [HttpGet("{ma}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmNhomCapHocByMa(string ma)
        {
            var dmNhomCapHoc = await _context.DmNhomCapHoc.FindAsync(ma);

            var dmNhomCapHochVm = new DMNhomCapHocVm()
            {
                Ma = dmNhomCapHoc.Ma,
                Ten = dmNhomCapHoc.Ten,
                DsCap = dmNhomCapHoc.DSCap,
                Cap = dmNhomCapHoc.Cap,
            };
            return Ok(dmNhomCapHochVm);
        }
    }
}