namespace HelloConsoleAppCSharp.Core.Infrastructure;

/// <summary>
/// コンソールの操作でよく使う機能をまとめたクラス
/// </summary>
public static class MuzConsoleHelper
{


    // ========================================
    // 窓口メソッド
    // ========================================


    /// <summary>
    ///     <pre>
    /// 前景色、背景色を一時的に変更して、指定された処理を実行します。
    ///     
    ///     📍 NOTE:
    ///     
    ///         全部で16色あるよ：
    ///     
    ///         Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
    ///         DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
    ///     
    ///         Console.Clear(); を呼ぶと、ウィンドウ全体の背景色も変わる（現在のBackgroundColorが適用される）。
    ///         ANSIエスケープシーケンス を使えば、真のRGBカラー（24bit）や下線・太字なども使えるようになる。
    ///     
    ///     </pre>
    /// </summary>
    /// <param name="onColorChanged">色を変更した後に実行する処理</param>
    /// <param name="fgColor">設定する前景色</param>
    /// <param name="bgColor">設定する背景色</param>
    public static async Task SetColorAsync(
        Func<Task> onColorChanged,
        ConsoleColor? fgColor = null,
        ConsoleColor? bgColor = null)
    {
        // 現在の色を記憶
        var oldFgColor = Console.ForegroundColor;
        var oldBgColor = Console.BackgroundColor;

        // 色を設定
        Console.ForegroundColor = fgColor ?? Console.ForegroundColor;
        Console.BackgroundColor = bgColor ?? Console.BackgroundColor;

        // 色を変更した後の処理を実行
        await onColorChanged();

        // 色を戻す
        Console.ForegroundColor = oldFgColor;
        Console.BackgroundColor = oldBgColor;
    }


    /// <summary>
    ///     <pre>
    /// 色名からオブジェクトを取得するぜ（＾～＾）！
    ///     
    ///     📍 NOTE:
    ///     
    ///         全部で16色あるよ：
    ///     
    ///         Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
    ///         DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
    ///         
    ///         指定しない場合は Default を入れてね（＾～＾）！
    ///     </pre>
    /// </summary>
    /// <param name="name">色の名前</param>
    /// <returns>対応するConsoleColorオブジェクト、またはnull（Defaultの場合）</returns>
    public static ConsoleColor? GetColorByName(string name)
    {
        switch (name)
        {
            case "Black":
                return ConsoleColor.Black;

            case "DarkBlue":
                return ConsoleColor.DarkBlue;

            case "DarkGreen":
                return ConsoleColor.DarkGreen;

            case "DarkCyan":
                return ConsoleColor.DarkCyan;

            case "DarkRed":
                return ConsoleColor.DarkRed;

            case "DarkMagenta":
                return ConsoleColor.DarkMagenta;

            case "DarkYellow":
                return ConsoleColor.DarkYellow;

            case "Gray":
                return ConsoleColor.Gray;

            case "DarkGray":
                return ConsoleColor.DarkGray;

            case "Blue":
                return ConsoleColor.Blue;

            case "Green":
                return ConsoleColor.Green;

            case "Cyan":
                return ConsoleColor.Cyan;

            case "Red":
                return ConsoleColor.Red;

            case "Magenta":
                return ConsoleColor.Magenta;

            case "Yellow":
                return ConsoleColor.Yellow;

            case "White":
                return ConsoleColor.White;

            case "Default": // thru
            default:
                return null;
        }
    }


    /// <summary>
    /// 処理が終わった後、カーソルを元の位置に戻します。
    /// </summary>
    /// <returns></returns>
    public static async Task ResetCursorLocationAfterExecute(
        Func<Task> executeAsync)
    {
        // 現在のカーソル位置を記憶
        var oldLeft = Console.CursorLeft;
        var oldTop = Console.CursorTop;

        // 処理を実行
        await executeAsync();

        // カーソルの位置を戻す
        Console.SetCursorPosition(oldLeft, oldTop);
    }


}
