using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

[Keyless]
[Table("t_SystemNotifications")]
public partial class TSystemNotificationHelper
{
    [Column("messageID")]
    public int MessageId { get; set; }

    [Column("message")]
    [Unicode(false)]
    public string Message { get; set; } = null!;

    [Column("userDetailsID")]
    public int UserDetailsId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DayAdded { get; set; }

}
