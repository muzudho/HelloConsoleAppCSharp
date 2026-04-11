namespace HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// コンソールの操作でよく使う機能をまとめたクラス
/// </summary>
internal static class MuzConsoleHelper
{
    /// <summary>
    /// 前景色、背景色を一時的に変更して、指定された処理を実行します。
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
    /// 処理が終わった後、カーソルの位置をリセットします。（初期位置に戻す）
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
