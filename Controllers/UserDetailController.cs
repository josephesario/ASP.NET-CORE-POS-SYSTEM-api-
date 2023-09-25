using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using POS.secure;
using BCrypt.Net;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using System.Threading;
using POS.ModelHelper;
using System.Globalization;

namespace POSApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase, IUserDetailController
    {
        private readonly posDbContext _context;
        private readonly IConfiguration Configuration; // Inject IConfiguration if not already available
        LoginRequest loginRequest = new();

        public UserDetailController(posDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // POST: api/TUserDetail
        [AllowAnonymous]
        [HttpPost("PostTUserDetail")]
        public async Task<ActionResult<TUserDetail>> PostTUserDetail(TUserDetailHelper tUserDetail)
        {
            try
            {
                if (tUserDetail == null)
                {
                    return BadRequest("Invalid input data.");
                }

                var checkTrueOrFalse = _context.TUserTypes.Any(e => e.UserTypeId.Equals(tUserDetail.UserTypeId));
                var checkTrueOrFalse2 = _context.TUserTypeStatuses.Any(e => e.UserTypeStatusId.Equals(tUserDetail.UserDetailsStatusId));
                var checkTrueOrFalse3 = _context.TUserRegistrationDetails.Any(e => e.RegistrationId.Equals(tUserDetail.RegistrationId));
                var vPassword = HashPassword(tUserDetail.Password);

                if (checkTrueOrFalse && checkTrueOrFalse2 && checkTrueOrFalse3)
                {

                    TUserDetail tUserDetailtoDb = new();
                    tUserDetailtoDb.AddedBy = tUserDetail.AddedBy;
                    tUserDetailtoDb.DayAdded = tUserDetail.DayAdded;
                    tUserDetailtoDb.Email = tUserDetail.Email;
                    tUserDetailtoDb.Password = vPassword;
                    tUserDetailtoDb.UserDetailsId = tUserDetail.UserDetailsId;
                    tUserDetailtoDb.UserTypeId = tUserDetail.UserTypeId;
                    tUserDetailtoDb.RegistrationId = tUserDetail.RegistrationId;
                    tUserDetailtoDb.UserName = tUserDetail.UserName;
                    tUserDetailtoDb.UserDetailsStatusId = tUserDetail.UserDetailsStatusId;

                    _context.TUserDetails.Add(tUserDetailtoDb);
                    await _context.SaveChangesAsync();
                    return Ok("Inserted Successfully");
                }
                else
                {
                    return NotFound("Please Provide All Required Data");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserDetail
        [HttpGet("GetTUserDetails")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TUserDetail>>> GetTUserDetails()
        {
            try
            {
                var userDetails = await _context.TUserDetails.ToListAsync();
                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TUserDetail/5
        [HttpGet("GetTUserDetail/{id}")]
        public async Task<ActionResult<TUserDetail>> GetTUserDetail(int id)
        {
            try
            {
                var tUserDetail = await _context.TUserDetails.FindAsync(id);

                if (tUserDetail == null)
                {
                    return NotFound("User detail not found.");
                }

                return Ok(tUserDetail);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TUserDetail/5
        [HttpPut("PutTUserDetail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutTUserDetail(int id, TUserDetailHelper tUserDetail)
        {
            try
            {
                if (id != tUserDetail.UserDetailsId)
                {
                    return BadRequest("Invalid ID.");
                }

                var existingEntity = _context.TUserDetails.FirstOrDefault(e => e.UserDetailsId == id);

                if (existingEntity == null)
                {
                    return NotFound("User not found.");
                }

                // Update the properties of the existing entity
                existingEntity.UserName = tUserDetail.UserName;
                existingEntity.Email = tUserDetail.Email;

                var vPassword = HashPassword(tUserDetail.Password);
                if (existingEntity.Password != vPassword)
                {
                    existingEntity.Password = vPassword;
                }

                existingEntity.RegistrationId = tUserDetail.RegistrationId;
                existingEntity.UserTypeId = tUserDetail.UserTypeId;
                existingEntity.UserDetailsStatusId = tUserDetail.UserDetailsStatusId;
                existingEntity.DayAdded = tUserDetail.DayAdded;
                existingEntity.AddedBy = tUserDetail.AddedBy;

                // Update EntityState to Modified
                _context.Entry(existingEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        // DELETE: api/TUserDetail/5
        [HttpDelete("DeleteTUserDetail/{id}")]
        public async Task<IActionResult> DeleteTUserDetail(int id)
        {
            try
            {
                var tUserDetail = await _context.TUserDetails.FindAsync(id);
                if (tUserDetail == null)
                {
                    return NotFound("User detail not found.");
                }

                _context.TUserDetails.Remove(tUserDetail);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }




        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromHeader] string Email, string Password)
        {
            try
            {

                // Authenticate the user (check username and hashed password)
                var user = _context.TUserDetails.SingleOrDefault(u => u.Email == Email);


                if(user == null)
                {
                    return NotFound("Login Fail");
                }


                var outputUserType = await _context.TUserTypes.FirstOrDefaultAsync(e => e.UserTypeId.Equals(user.UserTypeId));


                if (outputUserType!= null && VerifyPassword(Password, user.Password)) {

                    // Pass the Configuration object to the Token class constructor
                    Token token = new Token(Configuration);

                    var roleName = outputUserType.ClientTypeName;
                    // Generate a JWT token
                    var tokens = token.GenerateJwtToken(user, roleName);
                    return Ok(new { tokens });
                }
                else
                {
                    return NotFound("Login Fail");
                }

                return NotFound("Invalide UserType");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }



        private string HashPassword(string password)
        {
            // Generate a salt (the number 12 is the work factor, you can adjust it)
            var salt = BCrypt.Net.BCrypt.GenerateSalt(15);

            // Hash the password with the salt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }


        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            // Check if the input password matches the hashed password
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

    }
}
