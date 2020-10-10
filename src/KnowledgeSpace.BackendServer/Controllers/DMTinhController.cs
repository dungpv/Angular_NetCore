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
    public class DMTinhController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMTinhController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmTinh()
        {
            var query =  _context.DmTinh;

            var dmTinhVms = await query.Select(u => new DMTinhVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                Cap = u.Cap,
                ThuTu = u.ThuTu,

            }).ToListAsync();

            return Ok(dmTinhVms);
        }
        
    }
}