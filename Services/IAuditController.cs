using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Models;

namespace POS.Controllers
{
    public interface IAuditController
    {
        Task<ActionResult<IEnumerable<TAudit>>> GetAudits();

        Task<ActionResult<TAudit>> GetAuditByUser(int userId);

        Task<ActionResult> CreateAudit(TAuditHelper auditHelper);

    }
}
