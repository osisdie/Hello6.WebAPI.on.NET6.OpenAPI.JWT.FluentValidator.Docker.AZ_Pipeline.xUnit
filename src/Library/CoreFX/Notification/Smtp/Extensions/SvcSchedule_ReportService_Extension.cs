namespace CoreFX.Notification.Smtp.Extensions
{
    public static class SvcSchedule_ReportService_Extension
    {
        public static string ToResultColor(this bool isSuccess)
        {
            return isSuccess ? $"<font color='#70AD47'>successful</font>" : "<font color='#FF0000'>failed</font>";
        }
    }
}
