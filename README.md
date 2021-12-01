# CodeCube.AspNetCore.Extensions
This library provides extensions for the .NET Core and .NET 5 framework in general

# Examples

## Option 1

The default implementation will add an 'x-application-header'-header to all the responses. The version is automatically taken from the entry assembly.
``` C#
app.UseVersionHeader();
```

## Option 2
For improved flexibility it is also possible to provide the following options;
```C#
app.UseVersionHeader(options =>
{
  // Provide the versionnumber
    options.Version = "1.0.0.0";

  // Provide the assembly to take the version from
    options.Assembly = "Assembly.To.Get.Version.From";

  // The name of the header to return
    options.HeaderName = "x-application-version";
});
```
