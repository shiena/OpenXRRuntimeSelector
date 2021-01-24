using System;
using System.IO;

namespace OpenXRRuntimeJsons
{
    internal class WindowsMRRuntimeJson : IOpenXRRuntimeJson
    {
        private const string JsonName = "MixedRealityRuntime.json";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.WindowsMR;

        public Lazy<string> JsonPath { get; } = new Lazy<string>(GetJsonPath);

        private static string GetJsonPath()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            var path = Path.Combine(systemPath, JsonName);
            if (File.Exists(path))
            {
                return Path.GetFullPath(path);
            }

            return string.Empty;
        }
    }
}