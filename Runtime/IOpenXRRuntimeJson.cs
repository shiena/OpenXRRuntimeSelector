// Copyright 2021 KOGA Mitsuhiro Authors. All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using System;

namespace OpenXRRuntimeJsons
{
    internal interface IOpenXRRuntimeJson
    {
        OpenXRRuntimeType Name { get; }
        Lazy<string> JsonPath { get; }
    }
}