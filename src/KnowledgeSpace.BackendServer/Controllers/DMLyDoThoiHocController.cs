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
    public class DMLyDoThoiHocController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMLyDoThoiHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmLyDoThoiHoc()
        {
            var dmLyDoThoiHoc = _context.DmLyDoThoiHoc;

            var dmLyDoThoiHocVms = await dmLyDoThoiHoc.Select(u => new DMLyDoThoiHocVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu,
            }).ToListAsync();

            return Ok(dmLyDoThoiHocVms);
        }
        
    }
}