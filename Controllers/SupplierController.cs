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
    public class SupplierController : ControllerBase, ISupplierController
    {
        private readonly posDbContext _context;

        public SupplierController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TSupplier
        [HttpPost("PostTSupplier")]
        public async Task<ActionResult<TSupplier>> PostTSupplier(TSupplierHelper tSupplierHelper)
        {
            try
            {
                if (tSupplierHelper == null)
                {
                    return Ok("No Data Provided");
                }

                TSupplier tSupplier = new TSupplier();
                tSupplier.SupplierEmail = tSupplierHelper.SupplierEmail;
                tSupplier.UserDetailsId = tSupplierHelper.UserDetailsId;
                tSupplier.SupplierPhoneNumber = tSupplierHelper.SupplierPhoneNumber;
                tSupplier.SupplierId = tSupplierHelper.SupplierId;
                tSupplier.DayAdded = tSupplierHelper.DayAdded;
                tSupplier.AddedBy = tSupplierHelper.AddedBy;
                tSupplier.SupplierName = tSupplierHelper.SupplierName;


                _context.TSuppliers.Add(tSupplier);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTSupplier", new { id = tSupplier.SupplierId }, tSupplier);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TSupplier
        [HttpGet("GetTSuppliers")]
        public async Task<ActionResult<IEnumerable<TSupplier>>> GetTSuppliers()
        {
            try
            {
                var suppliers = await _context.TSuppliers.ToListAsync();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TSupplier/5
        [HttpGet("GetTSupplier/{id}")]
        public async Task<ActionResult<TSupplier>> GetTSupplier(int id)
        {
            try
            {
                var tSupplier = await _context.TSuppliers.FindAsync(id);

                if (tSupplier == null)
                {
                    return NotFound("Supplier not found.");
                }

                return Ok(tSupplier);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TSupplier/5
        [HttpPut("PutTSupplier/{id}")]
        public async Task<IActionResult> PutTSupplier(int id, TSupplierHelper tSupplier)
        {
            try
            {
                if (id != tSupplier.SupplierId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tSupplier).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TSupplier/5
        [HttpDelete("DeleteTSupplier/{id}")]
        public async Task<IActionResult> DeleteTSupplier(int id)
        {
            try
            {
                var tSupplier = await _context.TSuppliers.FindAsync(id);
                if (tSupplier == null)
                {
                    return NotFound("Supplier not found.");
                }

                _context.TSuppliers.Remove(tSupplier);
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
