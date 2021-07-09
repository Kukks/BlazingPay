using System;
using System.Threading.Tasks;

namespace BlazingPay.Abstractions.Contracts
{
    public interface INotificationManager
    {
        bool Initialized { get; }
        event EventHandler<NotificationEventArgs> NotificationReceived;

        Task<bool> Initialize();

        Task<string> ScheduleNotification(string title, string message);

        void ReceiveNotification(string title, string message);
    }
}