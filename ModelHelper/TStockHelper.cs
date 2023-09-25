using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_Stock")]
public partial class TStockHelper
{
    [Key]
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

    [Column("totalAmount", TypeName = "decimal(18, 0)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? PricePerUnit { get; set; }

    [StringLength(120)]
    public string AddedBy { get; set; } = null!;

    [Column("supplierID")]
    public int SupplierId { get; set; }

    [Column("clientID")]
    public int ClientId { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }


}
