using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.ViewModels.Contents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public partial class KnowledgeBasesController
    {
        #region Attachments
        [HttpGet("{knowledgeBaseId}/attachments")]
        public async Task<IActionResult> GetAttachment(int knowledgeBaseId)
        {
            var query = await _context.Attachments.Where(x => x.KnowledgeBaseId == knowledgeBaseId)
                .Select(c => new AttachmentVm()
                {
                    Id = c.Id,
                    FileName = c.FileName,
                    FilePath = c.FilePath,
                    FileType = c.FileType,
                    FileSize = c.FileSize,
                    KnowledgeBaseId = c.KnowledgeBaseId,
                    CreateDate = c.CreateDate,
                    LastModifiedDate = c.LastModifiedDate,
                }).ToListAsync(); ;

            return Ok();
        }
        [HttpDelete("{knowledgeBaseId}/attachments/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(int knowledgeBaseId, int attachmentId)
        {
            var attachment = await _context.Attachments.FindAsync(attachmentId);
            if (attachment == null)
                return NotFound();

            _context.Attachments.Remove(attachment);

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