# `Avolantis.Text.QueryString`
> URL query string handling and serialization for .NET 6

[![nuget](https://img.shields.io/nuget/v/Avolantis.Text.QueryString?logo=nuget)](https://www.nuget.org/packages/Avolantis.Text.QueryString)
[![main](https://github.com/avolantis/Avolantis.Text.QueryString/workflows/main/badge.svg)](https://github.com/avolantis/Avolantis.Text.QueryString/actions/workflows/main.yml)
[![coverage](https://coveralls.io/repos/github/avolantis/Avolantis.Text.QueryString/badge.svg)](https://coveralls.io/github/avolantis/Avolantis.Text.QueryString)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](./.github/CODE_OF_CONDUCT.md)
[![license: MIT](https://img.shields.io/badge/license-MIT-yellow.svg)](./LICENSE.md)

This library helps you manage URL query parameters in a specialized collection and
allows you to serialize a POCO to a query string.

**:warning: :construction: IMPORTANT: This project has not yet reached it's first stable release
and is being developed currently. See how you can help below.**

## Install
```shell
dotnet package add Avolantis.Text.QueryString
```

`Avolantis.Text.QueryString` targets .NET 6. Support for preceding versions of .NET (Core) is
not a current goal. Please open an [issue](https://github.com/avolantis/Avolantis.Text.QueryString/issues)
to request targeting such environments.

The project follows the conventions outlined in [Semantic Versioning 2.0.0](https://www.semver.org).

## Motivation
Imagine having to write a client application in C# (e.g. Blazor WebAssembly) to send queries
to REST endpoint with various filtering options via query string parameters, like
```c#
Task<IEnumerable<UserDto>> GetList([FromQuery] UserQueryOptions options)
{
    // ...
}

class UserDto
{
    public string UserName { get; set; }
    public DateTime CreatedAt { get;set; }
    public bool IsDeleted { get; set; }
}

class UserQueryOptions
{
    // Retrieve users with specified names 
    public IEnumerbale<string> Name { get; set; }
    
    // Retrieve users created before the given timestamp 
    public DateTime? CreatedBefore { get; set; }
    
    // Filter by deleted status 
    public bool IsDeleted { get; set; } = false;    
}
```

Using this library you can use a POCO like the one above to construct your filters.
```c#
var filter = new UserQueryOptions
{
    Name = new string[] { "John Doe", "Jane Doe" },
    IsDeleted = true
};
var qs = QueryStringSerializer.Serialize(filter);
// ?name=John+Doe&name=Jane+Doe&isDeleted=true
```

## Features

- `QueryParameterCollection` for constructing and manipulating query string parameters
- Built-in support for serializing primitives, strings, `Guid`-s,
`DateTime`, `DateOnly`, `TimeOnly`, anonymous objects, POCO-s
and collections implementing `IEnumerable`
- Extensible converter system inspired by System.Text.Json
- Override serialization defaults using attributes, like
`[QueryStringParameterName("param-name")]` or `[QueryStringIgnore]`
- Auto trim string parameters
- Configurable parameter name casing
- Convert `DateTime` to UTC automatically

## Documentation
Documentation for the project is located the `docs` folder and can be viewed online at
[https://avolantis.github.io/Avolantis.Text.QueryString](https://avolantis.github.io/Avolantis.Text.QueryString).
API documentation is available on the website, generated using [docfx](https://dotnet.github.io/docfx/)
from XML source code comments. Please note, these docs are under construction, as the project itself.

## Support
Feature requests and bug reports can be submitted in the project's
[issue tracker](https://github.com/avolantis/Avolantis.Text.QueryString/issues) on GitHub.
You can also open a support request ticket, please use one of the available issue templates.

In case of a security issue or vulnerability, please use a private communication channel
to report such incident towards the current maintainer, or [Avolantis](https://avolantis.net)
on security@avolantis.net. This helps us prepare a fix for a potential vulnerability
before publicizing that such issue exists.

Paid priority support is also available for commercial environments. Please contact
[@avolantis](https://github.com/avolantis) for details of this service.

## Limitations
This library is not _(yet)_ capable of deserializing query string to CLR objects.
As expressed in the [motivation](#motivation) section, this library is primarily
meant to provide an easy way to serialize POCO-s to query strings.

As a usable alternative is to use the build-in query binder in aspnet core and
annotate the parameters of the model with `[FromQuery]`, however this does not
support nested binding objects.

```c#
public class Filter
{
    public string Name { get; set; }
}

// ..

[ApiController]
public class UsersController : ControllerBase
{
    public Task<IEnumerable<User>> GetList([FromQuery] Filter filter)
    {
        // Do something with filter
    }
}
```

## Contributing

**Contributions** from the open source community **are welcomed and appreciated**.
Please, make sure you read the [contribution guidelines](./.github/CONTRIBUTING.md)
before submitting a PR.

This project has adopted the [Contributor Covenant](https://www.contributor-covenant.org/)
code of conduct, which can be found [here](./.github/CODE_OF_CONDUCT.md).

Maintainer: [@bencelang](https://github.com/bencelang)

## License

[MIT](./LICENSE.md)
