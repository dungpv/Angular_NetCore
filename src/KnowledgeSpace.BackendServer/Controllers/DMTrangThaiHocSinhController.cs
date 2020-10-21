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
    public class DMTrangThaiHocSinhController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMTrangThaiHocSinhController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmTrangThaiHocSinh()
        {
            var dmTrangThaiHocSinh = _context.DmTrangThaiHocSinh;

            var dmTrangThaiHocSinhVms = await dmTrangThaiHocSinh.Select(u => new DMTrangThaiHocSinhVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmTrangThaiHocSinhVms);
        }
        
    }
}