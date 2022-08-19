using UnityEngine;
using UnityEngine.UI;
using Tool.PushNotifications;
using Tool.PushNotifications.Settings;

namespace Tool.Notifications.Examples
{
    internal sealed class NotificationWindow : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private NotificationSettings _settings;

        [Header("Scene Components")]
        [SerializeField] private Button _notificationButton;
        [SerializeField] private Button _delAllNotificationButton;

        private INotificationScheduler _scheduler;


        private void Awake()
        {
            NotificationSchedulerFactory schedulerFactory = new(_settings);
            _scheduler = schedulerFactory.Create();
        }

        private void OnEnable()
        {
            _notificationButton.onClick.AddListener(CreateNotification);
            _delAllNotificationButton.onClick.AddListener(DeleteNotification);
        }

        private void OnDisable()
        {
            _notificationButton.onClick.RemoveListener(CreateNotification);
            _delAllNotificationButton.onClick.RemoveListener(DeleteNotification);
        }

        private void CreateNotification()
        {
            foreach (NotificationData notificationData in _settings.Notifications)
                _scheduler.ScheduleNotification(notificationData);
        }

        private void DeleteNotification() => 
            _scheduler.RemoveAllNotifications();
    }
}