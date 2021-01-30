// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;

namespace OpenXRRuntimeJsons
{
    internal class EnvironmentVariableRuntimeJson : IOpenXRRuntimeJson
    {
        private const string EnvVarName = "XR_RUNTIME_JSON";

        public OpenXRRuntimeType Name => OpenXRRuntimeType.EnvironmentVariable;
        public Lazy<string> JsonPath { get; } = new Lazy<string>(GetJsonPath);

        private static string GetJsonPath()
        {
            return Environment.GetEnvironmentVariable(EnvVarName);
        }

        public static void SetRuntimeJsonPath(string jsonPath)
        {
            Environment.SetEnvironmentVariable(EnvVarName, jsonPath);
        }
    }
}