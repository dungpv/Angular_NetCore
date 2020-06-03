using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.ViewModels.Contents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public partial class KnowledgeBasesController
    {

        #region Votes
        [HttpGet("{knowledgeBaseId}/votes")]
        public async Task<IActionResult> GetVotes(int knowledgeBaseId)
        {
            var votes = await _context.Votes.Where(x => x.KnowledgeBaseId == knowledgeBaseId)
                .Select(x => new VoteVm()
                {
                    UserId = x.UserId,
                    KnowledgeBaseId = x.KnowledgeBaseId,
                    CreateDate = x.CreateDate,
                    LastModifiedDate = x.LastModifiedDate
                }).ToListAsync();

            return Ok(votes);
        }
        [HttpPut("{knowledgeBaseId}/votes")]
        public async Task<IActionResult> PostVote(int knowledgeBaseId, [FromBody]VoteCreateRequest request)
        {
            var vote = await _context.Votes.FindAsync(knowledgeBaseId, request.UserId);
            if (vote == null)
                return BadRequest("This user has been voted for this KB");
            vote = new Vote()
            {
                KnowledgeBaseId = request.KnowledgeBaseId,
                UserId = request.UserId,
            };
            _context.Votes.Add(vote);
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase != null)
                return BadRequest();

            knowledgeBase.NumberOfVotes = knowledgeBase.NumberOfVotes.GetValueOrDefault(0) + 1;
            _context.KnowledgeBases.Update(knowledgeBase);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{knowledgeBaseId}/votes/{userId}")]
        public async Task<IActionResult> DeleteVote(int knowledgeBaseId, int userId)
        {
            var vote = await _context.Votes.FindAsync(knowledgeBaseId, userId);
            if (vote == null)
                return NotFound();

            _context.Votes.Remove(vote);
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase != null)
                return BadRequest();

            knowledgeBase.NumberOfVotes = knowledgeBase.NumberOfVotes.GetValueOrDefault(0) - 1;
            _context.KnowledgeBases.Update(knowledgeBase);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var voteVm = new VoteVm()
                {
                    KnowledgeBaseId = vote.KnowledgeBaseId,
                    UserId = vote.UserId,
                    CreateDate = vote.CreateDate,
                    LastModifiedDate = vote.LastModifiedDate,
                };
                return Ok(voteVm);
            }
            return BadRequest();
        }
        #endregion
    }
}