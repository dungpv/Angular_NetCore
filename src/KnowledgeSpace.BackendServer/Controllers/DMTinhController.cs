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
    public class DMTinhController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMTinhController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmTinh()
        {
            var query =  _context.DmTinh;

            var dmTinhVms = await query.Select(u => new DMTinhVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                Cap = u.Cap,
                ThuTu = u.ThuTu.HasValue ? u.ThuTu.Value : 0,

            }).ToListAsync();

            return Ok(dmTinhVms);
        }
        // URL: GET: http://localhost:5001/api/DmTinh/{ma}
        [HttpGet("{ma}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmTinhByMa(string ma)
        {
            var dmTinh = await _context.DmTinh.FindAsync(ma);

            var dmTinhVm = new DMTinhVm()
            {
                Ma = dmTinh.Ma,
                Ten = dmTinh.Ten,
                Cap = dmTinh.Cap,
                ThuTu = dmTinh.ThuTu.HasValue ? dmTinh.ThuTu.Value : 0,
            };
            return Ok(dmTinhVm);
        }

    }
}