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
    public class DMSoBuoiHocTrenTuanController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMSoBuoiHocTrenTuanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
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
        [HttpGet("{ma}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmSoBuoiHocTrenTuancByMa(string ma)
        {
            var dmSoBuoiHoc = await _context.DmSoBuoiHocTrenTuan.FindAsync(ma);

            var dmSoBuoiHocVm = new DMSoBuoiHocTrenTuanVm()
            {
                Ma = dmSoBuoiHoc.Ma,
                Ten = dmSoBuoiHoc.Ten,
                ThuTu = dmSoBuoiHoc.ThuTu.HasValue ? dmSoBuoiHoc.ThuTu.Value : 0,
            };
            return Ok(dmSoBuoiHocVm);
        }
    }
}