namespace HelloConsoleAppCSharp;

using System;

/// <summary>
///     <pre>
/// ［作りたいもの］と［作るのを便利にするもの］に分けたとき、  
/// 前者を［ドメインモデル］、後者を［インフラストラクチャー］と呼ぶことがあるぜ（＾～＾）！
/// 
/// それを踏まえて、［ドメインモデル］の方で使い、［インフラストラクチャー］の方では使わないデータを、詰め込むのがこの［プログラム・サービス］クラスだぜ（＾～＾）！
///     </pre>
/// </summary>
internal class ProgramService
{


    // ========================================
    // 構成
    // ========================================


    #region ［アプリケーションの起動日時］

    /// <summary>
    /// アプリケーションの起動日時を格納しておくぜ（＾～＾）！
    /// </summary>
    internal DateTime LaunchDateTime { get; set; }

    #endregion


    #region ［メッセージのキャッシュ］

    /// <summary>
    /// メッセージのキャッシュを格納しておくぜ（＾～＾）！
    /// </summary>
    internal Dictionary<string, string> MessageCache { get; set; } = new Dictionary<string, string>();

    #endregion


}
