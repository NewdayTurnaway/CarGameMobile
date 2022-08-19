using Tool.PushNotifications.Settings;
using UnityEngine;

namespace Tool.PushNotifications
{
    internal sealed class StubNotificationScheduler : INotificationScheduler
    {
        public void ScheduleNotification(NotificationData notificationData) =>
            Debug.Log($"[{GetType()}] {notificationData}");
    }
}
