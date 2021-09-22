// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenXRRuntimeJsons.Internal;

namespace OpenXRRuntimeJsons
{
    public static class OpenXRRuntimeJson
    {
        private static readonly IReadOnlyDictionary<OpenXRRuntimeType, string> OpenXRRuntimeJsons;

        static OpenXRRuntimeJson()
        {
            var runtimeJsons = new List<IOpenXRRuntimeJson>
            {
                new EnvironmentVariableRuntimeJson(),
                new SystemDefaultRuntimeJson(),
                new OculusRuntimeJson(),
                new WindowsMRRuntimeJson(),
                new SteamVRRuntimeJson(),
                new ViveVRRuntimeJson(),
                new VarjoRuntimeJson(),
            };
            OpenXRRuntimeJsons = runtimeJsons
                .Select(GetJsonPath)
                .Where(e => e.result)
                .ToDictionary(e => e.name, e => e.path);
        }

        private static (bool result, OpenXRRuntimeType name, string path) GetJsonPath(IOpenXRRuntimeJson e)
        {
            var result = e.TryGetJsonPath(out var path);
            return (result, e.Name, path);
        }

        public static IReadOnlyDictionary<OpenXRRuntimeType, string> GetRuntimeJsonPaths()
        {
            return OpenXRRuntimeJsons;
        }

        public static bool SetRuntimeJsonPath(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                EnvironmentVariableRuntimeJson.SetRuntimeJsonPath(jsonPath);
                return true;
            }

            return false;
        }

        public static bool SetRuntimeJsonPath(OpenXRRuntimeType runtimeType)
        {
            if (OpenXRRuntimeJsons.TryGetValue(runtimeType, out var jsonPath))
            {
                return SetRuntimeJsonPath(jsonPath);
            }

            return false;
        }

        public static string GetRuntimeName(string jsonPath)
        {
            var result = string.Empty;
            if (File.Exists(jsonPath))
            {
                var jsonText = File.ReadAllText(jsonPath);
                var json = Manifest.FromJson(jsonText);
                result = json?.Runtime?.Name;

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = Path.GetFileNameWithoutExtension(jsonPath);
                }
            }

            return result;
        }

        public static string GetCurrentRuntimeName()
        {
            if (!new EnvironmentVariableRuntimeJson().TryGetJsonPath(out var path))
            {
                new SystemDefaultRuntimeJson().TryGetJsonPath(out path);
            }

            return GetRuntimeName(path);
        }

        public static IReadOnlyDictionary<string, string> GetAvailableRuntimesJsonPath()
        {
            if (new OpenXRAvailableRuntimes().TryGetJsonPaths(out var activeRuntimes))
            {
                return activeRuntimes.ToDictionary(GetRuntimeName, v => v);
            }

            return null;
        }
    }
}