using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Authorization;
using KnowledgeSpace.BackendServer.Constants;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Helper;
using KnowledgeSpace.ViewModels.Contents;
using KnowledgeSpace.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KnowledgeSpace.BackendServer.Services;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;
        public CategoriesController(ApplicationDbContext context,
            ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        [HttpPost]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.CREATE)]
        public async Task<IActionResult> PostCategory([FromBody]CategoryCreateRequest request)
        {
            var category = new Category()
            {
                Name = request.Name,
                ParentId = request.ParentId,
                SortOrder = request.SortOrder,
                SeoAlias = request.SeoAlias,
                SeoDescription = request.SeoDescription,
            };
            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                await _cacheService.RemoveAsync("Categories");
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, request);
            }
            else
            {
                return BadRequest();
            }
        }
        // URL: GET: http://localhost:5001/api/Category/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new ApiNotFoundResponse($"Category with id: {id} is not found"));

            CategoryVm categoryVm = CreateCategoryVm(category);

            return Ok(categoryVm);
        }
        // URL: GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var cachedData = await _cacheService.GetAsync<List<CategoryVm>>("Categories");
            if (cachedData == null)
            {
                var categorys = await _context.Categories.ToListAsync();

                var categoryVms = categorys.Select(c => CreateCategoryVm(c)).ToList();
                await _cacheService.SetAsync("Categories", categoryVms);
                cachedData = categoryVms;
            }

            return Ok(cachedData);
        }
        // URL: GET: http://localhost:5001/api/Category/?quer
        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.VIEW)]
        public async Task<IActionResult> GetCategoryPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var data = items.Select(c => CreateCategoryVm(c)).ToList();

            var pagination = new Pagination<CategoryVm>
            {
                Items = data,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }
        // URL: PUT: http://localhost:5001/api/Category/{id}
        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.UPDATE)]
        public async Task<IActionResult> PutCategory(int id, [FromBody]CategoryCreateRequest request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            if (id == request.ParentId)
                return BadRequest("Category cannot be a child itself.");

            category.Name = request.Name;
            category.ParentId = request.ParentId;
            category.SortOrder = request.SortOrder;
            category.SeoAlias = request.SeoAlias;
            category.SeoDescription = request.SeoDescription;

            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                await _cacheService.RemoveAsync("Categories");
                return NoContent();
            }
            return BadRequest();
        }

        // URL: DELETE: http://localhost:5001/api/Category/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                await _cacheService.RemoveAsync("Categories");

                CategoryVm categoryvm = CreateCategoryVm(category);
                return Ok(categoryvm);
            }
            return BadRequest();
        }

        private static CategoryVm CreateCategoryVm(Category category)
        {
            return new CategoryVm()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                SortOrder = category.SortOrder,
                SeoAlias = category.SeoAlias,
                SeoDescription = category.SeoDescription,
            };
        }
    }
}