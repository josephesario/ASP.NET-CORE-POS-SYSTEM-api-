using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

public interface ISystemNotificationController
{
    Task<ActionResult<TSystemNotificationHelper>> PostTSystemNotification(TSystemNotificationHelper tSystemNotification);
    Task<ActionResult<IEnumerable<TSystemNotification>>> GetTSystemNotifications();
    Task<ActionResult<TSystemNotification>> GetTSystemNotification(int id);
    Task<IActionResult> PutTSystemNotification(int id, TSystemNotificationHelper tSystemNotification);
    Task<IActionResult> DeleteTSystemNotification(int id);
}
