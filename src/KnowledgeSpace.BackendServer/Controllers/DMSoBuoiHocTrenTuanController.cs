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
    public class DMSoBuoiHocTrenTuanController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMSoBuoiHocTrenTuanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmSoBuoiHocTrenTuan()
        {
            var dmSoBuoiHocTrenTuan = _context.DmSoBuoiHocTrenTuan;

            var dmSoBuoiHocTrenTuanVms = await dmSoBuoiHocTrenTuan.Select(u => new DMSoBuoiHocTrenTuanVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmSoBuoiHocTrenTuanVms);
        }
        
    }
}