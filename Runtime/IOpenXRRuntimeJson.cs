using System;

namespace OpenXRRuntimeJsons
{
    internal interface IOpenXRRuntimeJson
    {
        OpenXRRuntimeType Name { get; }
        Lazy<string> JsonPath { get; }
    }
}