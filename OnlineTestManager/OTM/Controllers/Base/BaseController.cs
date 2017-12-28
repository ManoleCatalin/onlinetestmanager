using Data.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OTM.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;

        protected BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }
    }
}
