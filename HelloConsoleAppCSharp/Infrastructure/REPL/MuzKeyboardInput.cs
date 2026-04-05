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

        while (true)
        {



        }
    }
}
