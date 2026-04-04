namespace HelloConsoleAppCSharp.Views;

/// <summary>
/// コンソール画面を、１画面に見立てて、枠組みを表示するためのものです。
/// </summary>
internal class MuzPageLayouts
{
    public static void PrintTitlePage()
    {
        // いったん、背景色を黒にして、画面全体を塗りつぶします。
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        // 次に、シアン色背景、白文字で、デコレーションします。
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("***************************************");
        Console.WriteLine("*                                     *");
        Console.WriteLine("*         Hello Console App!          *");
        Console.WriteLine("*                                     *");
        Console.WriteLine("***************************************");
    }
}
