namespace HelloConsoleAppCSharp.Controls;

using HelloConsoleAppCSharp.Infrastructure.REPL;
using System;
using System.Collections.Generic;
using System.Text;

internal class MuzMessageBoxControl
{


    // ========================================
    // 生成／破棄
    // ========================================


    internal MuzMessageBoxControl(
        List<string> messageList)
    {
        this.MessageList = messageList;
    }


    // ========================================
    // 窓口データメンバー
    // ========================================


    /// <summary>
    /// メッセージの一覧
    /// </summary>
    internal List<string> MessageList { get; init; } = default!;


    // ========================================
    // 窓口メソッド
    // ========================================


    public async Task<MuzRequestType> Enter()
    {
        return MuzRequestType.None;
    }
}
