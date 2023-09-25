using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_Supplier")]
public partial class TSupplier
{
    [Key]
    [Column("supplierID")]
    public int SupplierId { get; set; }

    [Column("supplierName")]
    [StringLength(120)]
    public string SupplierName { get; set; } = null!;

    [Column("supplierPhone_Number")]
    [StringLength(20)]
    public string SupplierPhoneNumber { get; set; } = null!;

    [Column("supplierEmail")]
    [StringLength(120)]
    public string? SupplierEmail { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [StringLength(120)]
    public string AddedBy { get; set; } = null!;

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<TStock> TStocks { get; set; } = new List<TStock>();

    [ForeignKey("UserDetailsId")]
    [InverseProperty("TSuppliers")]
    public virtual TUserDetail UserDetails { get; set; } = null!;
}
