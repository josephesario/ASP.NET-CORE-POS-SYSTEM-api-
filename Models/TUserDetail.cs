using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_UserDetails")]
public partial class TUserDetail
{
    [Key]
    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

    [Column("userName")]
    [StringLength(20)]
    public string UserName { get; set; } = null!;

    [Column("email")]
    [StringLength(120)]
    public string? Email { get; set; }

    [Column("password")]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("registrationID")]
    public int RegistrationId { get; set; }

    [Column("userTypeID")]
    public int UserTypeId { get; set; }

    [Column("userDetails_StatusID")]
    public int UserDetailsStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [StringLength(120)]
    public string AddedBy { get; set; } = null!;

    [ForeignKey("RegistrationId")]
    [InverseProperty("TUserDetails")]
    public virtual TUserRegistrationDetail Registration { get; set; } = null!;

    [InverseProperty("UserDetails")]
    public virtual ICollection<TAccount> TAccounts { get; set; } = new List<TAccount>();

    [InverseProperty("UserDetails")]
    public virtual ICollection<TProductCategory> TProductCategories { get; set; } = new List<TProductCategory>();

    [InverseProperty("UserDetails")]
    public virtual ICollection<TStockStatus> TStockStatuses { get; set; } = new List<TStockStatus>();

    [InverseProperty("UserDetails")]
    public virtual ICollection<TSupplier> TSuppliers { get; set; } = new List<TSupplier>();

    [ForeignKey("UserDetailsStatusId")]
    [InverseProperty("TUserDetails")]
    public virtual TUserDetailsStatus UserDetailsStatus { get; set; } = null!;

    [ForeignKey("UserTypeId")]
    [InverseProperty("TUserDetails")]
    public virtual TUserType UserType { get; set; } = null!;
}
