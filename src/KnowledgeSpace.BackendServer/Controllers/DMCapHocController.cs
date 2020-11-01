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
    public class DMCapHocController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMCapHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmCapHoc()
        {
            var dmCapHoc = _context.DmCapHoc;

            var dmCapHocVms = await dmCapHoc.Select(u => new DMCapHocVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmCapHocVms);
        }

        [HttpGet("{ma}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmCapHocByMa(string ma)
        {
            var dmCapHoc = await _context.DmCapHoc.FindAsync(ma);

            var dmCapHochVm = new DMCapHocVm()
            {
                Ma = dmCapHoc.Ma,
                Ten = dmCapHoc.Ten,
                ThuTu = dmCapHoc.ThuTu.HasValue ? dmCapHoc.ThuTu.Value : 0,
            };
            return Ok(dmCapHochVm);
        }

    }
}