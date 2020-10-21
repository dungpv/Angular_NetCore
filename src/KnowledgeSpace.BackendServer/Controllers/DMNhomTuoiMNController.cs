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
    public class DMNhomTuoiMNController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMNhomTuoiMNController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmNhomTuoiMN()
        {
            var query = _context.DmNhomTuoiMN;
            var dmNhomTuoiMNVms = await query.Select(u => new DMNhomTuoiMNVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                MaNhomTre = u.MaNhomTre,

            }).ToListAsync();

            return Ok(dmNhomTuoiMNVms);
        }
        
    }
}