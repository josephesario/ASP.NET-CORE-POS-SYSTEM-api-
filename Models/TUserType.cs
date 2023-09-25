using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Table("t_UserType")]
public partial class TUserType
{
    [Key]
    [Column("userTypeID")]
    public int UserTypeId { get; set; }

    [Column("clientID")]
    public int ClientId { get; set; }

    [Column("clientTypeName")]
    [StringLength(20)]
    public string ClientTypeName { get; set; } = null!;

    [Column("userTypeStatusID")]
    public int UserTypeStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

    [StringLength(120)]
    public string AddedBy { get; set; } = null!;

    [ForeignKey("ClientId")]
    [InverseProperty("TUserTypes")]
    public virtual TClient Client { get; set; } = null!;

    [InverseProperty("UserType")]
    public virtual ICollection<TUserDetail> TUserDetails { get; set; } = new List<TUserDetail>();
}
