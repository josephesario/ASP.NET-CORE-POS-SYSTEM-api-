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
    public class StockController : ControllerBase, IStockController
    {
        private readonly posDbContext _context;

        public StockController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TStock
        [HttpPost("PostTStock")]
        public async Task<ActionResult<TStockHelper>> PostTStock(TStockHelper tStockHelper)
        {
            try
            {
                if (tStockHelper == null)
                {
                    return Ok("No Data To Insert");
                }

                TStock tStock = new TStock();
                tStock.ProductName = tStockHelper.ProductName;
                tStock.ProductId = tStockHelper.ProductId;
                tStock.ClientId = tStockHelper.ClientId;
                tStock.SupplierId = tStockHelper.SupplierId;
                tStock.CategoryId = tStockHelper.CategoryId;
                tStock.DayAdded = tStockHelper.DayAdded;
                tStock.ExpDate = tStockHelper.ExpDate;
                tStock.ManDate = tStockHelper.ManDate;
                tStock.PricePerUnit = tStockHelper.PricePerUnit;

                _context.TStocks.Add(tStock);
                await _context.SaveChangesAsync();
                return Ok("Inserted Successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TStock
        [HttpGet("GetTStocks")]
        public async Task<ActionResult<IEnumerable<TStock>>> GetTStocks()
        {
            try
            {
                var stocks = await _context.TStocks.ToListAsync();
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TStock/5
        [HttpGet("GetTStock/{id}")]
        public async Task<ActionResult<TStock>> GetTStock(int id)
        {
            try
            {
                var tStock = await _context.TStocks.FindAsync(id);

                if (tStock == null)
                {
                    return NotFound("Stock not found.");
                }

                return Ok(tStock);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TStock/5
        [HttpPut("PutTStock/{id}")]
        public async Task<IActionResult> PutTStock(int id, TStockHelper tStock)
        {
            try
            {
                if (id != tStock.ProductId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tStock).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TStock/5
        [HttpDelete("DeleteTStock/{id}")]
        public async Task<IActionResult> DeleteTStock(int id)
        {
            try
            {
                var tStock = await _context.TStocks.FindAsync(id);
                if (tStock == null)
                {
                    return NotFound("Stock not found.");
                }

                _context.TStocks.Remove(tStock);
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
