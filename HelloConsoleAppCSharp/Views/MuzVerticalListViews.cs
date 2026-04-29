namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Core.Infrastructure;

/// <summary>
/// 垂直方向の箇条書きを表示します。
/// </summary>
internal static class MuzVerticalListViews
{
    public static async Task PrintMenuAsync(
        int left,
        int top,
        string[] items)
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            // メニュー項目を表示
            for (int dy = 0; dy < items.Length; dy++)
            {
                Console.SetCursorPosition(left, top + dy);
                Console.Write(items[dy]);
            }
        });
    }
}
