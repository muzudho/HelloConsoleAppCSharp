# 枠付きの欄（ボックス）の描画



## フォルダーと空ファイルの作成

以下のフォルダーと空ファイルを用意してください。  

```plaintext
📁 HelloConsoleAppCSharp
+-- 📁 Commands
|   +-- 📁 TitlePageWarmup
|       +-- 📄 MuzTitlePageWarmupCommand.cs        # 既存ファイル
+-- 📁 Views
	+-- 📄 MuzBoxViews.cs
```


## 既存の［title-page-warmup］コマンドの編集

📄 `Commands/TitlePageWarmup/MuzTitlePageWarmupCommand.cs`:  

```csharp
～前後略～

internal static class MuzTitlePageWarmupCommand
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        ProgramContext pgContext)
    {
        MuzREPL.IsPromptVisible = false; // プロンプトは消す。
        await MuzPageLayouts.PrintTitlePageAsync();

        // 📍 NOTE:
        //
        //      ここで、ボックスを表示してみようぜ（＾～＾）！
        //      方眼紙などに画面の想像図を描いてから、位置とサイズを測って、コーディングしろだぜ（＾～＾）！
        //      例えば、以下のような感じだぜ（＾～＾）！
        //
        //              4   8  12  16  20  24  28  32  36  40  44  48  52  56  60  64  68  72  76  80
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //        5 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       10 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       15 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       20 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       25 |                                                                               |
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //
        //      👆　上のは白紙（＾～＾）
        //
        //              4   8  12  16  20  24  28  32  36  40  44  48  52  56  60  64  68  72  76  80
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //        5 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       10 +                                                                               +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                  20                                      60                   |
        //       15 +                15 ╔══════════════════════════════════════╗                    +
        //          |                   ║                                      ║                    |
        //          |                   ║                                      ║                    |
        //          |                   ║                                      ║                    |
        //          |                   ╚══════════════════════════════════════╝                    |
        //       20 +                20                                                             +
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //          |                                                                               |
        //       25 |                                                                               |
        //          +---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+
        //
        //      👆　位置を決める（＾～＾）
        //
        await MuzConsoleHelper.SetColorAsync(
            fgColor: ConsoleColor.Black,
            bgColor: ConsoleColor.Cyan,
            onColorChanged: async () =>
            {
                await MuzBoxViews.PrintDoubleBorderAsync(
                    left: 20,
                    top: 15,
                    width: 40,
                    height: 5);
            });

        return MuzRequestType.None;
    }
}
```

これで、このコンソール・アプリケーションを起動し、 `title-page-warmup` と入力すると、ボックスが表示されるようになります。  
