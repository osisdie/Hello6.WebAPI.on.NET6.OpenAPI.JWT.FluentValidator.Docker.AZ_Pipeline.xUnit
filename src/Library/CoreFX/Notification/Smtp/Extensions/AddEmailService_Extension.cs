using System;
using CoreFX.Abstractions.Notification.Models;
using CoreFX.Notification.Smtp.Interfaces;
using CoreFX.Notification.Smtp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoreFX.Notification.Smtp.Extensions
{
    public static class AddEmailService_Extension
    {
        public static IServiceCollection AddEmailService(
            this IServiceCollection serviceCollection,
            Action<EmailConfiguration> options,
            bool optional = false)
        {
            if (options == null && !optional)
            {
                throw new ArgumentNullException(nameof(options),
                    $"Please provide options for {typeof(IEmailService).Name}.");
            }

            if (options != null)
            {
                serviceCollection.Configure(options);
                serviceCollection.AddSingleton<IEmailService, EmailService>();
            }

            return serviceCollection;
        }
    }
}
