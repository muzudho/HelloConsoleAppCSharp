namespace HelloConsoleAppCSharp.Core.Infrastructure;

/// <summary>
/// コンソールの操作でよく使うメソッドをまとめたクラス
/// </summary>
public static class MuzConsole
{


    // ========================================
    // 構成
    // ========================================


    #region ［色関連］


    /// <summary>
    ///     <pre>
    /// 前景色、背景色を一時的に変更して、指定された処理を実行します。
    ///     
    ///     📍 NOTE:
    ///     
    ///         全部で16色あるよ：
    ///     
    ///         Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray
    ///         DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
    ///     
    ///         Console.Clear(); を呼ぶと、ウィンドウ全体の背景色も変わる（現在のBackgroundColorが適用される）。
    ///         ANSIエスケープシーケンス を使えば、真のRGBカラー（24bit）や下線・太字なども使えるようになる。
    ///     
    ///     </pre>
    /// </summary>
    /// <param name="onColorChanged">色を変更した後に実行する処理</param>
    /// <param name="fgColor">設定する前景色</param>
    /// <param name="bgColor">設定する背景色</param>
    public static async Task RunWithColorAsync(
        Func<Task> onColorChanged,
        ConsoleColor? fgColor = null,
        ConsoleColor? bgColor = null)
    {
        // 現在の色を記憶
        var oldFgColor = Console.ForegroundColor;
        var oldBgColor = Console.BackgroundColor;

        // 色を設定
        Console.ForegroundColor = fgColor ?? Console.ForegroundColor;
        Console.BackgroundColor = bgColor ?? Console.BackgroundColor;

        // 色を変更した後の処理を実行
        await onColorChanged();

        // 色を戻す
        Console.ForegroundColor = oldFgColor;
        Console.BackgroundColor = oldBgColor;
    }


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
    public static ConsoleColor? ParseColor(string name)
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


    #endregion

    #region ［メッセージ表示］


    /// <summary>
    /// 前景色、背景色を指定してメッセージ表示
    /// </summary>
    /// <param name="foregroundColor">前景色</param>
    /// <param name="backgroundColor">背景色</param>
    /// <param name="message">メッセージ</param>
    /// <returns></returns>
    public static async Task WriteLineAsync(
        ConsoleColor? foregroundColor,
        ConsoleColor? backgroundColor,
        string message)
    {
        // 色を一時的に変更
        await MuzConsole.RunWithColorAsync(
            fgColor: foregroundColor ?? Console.ForegroundColor,
            bgColor: backgroundColor ?? Console.BackgroundColor,
            onColorChanged: async () =>
            {
                // メッセージを表示
                Console.WriteLine(message);
            });
    }


    /// <summary>
    /// 左位置、上位置、前景色、背景色を指定してメッセージ表示
    /// </summary>
    /// <param name="left">左位置</param>
    /// <param name="top">上位置</param>
    /// <param name="foregroundColor">前景色</param>
    /// <param name="backgroundColor">背景色</param>
    /// <param name="message">メッセージ</param>
    /// <returns></returns>
    public static async Task WriteLineAsync(
        int left,
        int top,
        ConsoleColor? foregroundColor,
        ConsoleColor? backgroundColor,
        string message)
    {
        // 処理の後、カーソルを元の位置に戻す
        await MuzConsole.PreserveCursorPositionAsync(async () =>
        {
            Console.SetCursorPosition(left, top);

            // トークンの３つ目以降を次のコマンドに渡すぜ（＾～＾）
            await MuzConsole.WriteLineAsync(
                foregroundColor: foregroundColor,
                backgroundColor: backgroundColor,
                message: message);
        });
    }


    #endregion
    
    #region ［カーソル位置関連］


    /// <summary>
    /// 処理が終わった後、カーソルを元の位置に戻します。
    /// </summary>
    /// <returns></returns>
    public static async Task PreserveCursorPositionAsync(
        Func<Task> executeAsync)
    {
        // 現在のカーソル位置を記憶
        var oldLeft = Console.CursorLeft;
        var oldTop = Console.CursorTop;

        // 処理を実行
        await executeAsync();

        // カーソルの位置を戻す
        Console.SetCursorPosition(oldLeft, oldTop);
    }


    #endregion

    #region ［ボックス表示］


    // 矩形の塗り潰し領域を［ボックス］と呼ぶことにします。


    /// <summary>
    /// ［ボックス］表示
    /// </summary>
    /// <param name="left"></param>
    /// <param name="top"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static async Task FillRectAsync(
         int left,
         int top,
         int width,
         int height)
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsole.PreserveCursorPositionAsync(async () =>
        {
            // 次に、［固定サイズ］の面積をホワイトスペースで埋めます。
            for (int dy = 0; dy < height; dy++)
            {
                Console.SetCursorPosition(left, top + dy);

                for (int dx = 0; dx < width; dx++)
                {
                    Console.Write(' '); // ホワイトスペース
                }
                Console.WriteLine();    // 改行
            }
        });
    }

    /// <summary>
    ///     <pre>
    /// ［ボックス］表示
    /// 
    ///     - 色指定も一緒にするぜ（＾～＾）！
    ///     </pre>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="top"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="fgColor"></param>
    /// <param name="bgColor"></param>
    /// <returns></returns>
    public static async Task FillRectAsync(
         int left,
         int top,
         int width,
         int height,
         ConsoleColor? fgColor = null,
         ConsoleColor? bgColor = null)
    {
        await MuzConsole.RunWithColorAsync(
            fgColor: fgColor,
            bgColor: bgColor,
            onColorChanged: async () =>
            {
                await FillRectAsync(left, top, width, height);
            });
    }


    /// <summary>
    ///     <pre>
    /// 下図のような感じの罫線を表示します。
    /// 
    ///     ╔══════╗
    ///     ║      ║
    ///     ║      ║
    ///     ╚══════╝
    ///     </pre>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="top"></param>
    /// <param name="width">2 以上としてください。</param>
    /// <param name="height">2 以上としてください。</param>
    public static async Task DrawDoubleBorderRectAsync(
        int left,
        int top,
        int width,
        int height)
    {
        // 処理の後、カーソルの位置を戻す
        await MuzConsole.PreserveCursorPositionAsync(async () =>
        {
            // ダブル・ボーダーを表示するための文字
            char topLeft = '╔';
            char topRight = '╗';
            char bottomLeft = '╚';
            char bottomRight = '╝';
            char horizontal = '═';
            char vertical = '║';

            // 左上隅
            Console.SetCursorPosition(left, top);
            Console.Write(topLeft);

            // 上辺
            for (int dx = 0; dx < width - 2; dx++)
            {
                Console.Write(horizontal);
            }

            // 右上隅
            Console.Write(topRight);
            Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行

            // 上辺下辺を除いた中段行
            for (int dy = 0; dy < height - 2; dy++)
            {
                // 左辺
                Console.Write(vertical);

                // 中身（空白）
                for (int dx = 0; dx < width - 2; dx++)
                {
                    Console.Write(' ');
                }

                // 右辺
                Console.Write(vertical);
                Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行
            }

            // 左下隅
            Console.Write(bottomLeft);

            // 下辺
            for (int dx = 0; dx < width - 2; dx++)
            {
                Console.Write(horizontal);
            }

            // 右下隅
            Console.Write(bottomRight);
            Console.SetCursorPosition(left, Console.CursorTop + 1);   // 改行
        });
    }


    #endregion

    #region ［テキスト点滅］


    /// <summary>
    ///     <pre>
    /// テキストを点滅させます。
    /// 
    /// 仕組みとしては、２組みの［前景色、背景色］を用意して、一定間隔で切り替えることで点滅させる感じだぜ（＾～＾）！
    ///     </pre>
    /// </summary>
    /// <param name="onColorChanged">色を変更した後に実行する処理</param>
    /// <param name="fgColor">設定する前景色</param>
    /// <param name="bgColor">設定する背景色</param>
    public static async Task RunWithBlinkColorsAsync(
        ConsoleColor fgColor1,
        ConsoleColor bgColor1,
        ConsoleColor fgColor2,
        ConsoleColor bgColor2,
        bool isColor2,
        Func<Task> onColorChanged)
    {
        // 現在の色を記憶
        var oldFgColor = Console.ForegroundColor;
        var oldBgColor = Console.BackgroundColor;

        // 色を設定
        if (isColor2)
        {
            Console.ForegroundColor = fgColor2;
            Console.BackgroundColor = bgColor2;
        }
        else
        {
            Console.ForegroundColor = fgColor1;
            Console.BackgroundColor = bgColor1;
        }

        // 色を変更した後の処理を実行
        await onColorChanged();

        // 色を戻す
        Console.ForegroundColor = oldFgColor;
        Console.BackgroundColor = oldBgColor;
    }


    #endregion


}
