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
                .Where(e => !string.IsNullOrWhiteSpace(e.JsonPath.Value))
                .ToDictionary(e => e.Name, e => e.JsonPath.Value);
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