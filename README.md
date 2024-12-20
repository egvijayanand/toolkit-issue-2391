## Behaviors not working in CommunityToolkit.Maui NuGet package v10.0

Sample project to simulate the issue https://github.com/CommunityToolkit/Maui/issues/2391

As described in the Toolkit's [v10.0 release notes](https://github.com/CommunityToolkit/Maui/releases/tag/10.0.0), to address the breaking change with `Behavior's BindingContext`, it is now explicitly set on the `EventToCommandBehavior` node using the `Reference` binding workaround.

Refer to this article [.NET MAUI Community Toolkit v10.0 - How to Fix Behaviors Bindingcontext Issue](https://egvijayanand.in/2024/12/19/dotnet-maui-community-toolkit-v10-how-to-fix-bindingcontext-issues-with-behaviors/) for complete details.

#### XAML:

```xml
<HybridWebView x:Name="hwv">
    <HybridWebView.Behaviors>
        <mct:EventToCommandBehavior
            BindingContext="{Binding BindingContext, Source={x:Reference hwv}, x:DataType=HybridWebView}"
            Command="{Binding ShowMessageCommand}"
            EventArgsConverter="{StaticResource ArgsConverter}"
            EventName="RawMessageReceived" />
    </HybridWebView.Behaviors>
</HybridWebView>
```

#### C#:

```cs
Content = new HybridWebView()
    .Assign(out hwv) // hwv is defined as a field within the page.
    .Behaviors(new EventToCommandBehavior()
    {
        EventArgsConverter = (IValueConverter)Resources["ArgsConverter"],
        EventName = nameof(HybridWebView.RawMessageReceived)
    }.Bind(BindingContextProperty, static (HybridWebView x) => x.BindingContext, source: hwv)
     .BindCommand(static (MainViewModel vm) => vm.ShowMessageCommand));
```
