namespace HelloConsoleAppCSharp.Commands.Prints;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using System;

/// <summary>
/// EXPERIMENTAL: 色指定をしてメッセージ表示。
/// </summary>
internal class MuzMessageWithColorCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        string arguments,
        IServiceProvider services)
    {
        // 半角空白で引数を分割するぜ（＾～＾）
        //
        // 引数は必ず３つ入力されるものとし、
        // トークンの１つ目は前景色、
        // トークンの２つ目は背景色、
        // それ以降はメッセージだぜ（＾～＾）
        var parts = arguments.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

        // 引数が３つ未満のとき
        if (parts.Length < 3)
        {
            Console.WriteLine($@"【引数エラー】引数は半角区切りで、１つ目の要素は前景色、２つ目の要素は背景色、３つ目以降の要素はメッセージを入れてください（＾～＾）
全部で16色あるよ：
    Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
    DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
指定しない場合は Default を入れてね（＾～＾）
");
            return MuzRequestType.None;
        }

        ConsoleColor? fgColor = MuzConsoleHelper.GetColorByName(parts[0]);
        ConsoleColor? bgColor = MuzConsoleHelper.GetColorByName(parts[1]);
        string message = parts[2];

        // 続けて、背景色を一時的に黄色に変更
        await MuzConsoleHelper.SetColorAsync(
            fgColor: fgColor ?? Console.ForegroundColor,
            bgColor: bgColor ?? Console.BackgroundColor,
            onColorChanged: async () =>
            {
                Console.WriteLine(message);
            });

        return MuzRequestType.None;
    }
}
