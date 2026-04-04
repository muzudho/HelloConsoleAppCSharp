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

        // 次に、シアン色の背景色で、使用する［固定サイズ］の免責を塗りつぶします。［可変］サイズは難しいので、ここでは扱いません。
        Console.BackgroundColor = ConsoleColor.Cyan;
        int pageWidth = 80;
        int pageHeight = 25;
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < pageHeight; y++)
        {
            for (int x = 0; x < pageWidth; x++)
            {
                Console.Write(' '); // 全体を決め打ちでもいいが、とりあえず１文字ずつプリントする。
            }
            Console.WriteLine();    // 改行
        }

        // 画面の真ん中辺りにタイトルを表示するとかっこいい。
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        var title = "Hello Console App C#";
        var titleLeft = (pageWidth - title.Length) / 2;  // 漢字は横幅計算が難しいので、今回は半角英字だけのタイトルにします。
        var titleTop = pageHeight / 2;
        Console.SetCursorPosition(titleLeft, titleTop);
        Console.Write(title);

        //// 次に、シアン色の背景、白文字で、デコレーションします。

        //Console.WriteLine("***************************************");
        //Console.WriteLine("*                                     *");
        //Console.WriteLine("*         Hello Console App!          *");
        //Console.WriteLine("*                                     *");
        //Console.WriteLine("***************************************");
    }
}
