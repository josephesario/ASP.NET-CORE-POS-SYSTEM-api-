using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_UserDetails_Status")]
public partial class TUserDetailsStatus
{
    [Key]
    [Column("userDetails_StatusID")]
    public int UserDetailsStatusId { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayChanged { get; set; }

    [StringLength(120)]
    public string ChangedBy { get; set; } = null!;

    [Column("clientID")]
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("TUserDetailsStatuses")]
    public virtual TClient Client { get; set; } = null!;

    [InverseProperty("UserDetailsStatus")]
    public virtual ICollection<TUserDetail> TUserDetails { get; set; } = new List<TUserDetail>();
}
