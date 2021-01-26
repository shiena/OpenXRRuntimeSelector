using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenXRRuntimeJsons
{
    public static class OpenXRRuntimeJson
    {
        private static readonly IEnumerable<IOpenXRRuntimeJson> OpenXRRuntimeJsons;

        static OpenXRRuntimeJson()
        {
            OpenXRRuntimeJsons = new List<IOpenXRRuntimeJson>
            {
                new EnvironmentVariableRuntimeJson(),
                new SystemDefaultRuntimeJson(),
                new OculusRuntimeJson(),
                new WindowsMRRuntimeJson(),
                new SteamVRRuntimeJson(),
                new ViveVRRuntimeJson(),
                new VarjoRuntimeJson(),
            };
        }

        public static IDictionary<OpenXRRuntimeType, string> GetRuntimeJsonPaths()
        {
            return OpenXRRuntimeJsons
                .Where(e => !string.IsNullOrWhiteSpace(e.JsonPath.Value))
                .ToDictionary(e => e.Name, e => e.JsonPath.Value);
        }

        public static void SetRuntimeJsonPath(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                EnvironmentVariableRuntimeJson.SetRuntimeJsonPath(jsonPath);
            }
        }
    }
}