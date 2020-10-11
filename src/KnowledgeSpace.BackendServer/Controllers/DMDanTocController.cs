using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class DMDanTocController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DMDanTocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmDanToc()
        {
            var dmDanToc = _context.DmDanToc;

            var dmDanTocVms = await dmDanToc.Select(u => new DMDanTocVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                TenGoiKhac = u.TenGoiKhac,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmDanTocVms);
        }
        
    }
}