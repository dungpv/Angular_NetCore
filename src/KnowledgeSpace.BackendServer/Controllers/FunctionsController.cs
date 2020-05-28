using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class FunctionsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public FunctionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> PostFunction([FromBody]FunctionCreateRequest request)
        {
            var dbFunction = await _context.Functions.FindAsync(request.Id);
            if (dbFunction == null)
                return BadRequest($"Function with Id {request.Id} is existed");

            var function = new Function()
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId,
                SortOrder = request.SortOrder,
                Url = request.Url,
            };
            _context.Functions.Add(function);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = function.Id }, request);
            }
            else
            {
                return BadRequest();
            }
        }
        // URL: GET: http://localhost:5001/api/Functions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
                return NotFound();

            var functionVm = new FunctionVm()
            {
                Id = function.Id,
                Name = function.Name,
                ParentId = function.ParentId,
                SortOrder = function.SortOrder,
                Url = function.Url,
            };
            return Ok(functionVm);
        }
        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetFunctions()
        {
            var functions = _context.Functions;

            var functionvms = await functions.Select(u => new FunctionVm()
            {
                Id = u.Id,
                Name = u.Name,
                ParentId = u.ParentId,
                SortOrder = u.SortOrder,
                Url = u.Url,
            }).ToListAsync();

            return Ok(functionvms);
        }
        // URL: GET: http://localhost:5001/api/Functions/?quer
        [HttpGet("filter")]
        public async Task<IActionResult> GetFunctionsPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Functions.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter)
                || x.Id.Contains(filter)
                || x.Url.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new FunctionVm()
                {
                    Id = u.Id,
                    Name = u.Name,
                    ParentId = u.ParentId,
                    SortOrder = u.SortOrder,
                    Url = u.Url,
                })
                .ToListAsync();

            var pagination = new Pagination<FunctionVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }
        // URL: PUT: http://localhost:5001/api/Functions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFunction(string id, [FromBody]FunctionCreateRequest request)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
                return NotFound();

            function.Name = request.Name;
            function.ParentId = request.ParentId;
            function.SortOrder = request.SortOrder;
            function.Url = request.Url;

            _context.Functions.Update(function);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        // URL: DELETE: http://localhost:5001/api/Functions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFunction(string id)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
                return NotFound();

            _context.Functions.Remove(function);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var functionvm = new FunctionVm()
                {
                    Id = function.Id,
                    Name = function.Name,
                    ParentId = function.ParentId,
                    SortOrder = function.SortOrder,
                    Url = function.Url,
                };
                return Ok(functionvm);
            }
            return BadRequest();
        }

        // URL: GET
        [HttpGet("{functioniId}/commands")]
        public async Task<IActionResult> GetCommandsInFunction(string functionId)
        {
            var query = from a in _context.Commands
                        join cif in _context.CommandInFunctions on a.Id equals cif.CommandId into result1
                        from commandInFunction in result1.DefaultIfEmpty()
                        join f in _context.Functions on commandInFunction.FunctionId equals f.Id into result2
                        from function in result2.DefaultIfEmpty()
                        select new
                        {
                            a.Id,
                            a.Name,
                            commandInFunction.FunctionId
                        };

            query = query.Where(x => x.FunctionId == functionId);

            var data = await query.Select(x => new CommandVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return Ok(data);
        }
        [HttpPost("{functionId}/commands")]
        public async Task<IActionResult> PostCommandToFunction(string functionId, [FromBody] AddCommandToFunctionRequest request)
        {
            var commandInFunction = await _context.CommandInFunctions.FindAsync(request.CommandId, request.FunctionId);
            if (commandInFunction != null)
                return BadRequest($"This command has been added to function");

            var entity = new CommandInFunction()
            {
                CommandId = request.CommandId,
                FunctionId = request.FunctionId,
            };
            _context.CommandInFunctions.Add(entity);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { commandId = entity.CommandId, functionId = entity.FunctionId }, request);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{functionId}/commands/{commandId}")]
        public async Task<IActionResult> DeleteCommandToFunction(string functionId, string commandId)
        {
            var commandInFunction = await _context.CommandInFunctions.FindAsync(functionId, commandId);
            if (commandInFunction == null)
                return BadRequest($"This command is not existed in function");

            var entity = new CommandInFunction()
            {
                CommandId = commandId,
                FunctionId = functionId,
            };
            _context.CommandInFunctions.Remove(entity);
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

    }
}