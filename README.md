# CodeCube.AspNetCore.Extensions
This library provides extensions for the .NET Core and .NET 5 framework in general

#Examples

##Example 1 - Default settings

When using the default implementation, the version of the current entry assembly is taken to return in a header with the name 'x-application-version'

```C#
app.UseVersionHeader();
```

##Example 2 - Custom settings

You can set the assembly to grab the version from, the header name or even the version yourself.

```C#
app.UseVersionHeader(options =>
            {
                options.Assembly = "CodeCube.AspnetCore.Extensions";
                options.HeaderName = "x-my-versionheader";
                options.Version = "1.0.0";
            });
```