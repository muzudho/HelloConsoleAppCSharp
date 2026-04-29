namespace HelloConsoleAppCSharp.Commands.TypewriterEffectWarmup;

using HelloConsoleAppCSharp.Core.Infrastructure;

internal static class MuzTypewriterEffectWarmupCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services,
        int left,
        int top,
        string message)
    {
        int currentTop = top;

        // まず、行ごとに分割して、各行を順番に表示するぜ（＾～＾）
        string[] lines = message.Split('\n');

        foreach (string line in lines)
        {
            // 行頭
            Console.SetCursorPosition(left, currentTop);

            // 各行を文字ごとに表示するぜ（＾～＾）
            foreach (char c in line)
            {
                Console.Write(c);
                await Task.Delay(100); // 100ミリ秒の遅延を入れるぜ（＾～＾）
            }

            Console.WriteLine(); // 行の終わりで改行するぜ（＾～＾）
            currentTop++;
        }

        return MuzREPLRequestType.None;
    }
}
