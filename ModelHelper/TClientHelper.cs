using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_Client")]
public partial class TClientHelper
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


}
