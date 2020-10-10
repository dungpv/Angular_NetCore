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
    public class DMNuocController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMNuocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmNuoc()
        {
            var dmNuoc = _context.DmNuoc;

            var dmNuocVms = await dmNuoc.Select(u => new DMNuocVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu,
            }).ToListAsync();

            return Ok(dmNuocVms);
        }
        
    }
}