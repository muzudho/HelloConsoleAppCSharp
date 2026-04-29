namespace HelloConsoleAppCSharp.Commands.WaitFor3SecondsWarmup;

using HelloConsoleAppCSharp.Core.Infrastructure;

internal class MuzWaitFor3SecondsWarmupCommand
{
    internal static async Task<MuzREPLRequestType> ExecuteAsync(
        IServiceProvider services)
    {
        Console.WriteLine("３秒待てだぜ（＾～＾）");
        await Task.Delay(TimeSpan.FromSeconds(3));
        Console.WriteLine("お待たせだぜ～（＾▽＾）");

        return MuzREPLRequestType.None;
    }
}
