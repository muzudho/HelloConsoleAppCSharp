namespace HelloConsoleAppCSharp;

using HelloConsoleAppCSharp.Commands.BlinkLabelWarmup;
using HelloConsoleAppCSharp.Commands.ContinuePromptWarmup;
using HelloConsoleAppCSharp.Commands.CursorWarmup;
using HelloConsoleAppCSharp.Commands.KeyInputWarmup;
using HelloConsoleAppCSharp.Commands.LaunchTimer;
using HelloConsoleAppCSharp.Commands.MenuWarmup;
using HelloConsoleAppCSharp.Commands.PrintWarmup;
using HelloConsoleAppCSharp.Commands.TitlePageWarmup;
using HelloConsoleAppCSharp.Commands.TypewriterEffectWarmup;
using HelloConsoleAppCSharp.Controls;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;

/// <summary>
/// コマンド実行部
/// </summary>
internal static class ProgramCommands
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        string command,
        ProgramContext pgContext)
    {
        // 入力が空白だけだったら、無視するぜ（＾～＾）
        if (string.IsNullOrWhiteSpace(command)) return MuzRequestType.None;

        // 最初の半角空白でコマンドと引数を分割するぜ（＾～＾）
        var parts = command.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
        var commandName = parts[0];
        var arguments = parts.Length > 1 ? parts[1] : string.Empty;

        // ここに君のコマンド処理を書く。
        // `exit` は REPL 内で処理されているから、ここでは処理されないぜ（＾～＾）！
        switch (commandName)
        {
            case "exit":
                Console.WriteLine("REPLを終了するぜ（＾～＾）");
                return MuzRequestType.Exit;

            case "hello":
                Console.WriteLine("こんにちは（＾～＾）！");
                return MuzRequestType.None;

            case "color":
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("文字色を変えたよ！");
                Console.ResetColor();
                return MuzRequestType.None;

            //case "sound":
            //    Console.WriteLine("音を鳴らしてみるぜ（＾～＾）");
            //    //Console.Beep(800, 300);   // Windows 限定。

            //    //SystemSounds は無かった。
            //    //SystemSounds.Beep.Play();      // 標準のビープ
            //    //SystemSounds.Asterisk.Play();  // 情報音（！マークっぽい）
            //    //SystemSounds.Exclamation.Play(); // 警告音
            //    //SystemSounds.Hand.Play();      // エラー音（×マーク）
            //    //SystemSounds.Question.Play();  // 質問音

            //    //これも無かった。
            //    //SoundPlayer player = new SoundPlayer(@"C:\Windows\Media\Alarm01.wav"); // WAVファイル指定
            //    //player.Play();        // 非同期で鳴らす（PlaySync()で同期）
            //    //                      // player.PlaySync(); // 音が終わるまで待つ

            //    //NuGet を調べた方がいい？
            //    //dotnet add package NetCoreAudio

            //    return MuzRequestType.None;

            // ［コンソール出力］の動作確認
            case "print-warmup": return await MuzPrintWarmupCommand.ExecuteAsync();

            // ［フローティングボックス］の動作確認
            case "floating-box-warmup":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Blue,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        await MuzBoxViews.PrintAsync(
                            left: 1,
                            top: 2,
                            width: 78,
                            height: 5);
                    });
                return MuzRequestType.None;

            // ［枠付きのフローティングボックス］の動作確認
            case "double-boarder-floating-box-warmup":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Blue,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        await MuzBoxViews.PrintDoubleBorderAsync(
                            left: 1 - 1,
                            top: 2 - 1,
                            width: 78 + 2,
                            height: 5 + 2);
                    });
                return MuzRequestType.None;

            // ［フローティングラベル］の動作確認
            case "floating-label-warmup":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Black,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        await MuzLabelViews.PrintAsync(
                            left: 1,
                            top: 2,
                            text: "フローティングラベルのウォームアップだぜ（＾～＾）！\n複数行にも対応だぜ（＾～＾）！");
                    });
                return MuzRequestType.None;

            // ［点滅ラベル］の動作確認
            case "blink-label-warmup": return await MuzBlinkLabelWarmupCommand.ExecuteAsync();

            // ［壁面の塗り潰し］の動作確認
            case "wall-warmup":
                int wallHeight = 25;
                await MuzWallViews.PrintAsync(
                    wallWidth: 80,
                    wallHeight: wallHeight,
                    wallColor: ConsoleColor.Cyan);
                Console.SetCursorPosition(0, wallHeight);   // 改行
                return MuzRequestType.None;

            // ［アプリケーション起動からの経過時間を表示する］の練習
            case "launch-timer-warmup": return await MuzLaunchTimerWarmupCommand.ExecuteAsync(pgContext);

            // ［タイトル風ページ］の描画練習
            case "title-page-warmup": return await MuzTitlePageWarmupCommand.ExecuteAsync(pgContext);

            // ［キー入力］の動作確認
            case "key-input-warmup": return await MuzKeyInputWarmupCommand.ExecuteAsync();

            // ［カーソル］の動作確認
            case "cursor-warmup": return await MuzCursorWarmupCommand.ExecuteAsync();

            // ［メニュー］の動作確認
            case "menu-warmup": return await MuzMenuWarmupCommand.ExecuteAsync(pgContext);

            // ［タイプライター効果］の動作確認
            case "typewriter-effect-warmup":
                {
                    MuzRequestType result = MuzRequestType.None;
                    await MuzConsoleHelper.SetColorAsync(
                        fgColor: ConsoleColor.Black,
                        bgColor: ConsoleColor.Cyan,
                        onColorChanged: async () =>
                        {
                            result = await MuzTypewriterEffectWarmupCommand.ExecuteAsync(
                                left: Console.CursorLeft,
                                top: Console.CursorTop,
                                message: "タイプライターエフェクトのウォームアップだぜ（＾～＾）\n複数行にも対応だぜ（＾～＾）！");
                        });
                    return result;
                }

            // ［コンティニュープロンプト］の動作確認
            case "continue-prompt-warmup":
                return await MuzContinuePromptWarmupCommand.ExecuteAsync(
                messageList: new List<string>
                {
                                "続きのプロンプトのウォームアップだぜ（＾～＾）！\nこれも複数行に対応してるぜ（＾～＾）！",
                                "次のメッセージだぜ（＾～＾）！",
                                "最後のメッセージだぜ（＾～＾）！"
                });

            // ［メッセージボックス］の動作確認
            case "message-box-warmup":
                var messageBoxControl = new MuzMessageBoxControl(
                    messageList: new List<string>
                    {
                                    "メッセージボックスのウォームアップだぜ（＾～＾）！\nこれも複数行に対応してるぜ（＾～＾）！",
                                    "次のメッセージだぜ（＾～＾）！",
                                    "最後のメッセージだぜ（＾～＾）！"
                    });
                return await messageBoxControl.Enter();

            // ［コマンド呼出し］の動作確認
            case "command":
                return await ProgramCommands.ExecuteAsync(
                    command: arguments,
                    pgContext: pgContext);

            default:
                Console.WriteLine($"知らないコマンドだぜ: {command}");
                return MuzRequestType.None;
        }
    }
}
