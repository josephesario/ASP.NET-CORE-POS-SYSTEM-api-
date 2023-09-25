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
    public class UserTypeStatusController : ControllerBase, IUserTypeStatusController
    {
        private readonly posDbContext _context;

        public UserTypeStatusController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TUserTypeStatus
        [AllowAnonymous]
        [HttpPost("PostTUserTypeStatus")]
        public async Task<ActionResult<TUserTypeStatus>> PostTUserTypeStatus(TUserTypeStatusHelper tUserTypeStatusHelper)
        {
            try
            {

                if (tUserTypeStatusHelper == null)
                {
                    return BadRequest("Invalid input data.");
                }

                var checkTrueOrFalse =  _context.TClients.Any(e => e.ClientId.Equals(tUserTypeStatusHelper.ClientId));
                
                if (checkTrueOrFalse) {
                    
                    TUserTypeStatus tUserTypeStatus = new TUserTypeStatus();
                    tUserTypeStatus.UserTypeStatusId = tUserTypeStatusHelper.UserTypeStatusId;
                    tUserTypeStatus.UserTypeStatusName = tUserTypeStatusHelper.UserTypeStatusName;
                    tUserTypeStatus.ClientId = tUserTypeStatusHelper.ClientId;
                    tUserTypeStatus.CanAdd = tUserTypeStatusHelper.CanAdd;
                    tUserTypeStatus.CanDelete = tUserTypeStatusHelper.CanDelete;
                    tUserTypeStatus.CanEdit = tUserTypeStatusHelper.CanEdit;
                    tUserTypeStatus.CanView = tUserTypeStatusHelper.CanView;
                    tUserTypeStatus.ChangedBy = tUserTypeStatusHelper.ChangedBy;
                    tUserTypeStatus.DayChanged = tUserTypeStatusHelper.DayChanged;


                    if (tUserTypeStatus == null)
                    {
                        return BadRequest("Invalid input data.");
                    }

                    _context.TUserTypeStatuses.Add(tUserTypeStatus);
                    await _context.SaveChangesAsync();
                    return Ok("Inserted Successfully");
                }
                else
                {
                    return NotFound("Client Not Found.");
                }

            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserTypeStatus
        [HttpGet("GetTUserTypeStatuses")]
        public async Task<ActionResult<IEnumerable<TUserTypeStatus>>> GetTUserTypeStatuses()
        {
            try
            {
                var userTypeStatuses = await _context.TUserTypeStatuses.ToListAsync();
                return Ok(userTypeStatuses);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserTypeStatus/5
        [HttpGet("GetTUserTypeStatus/{id}")]
        public async Task<ActionResult<TUserTypeStatus>> GetTUserTypeStatus(int id)
        {
            try
            {
                var tUserTypeStatus = await _context.TUserTypeStatuses.FindAsync(id);

                if (tUserTypeStatus == null)
                {
                    return NotFound("User type status not found.");
                }

                return Ok(tUserTypeStatus);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TUserTypeStatus/5
        [HttpPut("PutTUserTypeStatus/{id}")]
        public async Task<IActionResult> PutTUserTypeStatus(int id, TUserTypeStatusHelper tUserTypeStatus)
        {
            try
            {
                if (id != tUserTypeStatus.UserTypeStatusId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tUserTypeStatus).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TUserTypeStatus/5
        [HttpDelete("DeleteTUserTypeStatus/{id}")]
        public async Task<IActionResult> DeleteTUserTypeStatus(int id)
        {
            try
            {
                var tUserTypeStatus = await _context.TUserTypeStatuses.FindAsync(id);
                if (tUserTypeStatus == null)
                {
                    return NotFound("User type status not found.");
                }

                _context.TUserTypeStatuses.Remove(tUserTypeStatus);
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
