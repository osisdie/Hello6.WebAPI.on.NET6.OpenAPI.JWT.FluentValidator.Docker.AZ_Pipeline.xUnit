﻿using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestAbstractions.Extensions
{
    public static class LaunchSettingsExtension
    {
        private const string LaunchSettingFilePath = "Properties\\launchSettings.json";
        public static void SetEnvironmentVariables()
        {
            if (!File.Exists(LaunchSettingFilePath))
            {
                return;
            }

            using (var file = File.OpenText(LaunchSettingFilePath))
            {
                var reader = new JsonTextReader(file);
                var jObject = JObject.Load(reader);

                var variables = jObject
                    .GetValue("profiles")
                    .SelectMany(profiles => profiles.Children())
                    .SelectMany(profile => profile.Children<JProperty>())
                    .Where(prop => prop.Name == "environmentVariables")
                    .SelectMany(prop => prop.Value.Children<JProperty>())
                    .ToList();

                foreach (var variable in variables)
                {
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                }
            }
        }
    }
}
