namespace HelloConsoleAppCSharp.Infrastructure.Configuration;

using Microsoft.Extensions.Configuration;   // `.AddJsonFile()` とか `IConfiguration` とかのために必要（＾～＾）
using Microsoft.Extensions.DependencyInjection; // `builder.Services.Configure<T>()` のために必要（＾～＾）
using Microsoft.Extensions.Hosting; // `HostApplicationBuilder` のために必要（＾～＾）

/// <summary>
/// ［アプリケーション設定ファイル］そのもののコードではないが、それを使う準備をするためのコードをまとめたクラスだぜ（＾～＾）！
/// </summary>
internal static class MuzAppSettingsHelper
{
    /// <summary>
    /// ［ホストビルド］の前に、諸々の設定をするぜ（＾～＾）
    /// </summary>
    public static void SetupBeforeHostBuild(IHostApplicationBuilder builder)
    {
        // ［設定ファイル］の設定（＾～＾）
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // 必須ファイル。ファイル編集後にはリロードするぜ（＾～＾）！
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)   // 環境ごとのファイル。ファイル編集後にはリロードするぜ（＾～＾）！　必須じゃないぜ（＾～＾）！
            .AddEnvironmentVariables();  // 環境変数で［環境の名前］を使って、設定のカスケード（上書き）を可能にするぜ（＾～＾）

        // ここで DI コンテナにサービスを登録（＾～＾）
        builder.Services.Configure<MuzAppSettings>(builder.Configuration);  // さっきの［設定ファイルの設定］を MuzAppSettings クラスにバインド（＾～＾）
    }
}
