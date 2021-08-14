// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public static void SetRuntimeJsonPath(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                EnvironmentVariableRuntimeJson.SetRuntimeJsonPath(jsonPath);
            }
        }
    }
}