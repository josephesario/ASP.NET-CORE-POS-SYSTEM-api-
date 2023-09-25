using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace POS.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : Controller, IAuditController
    {
        private readonly posDbContext _context;

        public AuditController(posDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAudits")]
        public async Task<ActionResult<IEnumerable<TAudit>>> GetAudits()
        {
            var audits = await _context.TAudits.ToListAsync();
            return audits;
        }

        [HttpGet("GetAuditby/{id}")]
        public async Task<ActionResult<TAudit>> GetAudit(int id)
        {
            var audit = await _context.TAudits.FindAsync(id);

            if (audit == null)
            {
                return NotFound();
            }

            return audit;
        }


        [HttpGet("GetAuditByUser/{userId}")]
        public async Task<ActionResult<TAudit>> GetAuditByUser(int userId)
        {
            var audit = await _context.TAudits.Where(e =>e.UserDetailsId.Equals(userId)).LastAsync();

            if (audit == null)
            {
                return NotFound();
            }

            return audit;
        }


        [HttpPost("CreateAudit")]
        public async Task<ActionResult> CreateAudit(TAuditHelper auditHelper)
        {

            try {

                TAudit audit = new TAudit();
                audit.AuditD = auditHelper.AuditD;
                audit.Action = auditHelper.Action;
                audit.DayAdded = auditHelper.DayAdded;
                audit.UserDetailsId = auditHelper.UserDetailsId;

                _context.TAudits.Add(audit);
                await _context.SaveChangesAsync();

                return Ok("Inserted Succesfully");

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
