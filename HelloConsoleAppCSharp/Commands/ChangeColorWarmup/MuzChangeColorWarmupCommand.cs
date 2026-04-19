namespace HelloConsoleAppCSharp.Commands.ChangeColorWarmup;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;

/// <summary>
/// 文字色を変えるデモンストレーションをします。
/// </summary>
internal class MuzChangeColorWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        Console.WriteLine("まずはデフォルトの色だぜ（＾▽＾）");

        // 文字色（前景色）を一時的に赤に変更
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Green,
            onColorChanged: async () =>
            {
                Console.WriteLine("続けて、ピーマンみたいな緑色の文字にしたぜ（＾▽＾）！");

                // 続けて、背景色を一時的に黄色に変更
                await MuzConsoleHelper.SetColorAsync(
                    bgColor: ConsoleColor.Yellow,
                    onColorChanged: async () =>
                    {
                        Console.WriteLine("続けて、字の背景を黄色で塗りつぶしたぜ、かぼちゃみたいだな（＾▽＾）！");
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    });

                Console.WriteLine("字の背景色を戻して、ピーマンに戻ったぜ（＾～＾）");
            });

        Console.WriteLine("字の色も戻して、ここはデフォルトの色に戻ったよ（＾～＾）");

        return MuzRequestType.None;
    }
}
