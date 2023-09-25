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
    public class StockStatusController : ControllerBase, IStockStatusController
    {
        private readonly posDbContext _context;

        public StockStatusController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TStockStatus
        [HttpPost("PostTStockStatus")]
        public async Task<ActionResult<TStockStatusHelper>> PostTStockStatus(TStockStatusHelper tStockStatusHelper)
        {
            try
            {
                if (tStockStatusHelper == null)
                {
                    return Ok("No Data To Insert");
                }

                TStockStatus tStockStatus = new TStockStatus();
                tStockStatus.StockStatusId = tStockStatusHelper.StockStatusId;
                tStockStatus.UserDetailsId = tStockStatusHelper.UserDetailsId;
                tStockStatus.ProductId = tStockStatusHelper.ProductId;
                tStockStatus.IsActive = tStockStatusHelper.IsActive;

                _context.TStockStatuses.Add(tStockStatus);
                await _context.SaveChangesAsync();
                return Ok("Inserted Succesfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TStockStatus
        [HttpGet("GetTStockStatuses")]
        public async Task<ActionResult<IEnumerable<TStockStatus>>> GetTStockStatuses()
        {
            try
            {
                var statuses = await _context.TStockStatuses.ToListAsync();
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TStockStatus/5
        [HttpGet("GetTStockStatus/{id}")]
        public async Task<ActionResult<TStockStatus>> GetTStockStatus(int id)
        {
            try
            {
                var tStockStatus = await _context.TStockStatuses.FindAsync(id);

                if (tStockStatus == null)
                {
                    return NotFound("Stock status not found.");
                }

                return Ok(tStockStatus);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TStockStatus/5
        [HttpPut("PutTStockStatus/{id}")]
        public async Task<IActionResult> PutTStockStatus(int id, TStockStatusHelper tStockStatus)
        {
            try
            {
                if (id != tStockStatus.StockStatusId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tStockStatus).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TStockStatus/5
        [HttpDelete("DeleteTStockStatus/{id}")]
        public async Task<IActionResult> DeleteTStockStatus(int id)
        {
            try
            {
                var tStockStatus = await _context.TStockStatuses.FindAsync(id);
                if (tStockStatus == null)
                {
                    return NotFound("Stock status not found.");
                }

                _context.TStockStatuses.Remove(tStockStatus);
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
