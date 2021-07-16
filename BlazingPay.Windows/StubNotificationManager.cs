using System;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;

namespace BlazingPay.Windows
{
    public class StubNotificationManager: INotificationManager
    {
        public bool Initialized { get; } = false;
        public event EventHandler<NotificationEventArgs> NotificationReceived;
        public Task<bool> Initialize()
        {
            return Task.FromResult(false);
        }

        public Task<string> ScheduleNotification(string title, string message)
        {
            return Task.FromResult<string>(null);
        }

        public void ReceiveNotification(string title, string message)
        {
        }
    }
}