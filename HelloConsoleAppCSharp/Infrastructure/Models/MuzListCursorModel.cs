namespace HelloConsoleAppCSharp.Infrastructure.Models;

internal class MuzListCursorModel
{
    internal MuzListCursorModel(int size)
    {
        this.Size = size;
    }

    /// <summary>
    /// リスト・サイズ
    /// </summary>
    internal int Size { get; set; }

    /// <summary>
    /// カーソルの前回位置
    /// </summary>
    internal int PreviousIndex { get; set; }

    /// <summary>
    /// カーソルの現在位置
    /// </summary>
    internal int SelectedIndex { get; set; }

    internal void Add(int offset)
    {
        this.PreviousIndex = this.SelectedIndex;
        this.SelectedIndex += offset;

        // 終わりを超えたら、始まりに戻します。
        if (this.SelectedIndex > this.Size)
        {
            this.SelectedIndex = 0;
        }
        else if (this.SelectedIndex < 0)
        {
            this.SelectedIndex = this.Size - 1;
        }
    }
}
