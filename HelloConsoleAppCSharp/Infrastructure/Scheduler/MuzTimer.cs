namespace HelloConsoleAppCSharp.Infrastructure.Scheduler;

/// <summary>
/// 一定間隔で、何かの処理を実行するためのクラスです。
/// </summary>
internal class MuzTimer
{


    // ========================================
    // 生成／破棄
    // ========================================


    public MuzTimer(TimeSpan timeSpan)
    {
        this.Timer = new PeriodicTimer(timeSpan);
    }


    // ========================================
    // 内部データメンバー
    // ========================================


    private PeriodicTimer Timer { get; init; } = default!;

        // キャンセルしたいときはこれを用意（任意）
    private CancellationTokenSource CTSource = new CancellationTokenSource();


    // ========================================
    // 窓口メソッド
    // ========================================


    public void Run(
        Func<Task> update)
    {
        // 終わりを待ちません
        _ = Task.Run(async () =>
        {
            try
            {
                while (await this.Timer.WaitForNextTickAsync(this.CTSource.Token))
                {
                    // TODO: ここに、タイマーが発火したときの処理を書いていきます。
                    await update();
                }
            }
            catch (OperationCanceledException)
            {
                //Console.WriteLine("タイマー正常終了～");
            }
        });
    }
}
