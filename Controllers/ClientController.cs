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
    public class TClientController : ControllerBase, ITClientController
    {
        private readonly posDbContext _context;

        public TClientController(posDbContext context)
        {
            _context = context;
        }

        // POST: api/TClient
        [AllowAnonymous]
        [HttpPost("PostTClient")]
        public async Task<ActionResult> PostTClient(TClientHelper tClient)
        {
            try
            {
                var trueOrFlase = _context.TClients.Any(e => e.ClientId == tClient.ClientId);

                if (tClient == null)
                {

                    return BadRequest("Invalid input data.");

                }else if (trueOrFlase)
                {

                    return Ok("Client Already Exist");

                }
                TClient client = new();
                client.ClientId = tClient.ClientId;
                client.ClientLastName = tClient.ClientLastName;
                client.ClientFirstName = tClient.ClientFirstName;
                client.ClientPhoneNumber = tClient.ClientPhoneNumber;
                client.ClientEmail = tClient.ClientEmail;
                client.AddedBy = tClient.AddedBy;
                client.DayAdded = tClient.DayAdded;
                client.Country = tClient.Country;
                client.Region = tClient.Region;



                _context.TClients.Add(client);
                await _context.SaveChangesAsync();
                return Ok("Record Inserted Successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

       

        // GET: api/TClient
        [HttpGet("GetTClients")]
        public async Task<ActionResult<TClientHelper>> GetTClients()
        {
            try
            {
                var clients = await _context.TClients.ToListAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/TClient/5
        [HttpGet("GetTClient/{id}")]
        public async Task<ActionResult<TClientHelper>> GetTClient(int id)
        {
            try
            {
                var tClient = await _context.TClients.FindAsync(id);

                if (tClient == null)
                {
                    return NotFound("Client not found.");
                }

                return Ok(tClient);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/TClient/5
        [HttpPut("PutTClient/{id}")]
        public async Task<ActionResult<TClientHelper>> PutTClient(int id, TClientHelper tClient)
        {
            try
            {
                if (id != tClient.ClientId)
                {
                    return BadRequest("Invalid ID.");
                }

                _context.Entry(tClient).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok("Client Details Opdated Succesfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/TClient/5
        [HttpDelete("DeleteTClient/{id}")]
        public async Task<IActionResult> DeleteTClient(int id)
        {
            try
            {
                var tClient = await _context.TClients.FindAsync(id);
                if (tClient == null)
                {
                    return NotFound("Client not found.");
                }

                _context.TClients.Remove(tClient);
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
