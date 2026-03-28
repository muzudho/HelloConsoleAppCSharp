namespace HelloConsoleAppCSharpTests;

using HelloConsoleAppCSharp.Infrastructure.Configuration;
using Xunit;

/// <summary>
///     <pre>
/// 単体テストの例。
/// 
///     - Grok の書いたものを調整しました。
///     </pre>
/// </summary>
public class HelloConsoleAppCSharpTests
{
    /// <summary>
    /// 2 と 3 を足したら 5 になることをテストする例。
    /// </summary>
    [Fact]
    public void AppName_SetString_ReturnsAppName()
    {
        // Expected
        var expected = "TestApp";


        // Act
        var appSettings = new MuzAppSettings();
        appSettings.AppName = expected;

        // Assert
        Assert.Equal(expected, appSettings.AppName);
    }
}
