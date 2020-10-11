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
    public class DMTrangThaiCanBoController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMTrangThaiCanBoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmTrangThaiCanBo()
        {
            var dmTrangThaiCanBo = _context.DmTrangThaiCanBo;

            var dmTrangThaiCanBoVms = await dmTrangThaiCanBo.Select(u => new DMTrangThaiCanBoVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmTrangThaiCanBoVms);
        }
        
    }
}