using System;
using CoreFX.Abstractions.Notification.Interfaces;
using CoreFX.Abstractions.Notification.Models;
using CoreFX.Notification.Smtp.Interfaces;
using CoreFX.Notification.Smtp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoreFX.Notification.Smtp.Extensions
{
    public static class AddReportService_Extension
    {
        public static IServiceCollection AddReportService<T>(
            this IServiceCollection serviceCollection,
            Action<ReportConfiguration> options,
            bool optional = true) where T : class, IReportDecordDto
        {
            if (options == null && !optional)
            {
                throw new ArgumentNullException(nameof(options),
                    $"Please provide options for {typeof(ISvcSchedule_ReportService<>).Name}.");
            }

            if (options != null)
            {
                serviceCollection.Configure(options);
                serviceCollection.AddSingleton<ISvcSchedule_ReportService<T>, SvcSchedule_ReportService<T>>();
            }

            return serviceCollection;
        }
    }
}
