namespace HelloConsoleAppCSharp.Core.Infrastructure;

/// <summary>
/// コンソールの操作でよく使う機能をまとめたクラス
/// </summary>
public static class MuzConsoleHelper
{


    // ========================================
    // 窓口メソッド
    // ========================================


    /// <summary>
    ///     <pre>
    /// 色名からオブジェクトを取得するぜ（＾～＾）！
    ///     
    ///     📍 NOTE:
    ///     
    ///         全部で16色あるよ：
    ///     
    ///         Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
    ///         DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
    ///         
    ///         指定しない場合は Default を入れてね（＾～＾）！
    ///     </pre>
    /// </summary>
    /// <param name="name">色の名前</param>
    /// <returns>対応するConsoleColorオブジェクト、またはnull（Defaultの場合）</returns>
    public static ConsoleColor? GetColorByName(string name)
    {
        switch (name)
        {
            case "Black":
                return ConsoleColor.Black;

            case "DarkBlue":
                return ConsoleColor.DarkBlue;

            case "DarkGreen":
                return ConsoleColor.DarkGreen;

            case "DarkCyan":
                return ConsoleColor.DarkCyan;

            case "DarkRed":
                return ConsoleColor.DarkRed;

            case "DarkMagenta":
                return ConsoleColor.DarkMagenta;

            case "DarkYellow":
                return ConsoleColor.DarkYellow;

            case "Gray":
                return ConsoleColor.Gray;

            case "DarkGray":
                return ConsoleColor.DarkGray;

            case "Blue":
                return ConsoleColor.Blue;

            case "Green":
                return ConsoleColor.Green;

            case "Cyan":
                return ConsoleColor.Cyan;

            case "Red":
                return ConsoleColor.Red;

            case "Magenta":
                return ConsoleColor.Magenta;

            case "Yellow":
                return ConsoleColor.Yellow;

            case "White":
                return ConsoleColor.White;

            case "Default": // thru
            default:
                return null;
        }
    }
}
