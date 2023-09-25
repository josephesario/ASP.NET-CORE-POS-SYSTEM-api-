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
    public class UserTypeController : ControllerBase, IUserTypeController
    {
        private readonly posDbContext _context;

        public UserTypeController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TUserType
        [AllowAnonymous]
        [HttpPost("PostTUserType")]
        public async Task<ActionResult<TUserTypeHelper>> PostTUserType(TUserTypeHelper tUserType)
        {
            try
            {
                if (tUserType == null)
                {
                    return BadRequest("Invalid input data.");
                }

                TUserType userType = new();

                userType.ClientId = tUserType.ClientId;
                userType.AddedBy = tUserType.AddedBy;
                userType.ClientTypeName = tUserType.ClientTypeName;
                userType.DayAdded = tUserType.DayAdded;
                userType.UserTypeStatusId = tUserType.UserTypeStatusId;

                _context.TUserTypes.Add(userType);
                await _context.SaveChangesAsync();
                return Ok("Inserted Successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserType
        [AllowAnonymous]
        [HttpGet("GetTUserTypes")]
        public async Task<ActionResult<IEnumerable<TUserType>>> GetTUserTypes()
        {
            try
            {
                var userTypes = await _context.TUserTypes.ToListAsync();
                return Ok(userTypes);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserType/5
        [HttpGet("GetTUserType/{id}")]
        public async Task<ActionResult<TUserType>> GetTUserType(int id)
        {
            try
            {
                var tUserType = await _context.TUserTypes.FindAsync(id);

                if (tUserType == null)
                {
                    return NotFound("User type not found.");
                }

                return Ok(tUserType);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TUserType/5
        [HttpPut("PutTUserType/{id}")]
        public async Task<IActionResult> PutTUserType(int id, TUserTypeHelper tUserType)
        {
            try
            {
                var output = _context.TUserTypes.Any(e=>e.UserTypeId == id);

                if (output)
                {
                    return NotFound("User Type Not Found");
                }

                _context.Entry(tUserType).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TUserType/5
        [HttpDelete("DeleteTUserType/{id}")]
        public async Task<IActionResult> DeleteTUserType(int id)
        {
            try
            {
                var tUserType = await _context.TUserTypes.FindAsync(id);

                if (tUserType == null)
                {
                    return NotFound("User type not found.");
                }

                _context.TUserTypes.Remove(tUserType);
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
