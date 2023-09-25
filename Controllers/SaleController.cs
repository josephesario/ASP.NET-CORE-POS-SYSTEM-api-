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
    public class SaleController : ControllerBase, ISaleController
    {
        private readonly posDbContext _context;

        public SaleController(posDbContext context)
        {
            _context = context;
        }

        // GET: api/TSale
        [HttpGet("GetTSales")]
        public async Task<ActionResult<IEnumerable<TSale>>> GetTSales()
        {
            try
            {
                var sales = await _context.TSales.ToListAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TSale/5
        [HttpGet("GetTSale/{id}")]
        public async Task<ActionResult<TSale>> GetTSale(int id)
        {
            try
            {
                // Since it's a keyless entity, you may need to adjust this part
                var tSale = await _context.TSales.FirstOrDefaultAsync(temp => temp.ProductId == id);

                if (tSale == null)
                {
                    return NotFound("Sale not found.");
                }

                return Ok(tSale);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TSale/5
        [HttpPost("AddTSale")]
        public async Task<ActionResult> AddTSale(TSaleHelper tSaleHelper)
        {
            try
            {
                if(tSaleHelper == null)
                {
                    return NotFound();
                }

                TSale tSale = new TSale();
                tSale.ProductName = tSaleHelper.ProductName;
                tSale.ProductId = tSaleHelper.ProductId;
                tSale.ProductQuantity = tSaleHelper.ProductQuantity;
                tSale.TotalPrice = tSaleHelper.TotalPrice;
                tSale.UserDetailsId = tSaleHelper.UserDetailsId;

                _context.TSales.Add(tSale);
                await _context.SaveChangesAsync();

                return Ok("Inserted Succesfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TSale/5
        [HttpPut("PutTSale/{id}")]
        public async Task<IActionResult> PutTSale(int id, TSaleHelper tSale)
        {
            try
            {
                // Since it's a keyless entity, you may need to adjust this part
                var existingSale = await _context.TSales.FirstOrDefaultAsync(temp => temp.ProductId == id);

                if (existingSale == null)
                {
                    return NotFound("Sale not found.");
                }

                // Update properties of the existing sale with values from tSale
                existingSale.ProductName = tSale.ProductName;
                existingSale.TotalPrice = tSale.TotalPrice;
                existingSale.ProductQuantity = tSale.ProductQuantity;

                // Save the changes to the database
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TSale/5
        [HttpDelete("DeleteTSale/{id}")]
        public async Task<IActionResult> DeleteTSale(int id)
        {
            try
            {
                // Since it's a keyless entity, you may need to adjust this part
                var tSale = await _context.TSales.FirstOrDefaultAsync(temp => temp.ProductId == id);

                if (tSale == null)
                {
                    return NotFound("Sale not found.");
                }

                _context.TSales.Remove(tSale);
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
