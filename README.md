# OpenXRRuntimeSelector
Runtime Json Selector for Unity OpenXR

## System Requirements

Unity
  - 2020.2+

Platforms
  - Windows

API Compatibility Level
  - .NET 4.x
  - .NET Standard 2.0 with [Microsoft.Win32.Registry](https://www.nuget.org/packages/Microsoft.Win32.Registry/)

## Support Runtime

- Environment Variable (`XR_RUNTIME_JSON`)
- System Default
- Oculus
- WindowsMR
- SteamVR
- ViveVR
- Varjo

## Installation

You can add `https://github.com/shiena/OpenXRRuntimeSelector.git` to Package Manager.

## Sample Code

```cs
using OpenXRRuntimeJsons;

void SetOculusRuntimeFromAvailableRuntimes()
{
    // Get the available OpenXR runtime json
    IDictionary<OpenXRRuntimeType,string> openXRRuntimes = OpenXRRuntimeJson.GetRuntimeJsonPaths();
    if (openXRRuntimes.TryGetValue(OpenXRRuntimeType.Oculus, out string oculusRuntime))
    {
        // Use the Oculus OpenXR runtime
        OpenXRRuntimeJson.SetRuntimeJsonPath(oculusRuntime);
    }
}
```

## References

- https://github.com/KhronosGroup/OpenXR-SDK-Source/blob/master/specification/loader/runtime.adoc
