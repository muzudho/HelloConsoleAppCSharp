namespace HelloConsoleAppCSharp.Views;

using HelloConsoleAppCSharp.Core.Infrastructure;

/// <summary>
/// （入力できる文字列と区別して）入力しない文字列をラベルと呼ぶことにします。
/// </summary>
internal static class MuzLabelViews
{
    /// <summary>
    /// フローティングラベル
    /// </summary>
    /// <param name="text"></param>
    /// <param name="left"></param>
    /// <param name="top"></param>
    /// <returns></returns>
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
