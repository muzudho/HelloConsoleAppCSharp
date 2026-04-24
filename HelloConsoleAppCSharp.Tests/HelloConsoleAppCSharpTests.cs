namespace HelloConsoleAppCSharp.Tests;

using HelloConsoleAppCSharp.Infrastructure.Configuration;
using HelloConsoleAppCSharp.Features.Messages;
using Microsoft.Extensions.DependencyInjection;
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
        var services = new ServiceCollection();
        services.AddSingleton<ProgramService>();
        var serviceProvider = services.BuildServiceProvider();

        // Expected
        var expected = @"""Don't Repeat Yourself.""

    — The Pragmatic Programmer
      Andy Hunt, Dave Thomas";

        // Act
        var actual = MuzMessagesHelper.GetMessage(serviceProvider, "Msg_1");

        // Assert - 改行コードを統一して比較（クロスプラットフォーム対応）
        Assert.Equal(
            expected.ReplaceLineEndings("\n"), 
            actual.ReplaceLineEndings("\n"));
    }
}
