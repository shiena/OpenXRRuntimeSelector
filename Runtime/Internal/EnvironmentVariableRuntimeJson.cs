// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;

namespace OpenXRRuntimeJsons.Internal
{
    internal class EnvironmentVariableRuntimeJson : IOpenXRRuntimeJson
    {
        private const string EnvVarName = "XR_RUNTIME_JSON";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.EnvironmentVariable;

        public bool TryGetJsonPath(out string jsonPath)
        {
            jsonPath = Environment.GetEnvironmentVariable(EnvVarName);
            return true;
        }

        public static void SetRuntimeJsonPath(string jsonPath)
        {
            Environment.SetEnvironmentVariable(EnvVarName, jsonPath);
        }
    }
}