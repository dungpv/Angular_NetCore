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
    public class DMLyDoNghiViecController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMLyDoNghiViecController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmLyDoNghiViec()
        {
            var dmLyDoNghiViec = _context.DmLyDoNghiViec;

            var dmLyDoNghiViecVms = await dmLyDoNghiViec.Select(u => new DMLyDoNghiViecVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmLyDoNghiViecVms);
        }
        
    }
}