using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace POS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationDetailController : ControllerBase, IUserRegistrationDetailController
    {
        private readonly posDbContext _context;

        public UserRegistrationDetailController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/UserRegistrationDetail
        [AllowAnonymous]
        [HttpPost("PostTUserRegistrationDetail")]
        public async Task<ActionResult> PostTUserRegistrationDetail(TUserRegistrationDetailHelper userRegistrationDetailHelper)
        {
            try
            {


                TUserRegistrationDetail userRegistrationDetail = new TUserRegistrationDetail();
                //userRegistrationDetail.RegistrationId = userRegistrationDetailHelper.RegistrationId;
                userRegistrationDetail.FirstName = userRegistrationDetailHelper.FirstName;
                userRegistrationDetail.LastName = userRegistrationDetailHelper.LastName;
                userRegistrationDetail.Identification = userRegistrationDetailHelper.Identification;
                userRegistrationDetail.Country = userRegistrationDetailHelper.Country;
                userRegistrationDetail.Region = userRegistrationDetailHelper.Region;
                userRegistrationDetail.ClientPhoneNumber = userRegistrationDetailHelper.ClientPhoneNumber;
                userRegistrationDetail.ClientEmail = userRegistrationDetailHelper.ClientEmail;
                userRegistrationDetail.DayAdded = userRegistrationDetailHelper.DayAdded;
                userRegistrationDetail.AddedBy = userRegistrationDetailHelper.AddedBy;
                userRegistrationDetail.ClientId = userRegistrationDetailHelper.ClientId;



                _context.TUserRegistrationDetails.Add(userRegistrationDetail);
                await _context.SaveChangesAsync();

                return Ok("User Inserted Succesfully");

            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }


        // GET: api/UserRegistrationDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TUserRegistrationDetail>>> GetTUserRegistrationDetails()
        {
            return await _context.TUserRegistrationDetails.ToListAsync();
        }

        // GET: api/UserRegistrationDetail/5
        [HttpGet("UserRegistration/{id}")]
        public async Task<ActionResult<TUserRegistrationDetail>> GetTUserRegistrationDetail(int id)
        {
            var userRegistrationDetail = await _context.TUserRegistrationDetails.FindAsync(id);

            if (userRegistrationDetail == null)
            {
                return NotFound();
            }

            return userRegistrationDetail;
        }


        // PUT: api/UserRegistrationDetail/5
        [HttpPut("PutTUserRegistrationDetail/{id}")]
        public async Task<IActionResult> PutTUserRegistrationDetail(int id, TUserRegistrationDetail userRegistrationDetail)
        {
            if (id != userRegistrationDetail.RegistrationId)
            {
                return BadRequest();
            }

            _context.Entry(userRegistrationDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TUserRegistrationDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/UserRegistrationDetail/5
        [HttpDelete("DeleteTUserRegistrationDetail/{id}")]
        public async Task<IActionResult> DeleteTUserRegistrationDetail(int id)
        {
            var userRegistrationDetail = await _context.TUserRegistrationDetails.FindAsync(id);
            if (userRegistrationDetail == null)
            {
                return NotFound();
            }

            _context.TUserRegistrationDetails.Remove(userRegistrationDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TUserRegistrationDetailExists(int id)
        {
            return _context.TUserRegistrationDetails.Any(e => e.RegistrationId == id);
        }
    }
}
