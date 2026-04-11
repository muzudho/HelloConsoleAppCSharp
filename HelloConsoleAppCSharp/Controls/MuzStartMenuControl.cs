namespace HelloConsoleAppCSharp.Controls;

using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;

internal class MuzStartMenuControl
{


    // ========================================
    // 生成／破棄
    // ========================================


    internal MuzStartMenuControl(
        List<string> menuItems,
        List<int> stopYList,
        int selectedIndex = 0)
    {
        this.MenuItems = menuItems;
        this.StopYList = stopYList;
        this.SelectedIndex = selectedIndex;
    }


    // ========================================
    // 窓口データメンバー
    // ========================================


    /// <summary>
    /// 項目の一覧
    /// </summary>
    internal List<string> MenuItems { get; init; } = default!;

    /// <summary>
    /// カーソルの停止Ｙ位置のリスト
    /// </summary>
    internal List<int> StopYList { get; init; } = default!;

    /// <summary>
    ///     <pre>
    /// 選択している項目が何番目か。０から始まる数
    /// 
    ///     - ［エスケープキー］が押されたら、ヌルにするぜ（＾～＾）！
    ///     </pre>
    /// </summary>
    internal int? SelectedIndex { get; private set; } = default!;


    // ========================================
    // 窓口メソッド
    // ========================================


    public async Task<MuzRequestType> Enter()
    {
        // 色替え
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                // ボックス表示
                await MuzBoxViews.PrintDoubleBorderBoxAsync(
                    left: 20,
                    top: 15,
                    width: 40,
                    height: 5);

                // メニュー項目表示
                await MuzVerticalMenus.PrintMenuAsync(
                    left: 38,
                    top: 16,
                    items: this.MenuItems.ToArray());

                // 📍 NOTE:
                //
                //      操作を説明しておきましょう。
                //
                Console.WriteLine("［↑］［↓］キーでカーソルを移動、［エンターキー］で確定、［エスケープキー］でキャンセルだぜ（＾～＾）...");

                int previousIndex = 0;   // カーソルの前回位置

                while (true)  // 無限ループ
                {
                    // 📍 NOTE:
                    //
                    //      一定間隔で点滅するカーソル（ブリンカー）を表示するぜ（＾～＾）！
                    //
                    MuzWidgets.PrintBlinkingText(
                        text: " ",  // ホワイトスペース
                        left: 36,
                        top: this.StopYList[previousIndex],
                        isVisible: false);  // 常にホワイトスペースを表示
                    MuzWidgets.PrintBlinkingText(
                        text: "▶",  // 右向きの三角形は、半角のようだ。
                        left: 36,
                        top: this.StopYList[this.SelectedIndex!.Value],
                        isVisible: (DateTime.Now.Millisecond / 500) % 2 == 0); // 0.5秒ごとに点滅

                    // キー入力がない場合は、少し待ってからループの先頭に戻るぜ（＾～＾）！
                    if (!Console.KeyAvailable)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(16));    // およそ１／６０秒で画面更新（＾～＾）
                        continue;
                    }

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

                    // ［エンターキー］が押されたら、ループを抜けます。this.SelectedIndexは、選択している項目の番号（０から始まる数）を保持しているぜ（＾～＾）！
                    if (key.Key == ConsoleKey.Enter || key.KeyChar == '\r' || key.KeyChar == '\n')
                    {
                        break;  // ループを抜ける
                    }

                    // ［エスケープキー］が押されたら、ループを抜けます。
                    if (key.Key == ConsoleKey.Escape)
                    {
                        this.SelectedIndex = null;
                        break;  // ループを抜ける
                    }

                    // ［↑］キーが押されたら、カーソルを上に移動します。
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        previousIndex = this.SelectedIndex!.Value;
                        this.SelectedIndex--;
                        if (this.SelectedIndex < 0)
                        {
                            this.SelectedIndex = this.StopYList.Count() - 1;
                        }
                        continue;
                    }

                    // ［↓］キーが押されたら、カーソルを下に移動します。
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        previousIndex = this.SelectedIndex!.Value;
                        this.SelectedIndex++;
                        if (this.SelectedIndex >= this.StopYList.Count())
                        {
                            this.SelectedIndex = 0;
                        }
                        continue;
                    }

                    // その他のキー入力は無視するぜ（＾～＾）！
                }
            });

        return MuzRequestType.None;
    }
}
