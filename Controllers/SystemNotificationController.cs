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
    public class SystemNotificationController : ControllerBase, ISystemNotificationController
    {
        private readonly posDbContext _context;

        public SystemNotificationController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TSystemNotification
        [HttpPost("PostTSystemNotification")]
        public async Task<ActionResult<TSystemNotificationHelper>> PostTSystemNotification(TSystemNotificationHelper tSystemNotificationHelper)
        {
            try
            {
                if (tSystemNotificationHelper == null)
                {
                    return Ok("No Data Provided");
                }

                TSystemNotification tSystemNotification = new TSystemNotification();
                tSystemNotification.UserDetailsId = tSystemNotificationHelper.UserDetailsId;
                tSystemNotification.MessageId = tSystemNotificationHelper.MessageId;
                tSystemNotification.DayAdded = tSystemNotificationHelper.DayAdded;

                _context.TSystemNotifications.Add(tSystemNotification);

                await _context.SaveChangesAsync();
                return Ok("Inserted Successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TSystemNotification
        [HttpGet("GetTSystemNotifications")]
        public async Task<ActionResult<IEnumerable<TSystemNotification>>> GetTSystemNotifications()
        {
            try
            {
                var notifications = await _context.TSystemNotifications.ToListAsync();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TSystemNotification/5
        [HttpGet("GetTSystemNotification/{id}")]
        public async Task<ActionResult<TSystemNotification>> GetTSystemNotification(int id)
        {
            try
            {
                var tSystemNotification = await _context.TSystemNotifications.FindAsync(id);

                if (tSystemNotification == null)
                {
                    return NotFound("Notification not found.");
                }

                return Ok(tSystemNotification);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TSystemNotification/5
        [HttpPut("PutTSystemNotification/{id}")]
        public async Task<IActionResult> PutTSystemNotification(int id, TSystemNotificationHelper tSystemNotification)
        {
            try
            {
                if (id != tSystemNotification.MessageId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tSystemNotification).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TSystemNotification/5
        [HttpDelete("DeleteTSystemNotification/{id}")]
        public async Task<IActionResult> DeleteTSystemNotification(int id)
        {
            try
            {
                var tSystemNotification = await _context.TSystemNotifications.FindAsync(id);
                if (tSystemNotification == null)
                {
                    return NotFound("Notification not found.");
                }

                _context.TSystemNotifications.Remove(tSystemNotification);
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
