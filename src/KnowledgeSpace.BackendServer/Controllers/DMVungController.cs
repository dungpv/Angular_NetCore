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
    public class DMVungController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMVungController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmVungByNamHoc()
        {
            var query  = from p in _context.DmVung
                                select new { p };

            var dmVungVms = await query.Select(u => new DMVungVm()
            {
                Ma = u.p.Ma,
                Ten = u.p.Ten,
                ThuTu = u.p.ThuTu,

            }).ToListAsync();

            return Ok(dmVungVms);
        }
        
    }
}