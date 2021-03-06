﻿using System;
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
    public class DMCumThiDuaController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public DMCumThiDuaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmCumThiDua()
        {
            var dmCumThiDua = _context.DmCumThiDua;

            var dmCumThiDuaVms = await dmCumThiDua.Select(u => new DMCumThiDuaVm()
            {
                Ma = u.Ma,
                Ten = u.Ten,
                ThuTu = u.ThuTu.HasValue ? u.ThuTu.Value : 0,
            }).ToListAsync();

            return Ok(dmCumThiDuaVms);
        }
        // URL: GET: http://localhost:5001/api/DMCumThiDua/{ma}
        [HttpGet("{ma}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDmCumThiDuaByMa(string ma)
        {
            var dmCumThiDua = await _context.DmCumThiDua.FindAsync(ma);

            var dmCumThiDuaVm = new DMCumThiDuaVm()
            {
                Ma = dmCumThiDua.Ma,
                Ten = dmCumThiDua.Ten,
                ThuTu = dmCumThiDua.ThuTu.HasValue ? dmCumThiDua.ThuTu.Value : 0,
            };
            return Ok(dmCumThiDuaVm);
        }
    }
}