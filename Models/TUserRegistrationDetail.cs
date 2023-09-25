using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_UserRegistrationDetails")]
public partial class TUserRegistrationDetail
{
    [Key]
    [Column("registrationID")]
    public int RegistrationId { get; set; }

    [StringLength(60)]
    public string FirstName { get; set; } = null!;

    [StringLength(60)]
    public string LastName { get; set; } = null!;

    [StringLength(120)]
    public string Identification { get; set; } = null!;

    [StringLength(20)]
    public string Country { get; set; } = null!;

    [StringLength(20)]
    public string? Region { get; set; }

    [Column("clientPhone_Number")]
    [StringLength(20)]
    public string ClientPhoneNumber { get; set; } = null!;

    [Column("clientEmail")]
    [StringLength(120)]
    public string? ClientEmail { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [StringLength(120)]
    public string AddedBy { get; set; } = null!;

    [Column("clientID")]
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("TUserRegistrationDetails")]
    public virtual TClient Client { get; set; } = null!;

    [InverseProperty("Registration")]
    public virtual ICollection<TUserDetail> TUserDetails { get; set; } = new List<TUserDetail>();
}
