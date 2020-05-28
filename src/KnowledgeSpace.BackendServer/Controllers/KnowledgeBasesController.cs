using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Services;
using KnowledgeSpace.ViewModels.Contents;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class KnowledgeBasesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly SequenceService _sequenceService;
        public KnowledgeBasesController(ApplicationDbContext context, SequenceService sequenceService)
        {
            _context = context;
            _sequenceService = sequenceService;
        }
        #region Knowledge base
        [HttpPost]
        public async Task<IActionResult> PostKnowledgeBase([FromBody]KnowledgeBaseCreateRequest request)
        {
            var knowledgeBase = new KnowledgeBase()
            {
                CategoryId = request.CategoryId,
                Title = request.Title,
                SeoAlias = request.SeoAlias,
                Description = request.Description,
                Environment = request.Environment,
                Problem = request.Problem,
                StepToReproduce = request.StepToReproduce,
                ErrorMessage = request.ErrorMessage,
                Workaround = request.Workaround,
                Note = request.Note,
                Labels = request.Labels,
            };
            knowledgeBase.Id = await _sequenceService.GetKnowledgeBaseNewId();
            _context.KnowledgeBases.Add(knowledgeBase);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = knowledgeBase.Id }, request);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetKnowledgeBases()
        {
            var knowledgeBases = _context.KnowledgeBases;

            var knowledgeBasevms = await knowledgeBases.Select(u => new KnowledgeBaseQuickVm()
            {
                Id = u.Id,
                CategoryId = u.CategoryId,
                CategoryName = u.Title,
                SeoAlias = u.SeoAlias,
                Description = u.Description,
            }).ToListAsync();

            return Ok(knowledgeBasevms);
        }
        // URL: GET: http://localhost:5001/api/KnowledgeBases/?quer
        [HttpGet("filter")]
        public async Task<IActionResult> GetKnowledgeBasesPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.KnowledgeBases.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Title.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new KnowledgeBaseQuickVm()
                {
                    Id = u.Id,
                    CategoryId = u.CategoryId,
                    CategoryName = u.Title,
                    SeoAlias = u.SeoAlias,
                    Description = u.Description,
                })
                .ToListAsync();

            var pagination = new Pagination<KnowledgeBaseQuickVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(id);
            if (knowledgeBase == null)
                return NotFound();

            var knowledgeBaseVm = CreateKnowledgeBaseVm(knowledgeBase);
            return Ok(knowledgeBaseVm);
        }
        // URL: PUT: http://localhost:5001/api/KnowledgeBases/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnowledgeBase(int id, [FromBody]KnowledgeBaseCreateRequest request)
        {
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(id);
            if (knowledgeBase == null)
                return NotFound();

            knowledgeBase.CategoryId = request.CategoryId;
            knowledgeBase.Title = request.Title;
            knowledgeBase.SeoAlias = request.SeoAlias;
            knowledgeBase.Description = request.Description;
            knowledgeBase.Environment = request.Environment;
            knowledgeBase.Problem = request.Problem;
            knowledgeBase.StepToReproduce = request.StepToReproduce;
            knowledgeBase.ErrorMessage = request.ErrorMessage;
            knowledgeBase.Workaround = request.Workaround;
            knowledgeBase.Note = request.Note;
            knowledgeBase.Labels = request.Labels;

            _context.KnowledgeBases.Update(knowledgeBase);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        // URL: DELETE: http://localhost:5001/api/KnowledgeBases/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnowledgeBase(int id)
        {
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(id);
            if (knowledgeBase == null)
                return NotFound();

            _context.KnowledgeBases.Remove(knowledgeBase);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var knowledgeBaseVm = CreateKnowledgeBaseVm(knowledgeBase);
                return Ok(knowledgeBaseVm);
            }
            return BadRequest();
        }

        private static KnowledgeBaseVm CreateKnowledgeBaseVm(KnowledgeBase knowledgeBase)
        {
            return new KnowledgeBaseVm()
            {
                Id = knowledgeBase.Id,
                CategoryId = knowledgeBase.CategoryId,
                Title = knowledgeBase.Title,
                SeoAlias = knowledgeBase.SeoAlias,
                Description = knowledgeBase.Description,
                Environment = knowledgeBase.Environment,
                Problem = knowledgeBase.Problem,
                StepToReproduce = knowledgeBase.StepToReproduce,
                ErrorMessage = knowledgeBase.ErrorMessage,
                Workaround = knowledgeBase.Workaround,
                Note = knowledgeBase.Note,
                OwnerUserId = knowledgeBase.OwnerUserId,
                Labels = knowledgeBase.Labels,
                CreateDate = knowledgeBase.CreateDate,
                LastModifiedDate = knowledgeBase.LastModifiedDate,
                NumberOfComments = knowledgeBase.NumberOfComments,
                NumberOfVotes = knowledgeBase.NumberOfVotes,
                NumberOfReports = knowledgeBase.NumberOfReports,
            };
        }
        #endregion
        #region Comment
        [HttpGet("{knowledgeBaseId}/comments/filter")]
        public async Task<IActionResult> GetCommentPaging(int knowledgeBaseId, string filter, int pageIndex, int pageSize)
        {
            var query = _context.Comments.Where(x => x.KnowledgeBaseId == knowledgeBaseId).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Content.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CommentVm()
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreateDate = c.CreateDate,
                    KnowledgeBaseId = c.KnowledgeBaseId,
                    LastModifiedDate = c.LastModifiedDate,
                    OwnerUserId = c.OwnwerUserId,
                })
                .ToListAsync();

            var pagination = new Pagination<CommentVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }
        [HttpGet("{knowledgeBaseId}/comments/{commentId}")]
        public async Task<IActionResult> GetCommentDetail(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                return NotFound();

            var commentVm = new CommentVm() {
                Id = comment.Id,
                Content = comment.Content,
                CreateDate = comment.CreateDate,
                KnowledgeBaseId = comment.KnowledgeBaseId,
                LastModifiedDate = comment.LastModifiedDate,
                OwnerUserId = comment.OwnwerUserId,
            };
            return Ok(commentVm);
        }

        [HttpPost("{knowledgeBaseId}/comments/{commentId}")]
        public async Task<IActionResult> GetCommentDetail(int knowledgeBaseId, [FromBody]CommentCreateRequest request)
        {
            var comment = new Comment()
            {
                Content = request.Content,
                KnowledgeBaseId = request.KnowledgeBaseId,
                OwnwerUserId = string.Empty,
            };
            _context.Comments.Add(comment);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetCommentDetail), new { id = knowledgeBaseId, commentId = comment.Id }, request);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{knowledgeBaseId}/comments/{commentId}")]
        public async Task<IActionResult> PutComment(int commentId, [FromBody]CommentCreateRequest request)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                return NotFound();
            if (comment.OwnwerUserId != User.Identity.Name)
                return Forbid();

            comment.Content = request.Content;
            _context.Comments.Update(comment);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{knowledgeBaseId}/comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
                return NotFound();

            _context.Comments.Remove(comment);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var commentVm = new CommentVm()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreateDate = comment.CreateDate,
                    KnowledgeBaseId = comment.KnowledgeBaseId,
                    LastModifiedDate = comment.LastModifiedDate,
                    OwnerUserId = comment.OwnwerUserId
                };
                return Ok(commentVm);
            }
            return BadRequest();
        }
        #endregion
    }
}