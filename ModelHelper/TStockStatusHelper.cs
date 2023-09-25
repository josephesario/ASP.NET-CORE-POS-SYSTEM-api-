using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_Stock_Status")]
public partial class TStockStatusHelper
{
    [Key]
    [Column("stockStatusID")]
    public int StockStatusId { get; set; }

    public bool IsActive { get; set; }

    [Column("productID")]
    public int ProductId { get; set; }

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

}
