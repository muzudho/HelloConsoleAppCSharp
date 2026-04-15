namespace HelloConsoleAppCSharp.Infrastructure.Models;

internal class MuzListCursorModel
{
    /// <summary>
    /// カーソルの前回位置
    /// </summary>
    internal int PreviousIndex { get; set; }

    /// <summary>
    /// カーソルの現在位置
    /// </summary>
    internal int SelectedIndex { get; set; }
}
