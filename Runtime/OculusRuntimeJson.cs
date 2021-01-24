using System;
using System.IO;
using Microsoft.Win32;

namespace OpenXRRuntimeJsons
{
    internal class OculusRuntimeJson : IOpenXRRuntimeJson
    {
        private const string InstallLocKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Oculus";
        private const string InstallLocValue = "InstallLocation";
        private const string JsonName = @"Support\oculus-runtime\oculus_openxr_64.json";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.Oculus;

        public Lazy<string> JsonPath { get; } = new Lazy<string>(GetJsonPath);

        private static string GetJsonPath()
        {
            var oculusPathValue = Registry.GetValue(InstallLocKey, InstallLocValue, string.Empty);
            if (oculusPathValue is string oculusPath && !string.IsNullOrWhiteSpace(oculusPath))
            {
                var path = Path.Combine(oculusPath, JsonName);
                if (File.Exists(path))
                {
                    return Path.GetFullPath(path);
                }
            }

            return string.Empty;
        }
    }
}