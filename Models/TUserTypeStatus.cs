using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_UserType_Status")]
public partial class TUserTypeStatus
{
    [Key]
    [Column("userTypeStatusID")]
    public int UserTypeStatusId { get; set; }

    [Column("userTypeStatusName")]
    [StringLength(20)]
    public string UserTypeStatusName { get; set; } = null!;

    [Column("canAdd")]
    public bool CanAdd { get; set; }

    [Column("canView")]
    public bool CanView { get; set; }

    [Column("canEdit")]
    public bool CanEdit { get; set; }

    [Column("canDelete")]
    public bool CanDelete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayChanged { get; set; }

    [StringLength(120)]
    public string ChangedBy { get; set; } = null!;

    [Column("clientID")]
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("TUserTypeStatuses")]
    public virtual TClient Client { get; set; } = null!;
}
