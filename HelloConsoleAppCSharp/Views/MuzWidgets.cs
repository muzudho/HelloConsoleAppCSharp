namespace HelloConsoleAppCSharp.Views;

/// <summary>
/// 画面上の部品（ウィジェット）を表示するためのものです。
/// </summary>
internal static class MuzWidgets
{
    /// <summary>
    /// 開始日時からの経過時間を表示します。
    /// </summary>
    /// <param name="startDateTime">開始日時</param>
    public static void PrintErapsedTime(
        string label,
        DateTime startDateTime,
        int left,
        int top,
        ConsoleColor fgColor,
        ConsoleColor bgColor)
    {
        // カーソルの現在位置を記憶。
        var oldLeft = Console.CursorLeft;
        var oldTop = Console.CursorTop;

        // 色を設定
        Console.ForegroundColor = fgColor;
        Console.BackgroundColor = bgColor;

        // 位置設定
        Console.SetCursorPosition(left, top);

        var elapsed = DateTime.Now - startDateTime;
        
        Console.WriteLine($"{label}{elapsed.Hours:D2}°{elapsed.Minutes:D2}'{elapsed.Seconds:D2}\"{elapsed.Milliseconds:D3}");

        // 色とカーソルの位置を戻す（大事！）
        Console.ResetColor();
        Console.SetCursorPosition(oldLeft, oldTop);
    }
}
