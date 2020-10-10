using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Helpers;
using KnowledgeSpace.ViewModels.CSDL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class DMKhoiController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMKhoiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetDmKhoiByCapHoc(string maCapHoc)
        {
            var query = from k in _context.DmKhoi
                        join c in _context.DmCapHoc on k.MaCapHoc equals c.Ma
                        select new { k, c };
            if (!string.IsNullOrEmpty(maCapHoc))
            {
                query = query.Where(x => x.k.MaCapHoc == maCapHoc);
            }
            if (query == null)
                return NotFound(new ApiNotFoundResponse($"DMKhoi with maCapHoc: {maCapHoc} is not found"));

            var dmKhoiVms = await query.Select(u => new DMKhoiVm()
            {
                Id = u.k.Id,
                MaCapHoc = u.k.MaCapHoc,
                TenCapHoc = u.c.Ten,
                Ma = u.k.Ma,
                Ten = u.k.Ten,
                MaLoaiLop = u.k.MaLoaiLop,
                ThuTu = u.k.ThuTu,

            }).ToListAsync();

            return Ok(dmKhoiVms);
        }
        
    }
}