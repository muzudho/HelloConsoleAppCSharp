namespace HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// コンソールの操作でよく使う機能をまとめたクラス
/// </summary>
internal static class MuzConsoleHelper
{


    /// <summary>
    ///     <pre>
    /// テキストを点滅させます。
    /// 
    /// 仕組みとしては、２組みの［前景色、背景色］を用意して、一定間隔で切り替えることで点滅させる感じだぜ（＾～＾）！
    ///     </pre>
    /// </summary>
    /// <param name="onColorChanged">色を変更した後に実行する処理</param>
    /// <param name="fgColor">設定する前景色</param>
    /// <param name="bgColor">設定する背景色</param>
    public static async Task BlinkAsync(
        ConsoleColor fgColor1,
        ConsoleColor bgColor1,
        ConsoleColor fgColor2,
        ConsoleColor bgColor2,
        bool isColor2,
        Func<Task> onColorChanged)
    {
        // 現在の色を記憶
        var oldFgColor = Console.ForegroundColor;
        var oldBgColor = Console.BackgroundColor;

        // 色を設定
        if (isColor2)
        {
            Console.ForegroundColor = fgColor2;
            Console.BackgroundColor = bgColor2;
        }
        else
        {
            Console.ForegroundColor = fgColor1;
            Console.BackgroundColor = bgColor1;
        }

        // 色を変更した後の処理を実行
        await onColorChanged();

        // 色を戻す
        Console.ForegroundColor = oldFgColor;
        Console.BackgroundColor = oldBgColor;
    }
}
