using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Authorization;
using KnowledgeSpace.BackendServer.Constants;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.ViewModels.Contents;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public partial class KnowledgeBasesController
    {
        #region Reports
        [HttpGet("{knowledgeBaseId}/reports/filter")]
        public async Task<IActionResult> GetReportPaging(int? knowledgeBaseId, string filter, int pageIndex, int pageSize)
        {
            var query = from r in _context.Reports
                        join u in _context.Users
                            on r.ReportUserId equals u.Id
                        select new { r, u };
            if (knowledgeBaseId.HasValue)
            {
                query = query.Where(x => x.r.KnowledgeBaseId == knowledgeBaseId.Value);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.r.Content.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ReportVm()
                {
                    Id = c.r.Id,
                    Content = c.r.Content,
                    CreateDate = c.r.CreateDate,
                    KnowledgeBaseId = c.r.KnowledgeBaseId,
                    LastModifiedDate = c.r.LastModifiedDate,
                    IsProcessed = false,
                    ReportUserId = c.r.ReportUserId,
                    ReportUserName = c.u.FirstName + " " + c.u.LastName
                }).ToListAsync();

            var pagination = new Pagination<ReportVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }
        [HttpGet("{knowledgeBaseId}/reports/{reportId}")]
        public async Task<IActionResult> GetReportDetail(int knowledgeBaseId, int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                return NotFound();

            var reportVm = new ReportVm()
            {
                Id = report.Id,
                Content = report.Content,
                CreateDate = report.CreateDate,
                KnowledgeBaseId = report.KnowledgeBaseId,
                LastModifiedDate = report.LastModifiedDate,
                IsProcessed = report.IsProcessed,
                ReportUserId = report.ReportUserId,
            };
            return Ok(reportVm);
        }

        [HttpPost("{knowledgeBaseId}/reports")]
        public async Task<IActionResult> PostReport(int knowledgeBaseId, [FromBody]ReportCreateRequest request)
        {
            var report = new Report()
            {
                Content = request.Content,
                KnowledgeBaseId = knowledgeBaseId,
                ReportUserId = request.ReportUserId,
                IsProcessed = false,
            };
            _context.Reports.Add(report);

            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase == null)
                return BadRequest();

            knowledgeBase.NumberOfReports = knowledgeBase.NumberOfReports.GetValueOrDefault(0) + 1;
            _context.KnowledgeBases.Update(knowledgeBase);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{knowledgeBaseId}/reports/{reportId}")]
        public async Task<IActionResult> PutReport(int reportId, [FromBody]ReportCreateRequest request)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                return NotFound();
            if (report.ReportUserId != User.Identity.Name)
                return Forbid();

            report.Content = request.Content;
            _context.Reports.Update(report);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{knowledgeBaseId}/report/{reportId}")]
        public async Task<IActionResult> DeleteReport(int knowledgeBaseId, int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                return NotFound();

            _context.Reports.Remove(report);
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase != null)
                return BadRequest();

            knowledgeBase.NumberOfReports = knowledgeBase.NumberOfReports.GetValueOrDefault(0) - 1;
            _context.KnowledgeBases.Update(knowledgeBase);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        #endregion
    }
}