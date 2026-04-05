namespace HelloConsoleAppCSharp.Infrastructure.REPL;

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
///     <pre>
/// `Console.ReadLine();` は、エンターキーを押すまでに入力された文字列を取得することができますが、
/// Shiftキーや、↑←↓→キー、ファンクション・キーなどの特殊なキーの入力を判別することはできません。
/// 
/// 特殊なキーの入力を判別するためには、`Console.ReadKey();` を使用する必要がありますが、
/// これもまた、単一のキー入力を取得するものであり、複数のキーの組み合わせや、キーの状態（押されているかどうか）を判別することはできません。
/// 
/// そこで、特殊なキーの入力を判別するために、 `Console.ReadKey();` を使って、少し手間をかけたプログラムを作ります。
///     </pre>
/// </summary>
internal static class MuzKeyboardInput
{
    /// <summary>
    /// ReadLineっぽく文字列を入力しつつ、F1〜F12などの特殊キーを検知できる。
    /// ファンクションキーが押されたら null を返す（または任意の処理）。
    /// Enterで入力文字列を返す。
    /// </summary>
    public static string? ReadInput()
    {
        StringBuilder inputString = new StringBuilder();
        int cursorPos = 0;  // カーソル位置（簡易版）

        while (true)
        {
            // 押されたキーを取得。
            // true : エコー（表示）しない。
            ConsoleKeyInfo key = Console.ReadKey(true);

            // === ファンクションキー検知 ===
            if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F12)
            {
                // ここでFキーの処理をしたい場合はここで分岐
                Console.WriteLine();  // 改行
                Console.WriteLine($"{key.Key} が押されたぜ！（特殊処理）");

                // 例: F1でヘルプ、F5でクリア など
                if (key.Key == ConsoleKey.F1)
                {
                    Console.WriteLine("ヘルプを表示します...");
                }

                return null;  // または特殊キーを示す特別な値を返す
            }

            // === 通常の文字入力処理 ===
            // ［エンターキー］が押されたら、そこまで入力された文字列を返します。
            if (key.Key == ConsoleKey.Enter)
            {
                return inputString.ToString();
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (inputString.Length > 0 && cursorPos > 0)
                {
                    inputString.Remove(cursorPos - 1, 1);
                    cursorPos--;

                    // 表示を修正（Backspaceで消す）
                    Console.Write("\b \b");
                }
            }
            else if (key.KeyChar != '\0')  // 通常の文字
            {
                inputString.Insert(cursorPos, key.KeyChar);
                cursorPos++;

                // 入力文字を表示
                Console.Write(key.KeyChar);
            }
            // 矢印キーなどもここで追加可能（LeftArrow, RightArrowなど）
        }
    }
}
