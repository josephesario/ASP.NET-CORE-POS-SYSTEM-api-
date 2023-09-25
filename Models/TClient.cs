using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_Client")]
public partial class TClient
{
    [Key]
    [Column("clientID")]
    public int ClientId { get; set; }

    [Column("clientFirstName")]
    [StringLength(60)]
    public string ClientFirstName { get; set; } = null!;

    [Column("clientLastName")]
    [StringLength(60)]
    public string ClientLastName { get; set; } = null!;

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

    [InverseProperty("Client")]
    public virtual ICollection<TAccount> TAccounts { get; set; } = new List<TAccount>();

    [InverseProperty("Client")]
    public virtual ICollection<TStock> TStocks { get; set; } = new List<TStock>();

    [InverseProperty("Client")]
    public virtual ICollection<TUserDetailsStatus> TUserDetailsStatuses { get; set; } = new List<TUserDetailsStatus>();

    [InverseProperty("Client")]
    public virtual ICollection<TUserRegistrationDetail> TUserRegistrationDetails { get; set; } = new List<TUserRegistrationDetail>();

    [InverseProperty("Client")]
    public virtual ICollection<TUserTypeStatus> TUserTypeStatuses { get; set; } = new List<TUserTypeStatus>();

    [InverseProperty("Client")]
    public virtual ICollection<TUserType> TUserTypes { get; set; } = new List<TUserType>();
}
