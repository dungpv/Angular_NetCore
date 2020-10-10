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

    public class SoGDController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public SoGDController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetSoGD()
        {
            var query = from s in _context.SoGD
                        join c in _context.DmCumThiDua on s.MaCumThiDua equals c.Ma
                        select new { s, c };

            var soGDVms = await query.Select(u => new SoGdVm()
            {
                Ma = u.s.Ma,
                Ten = u.s.Ten,
                MaTinh = u.s.MaTinh,
                DiaChi = u.s.DiaChi,
                DienThoai = u.s.DienThoai,
                Email = u.s.Email,
                Fax = u.s.Fax,
                Website = u.s.Website,
                TenCumThiDua = u.c.Ten,
                ThuTu = u.s.ThuTu,

            }).ToListAsync();

            return Ok(soGDVms);
        }
        
    }
}