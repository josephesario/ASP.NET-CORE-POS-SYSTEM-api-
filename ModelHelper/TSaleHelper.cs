using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Keyless]
[Table("t_Sales")]
[Index("ProductId", Name = "UQ__t_Sales__2D10D14B9622D7A0", IsUnique = true)]
public partial class TSaleHelper
{
    [Column("productID")]
    public int ProductId { get; set; }

    [Column("productName")]
    [StringLength(120)]
    public string ProductName { get; set; } = null!;

    [Column("productQuantity")]
    public int ProductQuantity { get; set; }

    [Column("totalPrice", TypeName = "decimal(18, 0)")]
    public decimal? TotalPrice { get; set; }

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

}
