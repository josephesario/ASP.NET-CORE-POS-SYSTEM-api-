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
    public class UserDetailsStatusController : ControllerBase
    {
        private readonly posDbContext _context;

        public UserDetailsStatusController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TUserDetailsStatus
        [AllowAnonymous]
        [HttpPost("PostTUserDetailsStatus")]
        public async Task<ActionResult<TUserDetailsStatusHelper>> PostTUserDetailsStatus(TUserDetailsStatusHelper tUserDetailsStatusHelper)
        {


            try
            {

                TUserDetailsStatus tUserDetailsStatus = new TUserDetailsStatus();
                tUserDetailsStatus.UserDetailsStatusId = tUserDetailsStatusHelper.UserDetailsStatusId;
                tUserDetailsStatus.IsActive = tUserDetailsStatusHelper.IsActive;
                tUserDetailsStatus.DayChanged = tUserDetailsStatusHelper.DayChanged;
                tUserDetailsStatus.ChangedBy = tUserDetailsStatusHelper.ChangedBy;
                tUserDetailsStatus.ClientId = tUserDetailsStatusHelper.ClientId;

                if (tUserDetailsStatus == null)
                {
                    return BadRequest("Invalid input data.");
                }

                _context.TUserDetailsStatuses.Add(tUserDetailsStatus);
                await _context.SaveChangesAsync();
                return Ok("User Status Inserted Succesfully");

            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserDetailsStatus
        [HttpGet("GetTUserDetailsStatuses")]
        public async Task<ActionResult<IEnumerable<TUserDetailsStatusHelper>>> GetTUserDetailsStatuses()
        {
            try
            {
                var userDetailsStatuses = await _context.TUserDetailsStatuses.ToListAsync();
                return Ok(userDetailsStatuses);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserDetailsStatus/5
        [HttpGet("GetTUserDetailsStatus/{id}")]
        public async Task<ActionResult<TUserDetailsStatusHelper>> GetTUserDetailsStatus(int id)
        {
            try
            {
                var tUserDetailsStatus = await _context.TUserDetailsStatuses.FindAsync(id);

                if (tUserDetailsStatus == null)
                {
                    return NotFound("User details status not found.");
                }

                return Ok(tUserDetailsStatus);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TUserDetailsStatus/5
        [HttpPut("PutTUserDetailsStatus/{id}")]
        public async Task<IActionResult> PutTUserDetailsStatus(int id, TUserDetailsStatusHelper tUserDetailsStatus)
        {
            try
            {
                if (id != tUserDetailsStatus.UserDetailsStatusId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tUserDetailsStatus).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TUserDetailsStatus/5
        [HttpDelete("DeleteTUserDetailsStatus/{id}")]
        public async Task<IActionResult> DeleteTUserDetailsStatus(int id)
        {
            try
            {
                var tUserDetailsStatus = await _context.TUserDetailsStatuses.FindAsync(id);
                if (tUserDetailsStatus == null)
                {
                    return NotFound("User details status not found.");
                }

                _context.TUserDetailsStatuses.Remove(tUserDetailsStatus);
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
