namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;

/// <summary>
/// フローティングテキスト
/// </summary>
internal static class MuzFloatingTextViews
{
    public static async Task PrintAsync(
        string text,
        int left = 0,
        int top = 0)
    {
        // 処理の後、カーソルを元の位置に戻す
        await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
        {
            int currentTop = top;

            // まず、行ごとに分割して、各行を順番に表示するぜ（＾～＾）
            string[] lines = text.Split('\n');

            foreach (string line in lines)
            {
                // 行頭
                Console.SetCursorPosition(left, currentTop);

                // 行を表示するぜ（＾～＾）
                Console.WriteLine(line);
                currentTop++;
            }
        });
    }
}
