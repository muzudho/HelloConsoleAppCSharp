namespace HelloConsoleAppCSharp.Commands.TypewriterEffectWarmup;

using HelloConsoleAppCSharp.Infrastructure.REPL;

internal static class MuzTypewriterEffectWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        string text = "タイプライターエフェクトのウォームアップだぜ（＾～＾）\n複数行にも対応だぜ（＾～＾）！";

        // まず、行ごとに分割して、各行を順番に表示するぜ（＾～＾）
        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            // 各行を文字ごとに表示するぜ（＾～＾）
            foreach (char c in line)
            {
                Console.Write(c);
                await Task.Delay(100); // 100ミリ秒の遅延を入れるぜ（＾～＾）
            }
            Console.WriteLine(); // 行の終わりで改行するぜ（＾～＾）
        }

        return MuzRequestType.None;
    }
}
