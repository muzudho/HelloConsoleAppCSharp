namespace HelloConsoleAppCSharp.Tests;

using HelloConsoleAppCSharp.Core.Features.Messages;
using HelloConsoleAppCSharp.Application;
using HelloConsoleAppCSharp.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;     // ServiceCollection を使用するために。
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


    /// <summary>
    /// メッセージを取得するテスト
    /// </summary>
    [Fact]
    public void Message_GetMessage_ReturnsMessage()
    {
        // Arrange - テスト用のDIコンテナを作成
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ApplicationService>();
        var services = serviceCollection.BuildServiceProvider();

        // Expected
        var expected = @"""Don't Repeat Yourself.""

    — The Pragmatic Programmer
      Andy Hunt, Dave Thomas";

        // Act
        var msgSvc = services.GetRequiredService<MuzMessagesService>();
        var actual = msgSvc.GetMessage("Msg_1");

        // Assert - 改行コードを統一して比較（クロスプラットフォーム対応）
        Assert.Equal(
            expected.ReplaceLineEndings("\n"), 
            actual.ReplaceLineEndings("\n"));
    }
}
