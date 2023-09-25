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
    public class StoreController : ControllerBase, IStoreController
    {
        private readonly posDbContext _context;

        public StoreController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TStore
        [HttpPost("PostTStore")]
        public async Task<ActionResult<TStoreHelper>> PostTStore(TStoreHelper tStoreHelper)
        {
            try
            {
                if (tStoreHelper == null)
                {
                    return BadRequest("Invalid input data.");
                }

                TStore tStore = new TStore();
                tStore.StockStatusId = tStoreHelper.StockStatusId;
                tStore.ProductName = tStoreHelper.ProductName;
                tStore.ProductId = tStoreHelper.ProductId;
                tStore.ExpDate = tStoreHelper.ExpDate;
                tStore.AddedBy = tStoreHelper.AddedBy;
                tStore.PricePerUnit = tStoreHelper.PricePerUnit;
                tStore.DayAdded = tStoreHelper.DayAdded;
                tStore.PricePerUnit = tStoreHelper.PricePerUnit;
                tStore.ManDate = tStoreHelper.ManDate;
                tStore.StockAvailable = tStoreHelper.StockAvailable;



                _context.TStores.Add(tStore);
                await _context.SaveChangesAsync();
                return Ok("Inserted Succesfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TStore
        [HttpGet("GetTStores")]
        public async Task<ActionResult<IEnumerable<TStore>>> GetTStores()
        {
            try
            {
                var stores = await _context.TStores.ToListAsync();
                return Ok(stores);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TStore/5
        [HttpGet("GetTStore/{id}")]
        public async Task<ActionResult<TStore>> GetTStore(int id)
        {
            try
            {
                var tStore = await _context.TStores.FindAsync(id);

                if (tStore == null)
                {
                    return NotFound("Store not found.");
                }

                return Ok(tStore);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TStore/5
        [HttpPut("PutTStore/{id}")]
        public async Task<IActionResult> PutTStore(int id, TStoreHelper tStore)
        {
            try
            {
                if (id != tStore.ProductId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tStore).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TStore/5
        [HttpDelete("DeleteTStore/{id}")]
        public async Task<IActionResult> DeleteTStore(int id)
        {
            try
            {
                var tStore = await _context.TStores.FindAsync(id);
                if (tStore == null)
                {
                    return NotFound("Store not found.");
                }

                _context.TStores.Remove(tStore);
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
