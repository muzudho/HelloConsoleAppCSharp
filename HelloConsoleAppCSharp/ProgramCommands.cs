namespace HelloConsoleAppCSharp;

using HelloConsoleAppCSharp.Commands.Clear;
using HelloConsoleAppCSharp.Commands.ContinuePromptWarmup;
using HelloConsoleAppCSharp.Commands.CursorIncrementWarmup;
using HelloConsoleAppCSharp.Commands.KeyInputWarmup;
using HelloConsoleAppCSharp.Commands.PrintWarmup;
using HelloConsoleAppCSharp.Commands.SelectStartlWarmup;
using HelloConsoleAppCSharp.Commands.ShowErapsedTime;
using HelloConsoleAppCSharp.Commands.ShowStartMenu;
using HelloConsoleAppCSharp.Commands.ShowStartVerticalList;
using HelloConsoleAppCSharp.Commands.TitlePageWarmup;
using HelloConsoleAppCSharp.Commands.TypewriterEffectWarmup;
using HelloConsoleAppCSharp.Commands.WaitFor3SecondsWarmup;
using HelloConsoleAppCSharp.Controls;
using HelloConsoleAppCSharp.Features.Messages;
using HelloConsoleAppCSharp.Features.Prints;
using HelloConsoleAppCSharp.Infrastructure.ConsoleCustom;
using HelloConsoleAppCSharp.Infrastructure.REPL;
using HelloConsoleAppCSharp.Views;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// コマンド実行部
/// </summary>
internal static class ProgramCommands
{
    internal static async Task<MuzRequestType> ExecuteAsync(
        string command,
        IServiceProvider services)
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


            // ----------------------------------------
            // 第１章：インフラストラクチャーの作成
            // ----------------------------------------


            // ［コマンド呼出し］の動作確認
            case "command":
                return await ProgramCommands.ExecuteAsync(
                    command: arguments,
                    services: services);


            // ----------------------------------------
            // 第１章第２節：ファイルからメッセージを読込もう
            // ----------------------------------------


            // ［メッセージを配列で持つメッセージファイル］の読込の動作確認
            case "read-messages-2-warmup":
                {
                    var dictionary = MuzMessagesHelper.GetMessagesAsDictionary("Assets/Messages.json");
                    Console.WriteLine(dictionary["Msg_1"]);
                    return MuzRequestType.None;
                }

            // ［メッセージを配列で持つメッセージファイル］を読込み、キャッシュへ格納
            case "load-messages":
                {
                    var pgSvc = services.GetRequiredService<ProgramService>();
                    pgSvc.MessageCache = MuzMessagesHelper.GetMessagesAsDictionary("Assets/Messages.json");
                    return MuzRequestType.None;
                }

            // キャッシュした［メッセージ辞書］から、メッセージ取得
            case "get-message-warmup":
                {
                    var pgSvc = services.GetRequiredService<ProgramService>();
                    // コマンドの第二引数をメッセージのキーとして使うぜ（＾～＾）
                    var message = pgSvc.MessageCache.GetValueOrDefault(arguments, "メッセージが見つからないぜ（＾～＾）！");
                    Console.WriteLine(message);
                    return MuzRequestType.None;
                }

            // メッセージ取得
            case "get-message":
                {
                    // コマンドの第二引数をメッセージのキーとして使うぜ（＾～＾）
                    var message = MuzMessagesHelper.GetMessage(services, arguments);
                    Console.WriteLine(message);
                    return MuzRequestType.None;
                }


            // ----------------------------------------
            // 第２章：プログレスバーの作成
            // ----------------------------------------


            // ［文字色の変更］の動作確認
            case "change-color-warmup": return await MuzChangeColorWarmupCommand.ExecuteAsync(services);

            // ［色を指定してメッセージ表示］
            case "print-message-with-color": return await MuzPrintMessageWithColorCommand.ExecuteAsync(services, arguments);

            // ［位置も指定してメッセージ表示］
            case "print-message-with-location": return await MuzPrintMessageWithLocationCommand.ExecuteAsync(services, arguments);

            // ［カーソルの移動］の動作確認
            case "move-cursor-warmup":
                return await MuzMoveCursorWarmupCommand.ExecuteAsync(services);

            // ［コンソール］の文字を全部消します。
            case "clear": return await MuzClearCommand.ExecuteAsync(services);

            // ［３秒待つ］動作確認
            case "wait-for-3-seconds-warmup": return await MuzWaitFor3SecondsWarmupCommand.ExecuteAsync(services);

            // ［コンソールの横幅取得］
            case "get-console-size":
                Console.WriteLine($"コンソールの（横幅, 縦幅）：（{Console.WindowWidth}, {Console.WindowHeight}）");
                return MuzRequestType.None;

            // ［プログレスバー］作成の練習
            case "show-progress-bar-warmup": return await MuzShowProgressBarWarmupCommand.ExecuteAsync(services);


            // ----------------------------------------
            // 第３章：タイトル画面の作成
            // ----------------------------------------


            // ［キー入力］の動作確認
            case "key-input-warmup": return await MuzKeyInputWarmupCommand.ExecuteAsync(services);

            // ［フローティングボックス］の動作確認＜その１＞
            case "hide-message-box":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Blue,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        await MuzBoxViews.PrintAsync(
                            left: 0,
                            top: 1,
                            width: 80,
                            height: 7);
                    });
                return MuzRequestType.None;

            // ［フローティングボックス］の動作確認＜その２＞
            case "hide-start-box":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Blue,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        await MuzBoxViews.PrintAsync(
                            left: 20,
                            top: 15,
                            width: 40,
                            height: 5);
                    });
                return MuzRequestType.None;

            // ［枠付きのフローティングボックス］の動作確認＜その１＞
            case "show-message-box":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Blue,
                    bgColor: ConsoleColor.Cyan,
                    onColorChanged: async () =>
                    {
                        await MuzBoxViews.PrintDoubleBorderAsync(
                            left: 0,
                            top: 1,
                            width: 80,
                            height: 7);
                    });
                return MuzRequestType.None;

            // ［枠付きのフローティングボックス］の動作確認＜その２＞
            case "show-start-box":
                await MuzConsoleHelper.SetColorAsync(
                    fgColor: ConsoleColor.Blue,
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
            case "show-title":
                // 処理の後、カーソルの位置を戻す
                await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
                {
                    await MuzConsoleHelper.SetColorAsync(
                        fgColor: ConsoleColor.Black,
                        bgColor: ConsoleColor.Cyan,
                        onColorChanged: async () =>
                        {
                            // 画面の真ん中辺りにタイトルを表示するとかっこいい。
                            var wallWidth = 80; // 壁の幅
                            var wallHeight = 25; // 壁の高さ
                            var titleStr = "Hello Console App C#";
                            var titleLeft = (wallWidth - titleStr.Length) / 2;  // 漢字は横幅計算が難しいので、今回は半角英字だけのタイトルにします。
                            var titleTop = wallHeight / 2;
                            await MuzLabelViews.PrintAsync(
                                left: titleLeft,
                                top: titleTop,
                                text: titleStr);
                        });
                });
                return MuzRequestType.None;
            case "show-credit":
                // 処理の後、カーソルの位置を戻す
                await MuzConsoleHelper.ResetCursorLocationAfterExecute(async () =>
                {
                    await MuzConsoleHelper.SetColorAsync(
                        fgColor: ConsoleColor.Black,
                        bgColor: ConsoleColor.Cyan,
                        onColorChanged: async () =>
                        {
                            // 画面の下辺辺りに、制作年、開発者を表示するとかっこいい。
                            var wallWidth = 80; // 壁の幅
                            var wallHeight = 25; // 壁の高さ
                            var creditStr = "(C) 2026 by Muzudho ; MIT License";
                            var creditLeft = (wallWidth - creditStr.Length) / 2;
                            var creditTop = wallHeight - 1;
                            await MuzLabelViews.PrintAsync(
                                left: creditLeft,
                                top: creditTop,
                                text: creditStr);
                        });
                });
                return MuzRequestType.None;

            // ［垂直の箇条書き］を表示する練習
            case "show-start-vertical-list": return await MuzShowStartVerticalListCommand.ExecuteAsync(services);

            // ［点滅ラベル］の動作確認
            case "select-start-warmup": return await MuzSelectStartWarmupCommand.ExecuteAsync(services);

            // ［カーソルとインクリメント］の動作確認
            case "cursor-increment-warmup": return await MuzCursorIncrementWarmupCommand.ExecuteAsync(services);

            // ［メニュー］の動作確認
            case "show-start-menu": return await MuzShowStartMenuCommand.ExecuteAsync(services);

            // ［壁面の塗り潰し］の動作確認
            case "show-wall":
                int wallHeight = 25;
                await MuzWallViews.PrintAsync(
                    wallWidth: 80,
                    wallHeight: wallHeight,
                    wallColor: ConsoleColor.Cyan);
                Console.SetCursorPosition(0, wallHeight);   // 改行
                return MuzRequestType.None;

            // ［アプリケーション起動からの経過時間を表示する］の練習
            case "show-erapsed-time": return await MuzShowErapsedTimeCommand.ExecuteAsync(services);

            // ［タイトル風ページ］の描画練習
            case "title-page-warmup": return await MuzTitlePageWarmupCommand.ExecuteAsync(services);

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
                                services: services,
                                left: Console.CursorLeft,
                                top: Console.CursorTop,
                                message: "タイプライターエフェクトのウォームアップだぜ（＾～＾）\n複数行にも対応だぜ（＾～＾）！");
                        });
                    return result;
                }

            // ［コンティニュープロンプト］の動作確認
            case "continue-prompt-warmup":
                return await MuzContinuePromptWarmupCommand.ExecuteAsync(
                services: services,
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
                return await messageBoxControl.Enter(services);

            // ［メッセージ］ファイルの読込の動作確認
            case "read-messages-warmup": return await MuzReadMessagesWarmupCommand.ExecuteAsync(services);

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

            case "experiment":
                Console.WriteLine("実験中だぜ（＾～＾）！");
                //new MuzExperiment();
                // Windows 専用： Console.SetWindowSize(120, Console.WindowHeight);
                return MuzRequestType.None;


            default:
                Console.WriteLine($"知らないコマンドだぜ: {command}");
                return MuzRequestType.None;
        }
    }
}
