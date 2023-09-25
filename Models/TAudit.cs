using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Keyless]
[Table("t_Audit")]
public partial class TAudit
{
    [Column("auditD")]
    public int AuditD { get; set; }

    [Unicode(false)]
    public string Action { get; set; } = null!;

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [ForeignKey("UserDetailsId")]
    public virtual TUserDetail UserDetails { get; set; } = null!;
}
