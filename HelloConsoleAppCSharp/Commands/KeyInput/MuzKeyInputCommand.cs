namespace HelloConsoleAppCSharp.Commands.KeyInput;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using System.Text;
using static HelloConsoleAppCSharp.Infrastructure.REPL.MuzREPL;

internal static class MuzKeyInputCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync()
    {
        // 📍 NOTE:
        //
        //      日本語入力への対応や、バックスペースキーの自力実装が難しいので、ここでは半角英数字キー１つの押下だけを想定しているぜ（＾～＾）！
        //

        Console.WriteLine("日本語入力への対応や、バックスペースキーの自力実装が難しいので、ここでは半角英数字キー１つの押下だけを想定しているぜ（＾～＾）！");
        Console.WriteLine("キー入力待機中。［エンターキー］押下でループを抜けるぜ（＾～＾）...");

        // （エンターキーが押されるまでの）入力中で未確定な文字列。
        StringBuilder draftString = new StringBuilder();

        while (true)  // 無限ループ。
        {
            // 📍 NOTE:
            //
            //      キー入力を受け取ります。
            //      プログラムは、ユーザーがキーを押すまで、ここで待機します。  
            //      `intercept`:  true でエコー（表示）しない。
            //
            ConsoleKeyInfo key = Console.ReadKey(
                intercept: true);

            // 📍 NOTE:
            //
            //      ここにお前のキー入力処理を書く。
            //

            // 例えば、F1〜F12のファンクションキーを検知することができるぜ（＾～＾）！
            if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F12)
            {
                Console.WriteLine($"{key.Key} が押されたぜ！（特殊処理）");

                // 例: F1でヘルプ、F5でクリア など
                if (key.Key == ConsoleKey.F1)
                {
                    Console.WriteLine("ヘルプを表示します...");
                }

                continue;
            }

            // ［エンターキー］が押されたら、そこまで入力された文字列を返します。
            if (key.Key == ConsoleKey.Enter || key.KeyChar == '\r' || key.KeyChar == '\n')
            {
                Console.WriteLine($"［エンターキー］が入力されたぜ（＾～＾）！");
                break;  // ループを抜ける
            }

            // 表示可能な文字（改行も拾ってしまうので、最後に行うこと）
            if (key.KeyChar != '\0' && !char.IsControl(key.KeyChar))
            {
                // 入力文字を表示
                Console.WriteLine(key.KeyChar);
                continue;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                Console.WriteLine($"バックスペースキーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.LeftArrow)
            {
                Console.WriteLine($"［←］キーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.UpArrow)
            {
                Console.WriteLine($"［↑］キーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.RightArrow)
            {
                Console.WriteLine($"［→］キーを押したぜ（＾～＾）");
                continue;
            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                Console.WriteLine($"［↓］キーを押したぜ（＾～＾）");
                continue;
            }

            // 他の制御キーも、欲しかったら実装してくれだぜ（＾～＾）
            // その他のキー入力は無視するぜ（＾～＾）！
        }

        return MuzREPL.MuzRequestType.None;
    }
}
