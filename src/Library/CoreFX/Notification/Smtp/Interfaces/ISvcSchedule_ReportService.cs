using CoreFX.Abstractions.Notification.Interfaces;

namespace CoreFX.Notification.Smtp.Interfaces
{
    public interface ISvcSchedule_ReportService<T>
        where T : class, IReportDecordDto
    {
        void StartTimer();
        void RestartTimer();
        void AddRecord(T rec);
        int CountRecords();
    }
}
