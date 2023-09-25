using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_Account")]
public partial class TAccount
{
    [Key]
    [Column("accountID")]
    public int AccountId { get; set; }

    [Column("accountName")]
    [StringLength(120)]
    public string AccountName { get; set; } = null!;

    [Column("deposit", TypeName = "decimal(18, 0)")]
    public decimal? Deposit { get; set; }

    [Column("allTimeBalance", TypeName = "decimal(18, 0)")]
    public decimal? AllTimeBalance { get; set; }

    [Column("withdrawal", TypeName = "decimal(18, 0)")]
    public decimal? Withdrawal { get; set; }

    [Column("description")]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("clientID")]
    public int? ClientId { get; set; }

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("TAccounts")]
    public virtual TClient? Client { get; set; }

    [ForeignKey("UserDetailsId")]
    [InverseProperty("TAccounts")]
    public virtual TUserDetail UserDetails { get; set; } = null!;
}
