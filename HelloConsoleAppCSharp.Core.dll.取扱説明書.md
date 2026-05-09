# HelloConsoleAppCSharp.Core.dll 取扱説明書

`HelloConsoleAppCSharp.Core.dll` は、C# のコンソールアプリケーションで使うための、軽量な描画補助ライブラリーです。  
特に、コンソールゲームや、枠付きの画面、メッセージ欄、簡単な UI 表示を作る用途を想定しています。  


## 想定している使い方

このライブラリーは、コンソールへ直接描画する方式を採っています。  
自前の画面バッファーは持たず、各メソッドが `System.Console` へ直接書き込みます。  

向いている用途:

- コンソールゲームの簡易 UI
- 枠付き画面
- メッセージ欄
- REPL アプリの表示装飾
- 学習用のコンソール描画実験


## 主要クラス

### `HelloConsoleAppCSharp.Core.Infrastructure.MuzConsole`

コンソール描画でよく使うメソッドをまとめた静的クラスです。  


## 主なメソッド

### 色関連

#### `RunWithColorAsync(...)`

前景色、背景色を一時的に変更し、指定された処理を実行したあと、元の色へ戻します。  

用途:

- 色付き文字表示
- 色付き矩形描画
- 一時的な色変更

---

#### `ParseColor(string name)`

文字列から `ConsoleColor?` を取得します。  
`Default` のときは `null` を返します。  

対応色:

- `Black`
- `DarkBlue`
- `DarkGreen`
- `DarkCyan`
- `DarkRed`
- `DarkMagenta`
- `DarkYellow`
- `Gray`
- `DarkGray`
- `Blue`
- `Green`
- `Cyan`
- `Red`
- `Magenta`
- `Yellow`
- `White`
- `Default`


### 文字表示

#### `WriteAtAsync(int left, int top, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string message)`

指定座標へカーソルを移動し、色付きでメッセージを表示します。  

仕様:

- 内部で `Console.SetCursorPosition(left, top)` を使います
- 内部では `Console.Write(...)` を使います
- 表示後に改行しません
- メッセージ内の改行は、そのまま `Console.Write(...)` の挙動に従います
- 描画前後でカーソル位置を退避・復元します

補足:

- 出力がリダイレクトされていない場合、必要ならコンソール・バッファーを自動的に広げます
- クリッピングや丸め込みは行いません
- 負の座標は `Console` 側の例外動作に依存します

---

#### `WriteLineAsync(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string message)`

現在のカーソル位置に、色付きでメッセージを表示します。  
内部では `Console.WriteLine(...)` を使います。  

仕様:

- 表示後に改行します
- `message` に改行が含まれていれば、そのまま複数行として表示されます
- メッセージ内改行を独自に整形する処理は行いません

---

#### `WriteLineAtAsync(int left, int top, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string message)`

指定座標へカーソルを移動し、色付きでメッセージを表示します。  

仕様:

- 内部で `WriteAtAsync(...)` を呼びます
- そのあとで空文字列を `WriteLineAsync(...)` へ渡して改行します
- 表示後に改行します
- メッセージ本体の表示は `Console.Write(...)` ベースです
- 改行部分は `Console.WriteLine(...)` ベースです
- 描画前後でカーソル位置を退避・復元します

補足:

- 出力がリダイレクトされていない場合、必要ならコンソール・バッファーを自動的に広げます
- クリッピングや丸め込みは行いません
- 負の座標は `Console` 側の例外動作に依存します


### カーソル位置関連

#### `PreserveCursorPositionAsync(Func<Task> executeAsync)`

処理前のカーソル位置を覚えて、処理後に元へ戻します。  

用途:

- 描画しても呼び出し元のカーソル位置を壊しにくくする
- 画面の一部だけを更新する


### 矩形描画

#### `FillRectAsync(int left, int top, int width, int height)`

指定した左上座標から、矩形領域を空白文字で塗りつぶします。  

仕様:

- `width <= 0` のときは何も描画しません
- `height <= 0` のときも何も描画しません
- 負の幅や高さで反対方向へ塗りつぶす仕様はサポートしません
- 出力がリダイレクトされていない場合、必要ならコンソール・バッファーを自動的に広げます
- 描画後はカーソル位置を元に戻します

実装の考え方:

- 1 行ずつカーソル移動します
- 各行を空白文字で埋めます
- 背景色を指定すれば、色付きの矩形塗りつぶしとして使えます

---

#### `FillRectAsync(int left, int top, int width, int height, ConsoleColor? fgColor = null, ConsoleColor? bgColor = null)`

色付きで矩形領域を塗りつぶします。  
内部では一時的に色を変更して、色なし版 `FillRectAsync(...)` を呼びます。  

仕様:

- `width <= 0` のときは何も描画しません
- `height <= 0` のときも何も描画しません
- 逆方向塗りつぶしは行いません
- 出力がリダイレクトされていない場合、必要ならコンソール・バッファーを自動的に広げます

---

#### `DrawDoubleBorderRectAsync(int left, int top, int width, int height)`

二重線の枠付き矩形を描画します。  

使用文字:

- `╔`
- `╗`
- `╚`
- `╝`
- `═`
- `║`

仕様:

- `width < 2` のときは何も描画しません
- `height < 2` のときも何も描画しません
- 二重線の矩形が潰れるサイズはサポートしません
- 出力がリダイレクトされていない場合、必要ならコンソール・バッファーを自動的に広げます
- 描画後はカーソル位置を元に戻します


### 点滅補助

#### `RunWithBlinkColorsAsync(...)`

2 組の色を切り替えて描画するための補助メソッドです。  
テキストや記号の点滅表現に使えます。  


## 描画スタイルの考え方

このライブラリーは、次の 2 つの描画スタイルを意識しています。  

### 1. 行単位の描画

向いている用途:

- メッセージ表示
- 矩形塗りつぶし
- パネル表示
- メッセージ欄

特徴:

- 実装が単純
- 学習用に理解しやすい
- 横に広い UI を作りやすい

---

### 2. セル単位の描画

向いている用途:

- キャラクター表示
- カーソル表示
- 小さな記号の点滅
- 一部セルだけの更新

特徴:

- 細かい制御に向く
- ゲーム向けの表現に向く

---

### どちらが正しいか

どちらか一方だけが正しいわけではありません。  

このライブラリーでは、現時点では次のように考えるのが自然です。  

- 文字列表示: 行単位
- 矩形描画: 行の集まりとして描画
- キャラクターや細かい記号: 将来的にセル単位の補助を追加するとよい


## 現状の制限

- 必要に応じたコンソール・バッファー拡張は行います
- ただし、負の座標補正は行いません
- 描画内容の丸め込みは行いません
- 画面外クリッピングは未対応です
- 自前の仮想画面バッファーは持ちません
- 高度なレイアウト機能は持ちません
- アニメーション専用フレーム制御は持ちません


## 利用者向けの注意

- 座標はコンソール内に収まる値を渡してください
- 長い文字列はコンソール標準の折り返し挙動に依存します
- 複数行文字列の表示位置そろえは、必要に応じて呼び出し側で調整してください
- 背景描画やパネル描画は `FillRectAsync(...)` を使うと簡単です
- 枠付き領域は `DrawDoubleBorderRectAsync(...)` を使うと簡単です


## ひとまずのまとめ

`HelloConsoleAppCSharp.Core.dll` は、コンソール描画を簡単にするための軽量ライブラリーです。  

特に次のように使うと分かりやすいです。  

- 1 行メッセージ表示: `WriteLineAsync(...)`
- 座標指定メッセージ表示（改行なし）: `WriteAtAsync(...)`
- 座標指定メッセージ表示: `WriteLineAtAsync(...)`
- 背景や面の塗りつぶし: `FillRectAsync(...)`
- 枠付き領域の表示: `DrawDoubleBorderRectAsync(...)`
- 描画後にカーソル位置を戻したい: `PreserveCursorPositionAsync(...)`

必要なら、この DLL と一緒にサンプルコード集も添えると、さらに使いやすくなります。  
