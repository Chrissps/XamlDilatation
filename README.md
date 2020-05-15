# XamlDilatation
Fully customizable Xaml Serializer

| Branch | Status                                                                                                 |
|--------|--------------------------------------------------------------------------------------------------------|
| master | ![.NET Core](https://github.com/Chrissps/XamlDilatation/workflows/.NET%20Core/badge.svg?branch=master) |


# XamlServiceExtensions

## RegisterShouldSerialize

```csharp
ShouldSerialize<T, TParent>(...);
ShouldSerialize<T>(...);
```

If you register it with both ways, the one with the parent is more detail and gets used first.
If you try to register the same function a second time, the existing function gets overwritten.
