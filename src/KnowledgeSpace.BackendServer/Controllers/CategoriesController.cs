using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.ViewModels.Contents;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
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
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, request);
            }
            else
            {
                return BadRequest();
            }
        }
        // URL: GET: http://localhost:5001/api/Category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            var categoryVm = CreateCategoryVm(category);
            return Ok(categoryVm);
        }
        // URL: GET
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoryvms =  categories.Select(c => CreateCategoryVm(c)).ToList();

            return Ok(categoryvms);
        }
        // URL: GET: http://localhost:5001/api/Category/?quer
        [HttpGet("filter")]
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
                return NoContent();
            }
            return BadRequest();
        }

        // URL: DELETE: http://localhost:5001/api/Category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
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