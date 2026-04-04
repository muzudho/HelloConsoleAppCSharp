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


    /// <summary>
    ///     <pre>
    /// 点滅するテキストを表示します。
    /// 点滅させるためには、定期的に、このメソッドを呼び出す必要があります。
    ///     </pre>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="left"></param>
    /// <param name="top"></param>
    /// <param name="fgColor"></param>
    /// <param name="bgColor"></param>
    /// <param name="isVisible"></param>
    public static void PrintBlinkingText(
        string text,
        int left,
        int top,
        ConsoleColor fgColor,
        ConsoleColor bgColor,
        bool isVisible)
    {
        // カーソルの現在位置を記憶。
        var oldLeft = Console.CursorLeft;
        var oldTop = Console.CursorTop;

        // 色を設定
        Console.ForegroundColor = fgColor;
        Console.BackgroundColor = bgColor;

        // 位置設定
        Console.SetCursorPosition(left, top);

        if (isVisible)
        {
            Console.Write(text);
        }
        else
        {
            // 点滅させるために、スペースで上書きします。
            Console.Write(new string(' ', text.Length));
        }

        // 色とカーソルの位置を戻す（大事！）
        Console.ResetColor();
        Console.SetCursorPosition(oldLeft, oldTop);
    }
}
