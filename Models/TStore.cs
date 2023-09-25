using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Keyless]
[Table("t_Store")]
[Index("ProductId", Name = "UQ__t_Store__2D10D14B86179938", IsUnique = true)]
public partial class TStore
{
    [Column("productID")]
    public int ProductId { get; set; }

    [Column("productName")]
    [StringLength(120)]
    public string ProductName { get; set; } = null!;

    [Column("Man_Date", TypeName = "datetime")]
    public DateTime ManDate { get; set; }

    [Column("Exp_Date", TypeName = "datetime")]
    public DateTime ExpDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [Column("stockAvailable")]
    public int StockAvailable { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? PricePerUnit { get; set; }

    [StringLength(120)]
    public string AddedBy { get; set; } = null!;

    [Column("stockStatusID")]
    public int StockStatusId { get; set; }

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

    [ForeignKey("ProductId")]
    public virtual TStock Product { get; set; } = null!;

    [ForeignKey("StockStatusId")]
    public virtual TStockStatus StockStatus { get; set; } = null!;

    [ForeignKey("UserDetailsId")]
    public virtual TUserDetail UserDetails { get; set; } = null!;
}
