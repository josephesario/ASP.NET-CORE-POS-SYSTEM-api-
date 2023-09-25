using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POS.Models;
using Microsoft.AspNetCore.Authorization;


namespace POSApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TProductCategoryController : ControllerBase, ITProductCategoryController
    {
        private readonly posDbContext _context;

        public TProductCategoryController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TProductCategory
        [HttpPost("PostTProductCategory")]
        public async Task<ActionResult<TProductCategoryHelper>> PostTProductCategory(TProductCategoryHelper tProductCategoryHelper)
        {
            try
            {
                if (tProductCategoryHelper == null)
                {
                    return Ok("No Data To Insert");
                }

                TProductCategory tProductCategory = new TProductCategory();
                tProductCategory.UserDetailsId = tProductCategoryHelper.UserDetailsId;
                tProductCategory.CategoryId = tProductCategoryHelper.CategoryId;
                tProductCategory.CategoryName = tProductCategoryHelper.CategoryName;
                tProductCategory.AddedBy = tProductCategoryHelper.AddedBy;
                tProductCategory.DayAdded = tProductCategoryHelper.DayAdded;



                _context.TProductCategories.Add(tProductCategory);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTProductCategory", new { id = tProductCategory.CategoryId }, tProductCategory);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TProductCategory
        [HttpGet("GetTProductCategories")]
        public async Task<ActionResult<IEnumerable<TProductCategory>>> GetTProductCategories()
        {
            try
            {
                var categories = await _context.TProductCategories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TProductCategory/5
        [HttpGet("GetTProductCategory/{id}")]
        public async Task<ActionResult<TProductCategory>> GetTProductCategory(int id)
        {
            try
            {
                var tProductCategory = await _context.TProductCategories.FindAsync(id);

                if (tProductCategory == null)
                {
                    return NotFound("Product category not found.");
                }

                return Ok(tProductCategory);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TProductCategory/5
        [HttpPut("PutTProductCategory/{id}")]
        public async Task<IActionResult> PutTProductCategory(int id, TProductCategoryHelper tProductCategory)
        {
            try
            {
                if (id != tProductCategory.CategoryId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tProductCategory).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TProductCategory/5
        [HttpDelete("DeleteTProductCategory/{id}")]
        public async Task<IActionResult> DeleteTProductCategory(int id)
        {
            try
            {
                var tProductCategory = await _context.TProductCategories.FindAsync(id);
                if (tProductCategory == null)
                {
                    return NotFound("Product category not found.");
                }

                _context.TProductCategories.Remove(tProductCategory);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
