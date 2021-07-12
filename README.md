# OpenXRRuntimeSelector
[![openupm](https://img.shields.io/npm/v/com.shiena.openxrruntimejson?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.shiena.openxrruntimejson/)

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

<details>
<summary>Add from OpenUPM <em>| via scoped registry, recommended</em></summary>

To add OpenUPM to your project:

- open `Edit/Project Settings/Package Manager`
- add a new Scoped Registry:
```
Name: OpenUPM
URL:  https://package.openupm.com/
Scope(s): com.shiena
```
- click <kbd>Save</kbd>
- open Package Manager
- Select ``My Registries`` in dropdown top left
- Select ``OpenXR Runtime Selector`` and click ``Install``
</details>

<details>
<summary>Add from GitHub | <em>not recommended, no updates through PackMan</em></summary>

You can also add it directly from GitHub on Unity 2019.4+. Note that you won't be able to receive updates through Package Manager this way, you'll have to update manually.

- open Package Manager
- click <kbd>+</kbd>
- select <kbd>Add from Git URL</kbd>
- paste `https://github.com/shiena/OpenXRRuntimeSelector.git`
- click <kbd>Add</kbd>
</details>

## Sample Code

If you start XR environment after setting a specific runtime JSON, that runtime will be used.

- [Automatic XR loading](https://docs.unity3d.com/Packages/com.unity.xr.management@4.0/manual/EndUser.html#automatic-xr-loading)

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

## Sample Project

- [OpenXRRuntimeSelectorSample](https://github.com/shiena/OpenXRRuntimeSelectorSample)

## References

- https://github.com/KhronosGroup/OpenXR-SDK-Source/blob/master/specification/loader/runtime.adoc
