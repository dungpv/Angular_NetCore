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
    public class DMLoaiKhuyetTatController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMLoaiKhuyetTatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmLoaiKhuyetTat()
        {
            var dmLoaiKhuyetTat = _context.DmLoaiKhuyetTat;

            var dmLoaiKhuyetTatVms = await dmLoaiKhuyetTat.Select(u => new DMLoaiKhuyetTatVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.Value,
            }).ToListAsync();

            return Ok(dmLoaiKhuyetTatVms);
        }
        
    }
}