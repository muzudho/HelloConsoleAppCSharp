namespace HelloConsoleAppCSharp.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

/// <summary>
/// どんなコンソール・アプリを作るときでも、本題に入る前に似たようなコードを書くことになる……、そんな似たコード［ホストビルド］をまとめたヘルパークラスだぜ（＾～＾）！
/// </summary>
internal static class MuzHostHelper
{
    public static async Task RunAsync(
        string[] commandLineArgs,
        Func<IHostApplicationBuilder, Action<IServiceCollection>, Task> beforeHostBuild,
        Action<IServiceCollection> executeBeforeBuild,
        Func<IHostApplicationBuilder, IServiceProvider, Func<IServiceProvider, Task>, Task> afterHostBuild,
        Func<IServiceProvider, Task> executeAfterHostBuild)
    {
        // ビルダー作成（＾～＾）
        //
        //      ここでは、［コンソールアプリケーション］用のビルダーを作るぜ（＾～＾）！
        //      もし、［ウェブアプリケーション］用のビルダーが必要なら、コメントアウトしてある行を使って、コードの対応個所の型を全部書き替えてくれだぜ（＾～＾）！
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(commandLineArgs);  // コンソールアプリケーション用（＾～＾）
        //WebApplicationBuilder builder = WebApplication.CreateBuilder(commandLineArgs);  // ウェブアプリケーション用（＾～＾）

        // ホストビルド前の処理（＾～＾）　例えば、［サービス］を追加したりとか、そういうのだぜ（＾～＾）！
        await beforeHostBuild(builder, executeBeforeBuild);

        // ホストビルド（＾～＾）
        var host = builder.Build();

        // ホストビルド後の処理（＾～＾）
        await afterHostBuild(builder, host.Services, executeAfterHostBuild);
    }
}
