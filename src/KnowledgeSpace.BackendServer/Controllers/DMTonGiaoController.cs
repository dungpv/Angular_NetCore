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
    public class DMTonGiaoController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMTonGiaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmTonGiao()
        {
            var dmTonGiao = _context.DMTonGiao;

            var dmTonGiaoVms = await dmTonGiao.Select(u => new DMTonGiaoVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmTonGiaoVms);
        }
        
    }
}