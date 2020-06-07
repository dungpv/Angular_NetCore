﻿using System;
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
using KnowledgeSpace.BackendServer.Authorization;
using KnowledgeSpace.BackendServer.Constants;
using KnowledgeSpace.BackendServer.Helper;
using Microsoft.Extensions.Logging;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class FunctionsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FunctionsController> _logger;
        public FunctionsController(ApplicationDbContext context, ILogger<FunctionsController> logger)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.CREATE)]
        [ApiValidationFilterAttribute]
        public async Task<IActionResult> PostFunction([FromBody]FunctionCreateRequest request)
        {
            _logger.LogInformation("Begin PostFunction API");
            var dbFunction = await _context.Functions.FindAsync(request.Id);
            if (dbFunction == null)
                return BadRequest(new ApiBadRequestResponse($"Function with Id {request.Id} is existed"));

            var function = new Function()
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId,
                SortOrder = request.SortOrder,
                Url = request.Url,
                Icon = request.Icon,
            };
            _context.Functions.Add(function);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                _logger.LogInformation("End PostFunction API - Success");
                return CreatedAtAction(nameof(GetById), new { id = function.Id }, request);
            }
            else
            {
                _logger.LogInformation("End PostFunction API - Failed");
                return BadRequest(new ApiBadRequestResponse("Create function is failed"));
            }
        }
        // URL: GET: http://localhost:5001/api/Functions/{id}
        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(string id)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found function with id {id}"));

            var functionVm = new FunctionVm()
            {
                Id = function.Id,
                Name = function.Name,
                ParentId = function.ParentId,
                SortOrder = function.SortOrder,
                Url = function.Url,
                Icon = function.Icon,
            };
            return Ok(functionVm);
        }
        // URL: GET
        [HttpGet]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
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
                Icon = u.Icon,
            }).ToListAsync();

            return Ok(functionvms);
        }
        // URL: GET: http://localhost:5001/api/Functions/?quer
        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
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
                    Icon = u.Icon,
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
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.UPDATE)]
        public async Task<IActionResult> PutFunction(string id, [FromBody]FunctionCreateRequest request)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found function with id {id}"));

            function.Name = request.Name;
            function.ParentId = request.ParentId;
            function.SortOrder = request.SortOrder;
            function.Url = request.Url;
            function.Icon = request.Icon;

            _context.Functions.Update(function);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse("Update function is failed"));
        }

        // URL: DELETE: http://localhost:5001/api/Functions/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteFunction(string id)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found function with id {id}"));

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
                    Icon = function.Icon,
                };
                return Ok(functionvm);
            }
            return BadRequest(new ApiBadRequestResponse("Delete function is failed"));
        }

        // URL: GET
        [HttpGet("{functioniId}/commands")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
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
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.UPDATE)]
        public async Task<IActionResult> PostCommandToFunction(string functionId, [FromBody] AddCommandToFunctionRequest request)
        {
            var commandInFunction = await _context.CommandInFunctions.FindAsync(request.CommandId, request.FunctionId);
            if (commandInFunction != null)
                return BadRequest(new ApiBadRequestResponse($"This command has been added to function"));

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
                return BadRequest(new ApiBadRequestResponse("Post command to function is failed"));
            }
        }
        [HttpDelete("{functionId}/commands/{commandId}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteCommandToFunction(string functionId, string commandId)
        {
            var commandInFunction = await _context.CommandInFunctions.FindAsync(functionId, commandId);
            if (commandInFunction == null)
                return BadRequest(new ApiBadRequestResponse($"This command is not existed in function"));

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
                return BadRequest(new ApiBadRequestResponse("Delete command in function is failed"));
            }
        }

    }
}