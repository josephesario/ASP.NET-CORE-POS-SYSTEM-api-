using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_Product_Category")]
public partial class TProductCategory
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(120)]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [StringLength(120)]
    public string AddedBy { get; set; } = null!;

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<TStock> TStocks { get; set; } = new List<TStock>();

    [ForeignKey("UserDetailsId")]
    [InverseProperty("TProductCategories")]
    public virtual TUserDetail UserDetails { get; set; } = null!;
}
