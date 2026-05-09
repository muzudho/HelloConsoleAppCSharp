# WriteLineAtAsync(...) 安全化メモ

この文書は、`HelloConsoleAppCSharp.Core.dll` 側の `MuzConsole.WriteLineAtAsync(...)` を改善するための整理メモです。  
ライブラリーのプロジェクトで GPT-5.4 に渡すことを想定しています。

---

## いま何が起きているか

`WriteLineAtAsync(int left, int top, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string message)` は、内部で次のような流れになっています。

1. `Console.SetCursorPosition(left, top)`
2. `WriteLineAsync(...)`
3. カーソル位置を退避・復元

このとき、`left` や `top` がコンソールのバッファー範囲外だと、`Console.SetCursorPosition(...)` が `ArgumentOutOfRangeException` を投げます。

ライブラリーの説明書にも、現状は次の仕様だと書かれています。

- 座標がコンソールバッファー外のときの自動調整は行わない
- クリッピングや丸め込みは行わない
- 不正な座標は `Console` 側の例外動作に依存する

この仕様のため、呼び出し側で表示領域を計算しても、少しでもバッファー不足があるとライブラリー側で落ちます。

---

## 実際に発生したエラー例

アプリ側で将棋盤を座標指定で描画したところ、以下の例外が発生しました。

- `System.ArgumentOutOfRangeException`
- Parameter: `top`
- Actual value was `30`

スタックトレース上では、以下の地点で失敗しています。

- `System.ConsolePal.SetCursorPosition(Int32 left, Int32 top)`
- `HelloConsoleAppCSharp.Core.Infrastructure.MuzConsole.WriteLineAtAsync(...)`

要するに、`top=30` に描こうとしたが、その時点の `Console.BufferHeight` が足りず、ライブラリー側で落ちました。

---

## 何が悪いのか

問題の本質は次の 3 点です。

### 1. 座標指定 API なのに、安全化がない

`WriteLineAtAsync(...)` は「座標を指定して描画する」API ですが、実際には `Console.SetCursorPosition(...)` をそのまま呼んでいるだけです。  
そのため、ライブラリー利用者は毎回、事前に `BufferWidth` / `BufferHeight` を自力で保証しなければなりません。

### 2. `WriteLineAsync(...)` を内部で呼ぶため、表示サイズを読み違えやすい

`WriteLineAtAsync(...)` は最終的に `Console.WriteLine(...)` を使うため、

- メッセージ末尾で改行する
- 長い文字列はコンソール標準の折り返し挙動に依存する
- メッセージ内改行があれば複数行になる

という性質があります。

つまり `top` だけ見ても足りず、実際には

- 描画開始位置
- メッセージの行数
- 各行の長さ
- 折り返しの有無

まで考えないと安全ではありません。

### 3. 行単位描画 API として最低限の防御が欲しい

この DLL は軽量ライブラリーで、行単位描画を重視しています。  
その場合でも、少なくとも

- 必要ならバッファーを広げる
- 画面外座標を検知する
- 利用者に分かりやすい挙動を提供する

のどれかはあった方が使いやすいです。

---

## どう直すとよいか

改善案は 3 段階あります。

---

## 改善案 A: `WriteLineAtAsync(...)` の中でバッファーを自動拡張する

もっとも実用的な案です。

### 方針

`WriteLineAtAsync(...)` の実行前に、

- `message` を行分割する
- 各行の最大長を求める
- 必要な `BufferWidth` と `BufferHeight` を計算する
- 足りなければ `Console.BufferWidth` / `Console.BufferHeight` を拡張する

そのあとで `SetCursorPosition(left, top)` を実行します。

### 必要な考慮点

- `message == null` は空文字扱いにするか、既存仕様に合わせる
- `\r\n` / `\n` を正しく行分割する
- `Console.IsOutputRedirected` のときは無理に触らない
- `left` / `top` が負数なら例外のままでよい
- `WindowWidth` / `WindowHeight` との関係で `BufferWidth` / `BufferHeight` を下げない

### この案の利点

- 呼び出し側の負担がかなり減る
- 座標描画 API として直感的になる
- 既存の `WriteLineAtAsync(...)` 呼び出しコードがほぼそのまま使える

### この案の注意点

- 折り返し判定はコンソール依存のため、完全には読めない
- ただし「少なくとも各行長ぶんの幅を確保する」だけでも、かなり改善される

---

## 改善案 B: 安全版 API を別名で追加する

既存仕様を壊したくない場合はこちらです。

例:

- `WriteLineAtSafeAsync(...)`
- `WriteLineAtEnsuredAsync(...)`
- `EnsureBufferAndWriteLineAtAsync(...)`

### 方針

既存の `WriteLineAtAsync(...)` はそのまま残し、

- 新しい安全版メソッドを追加
- 安全版の中でバッファーを確保してから描画

とします。

### この案の利点

- 既存利用者への影響が少ない
- 「低レベル版」と「安全版」を分けられる

### この案の欠点

- API が増える
- 利用者がどちらを使うべきか迷う

---

## 改善案 C: 低レベル版は維持しつつ、補助メソッドを追加する

例:

- `EnsureBufferSizeForText(int left, int top, string message)`
- `EnsureBufferSize(int requiredWidth, int requiredHeight)`

そのうえで利用者が

1. `EnsureBufferSizeForText(...)`
2. `WriteLineAtAsync(...)`

の順に呼ぶ方式です。

### この案の利点

- 役割分離が明確
- `FillRectAsync(...)` など他メソッドにも流用しやすい

### この案の欠点

- 呼び出し忘れで結局落ちる
- 安全化としては弱い

---

## おすすめの結論

GPT-5.4 への依頼としては、**改善案 A を第一候補** にするのがおすすめです。  
つまり、`WriteLineAtAsync(...)` 自体を「安全な座標描画メソッド」として振る舞わせる案です。

理由:

- 利用者の期待に合う
- 軽量ライブラリーでも実用性が上がる
- 既存アプリ側のコードを減らせる

もし後方互換を強く意識するなら、**改善案 B** が次点です。

---

## GPT-5.4 に依頼したい修正内容

以下のように依頼するとよいです。

### 依頼内容

`HelloConsoleAppCSharp.Core.Infrastructure.MuzConsole.WriteLineAtAsync(...)` を改善してください。

#### 現状の問題

- 内部で `Console.SetCursorPosition(left, top)` をそのまま呼んでいる
- `left` / `top` がバッファー外だと `ArgumentOutOfRangeException` が発生する
- 呼び出し側で毎回バッファーサイズ管理をしなければならず、不便

#### 修正方針

- `WriteLineAtAsync(...)` の描画前に必要バッファーサイズを計算する
- 必要なら `Console.BufferWidth` / `Console.BufferHeight` を拡張する
- 負の `left` / `top` は従来どおり例外でもよい
- `Console.IsOutputRedirected` の場合は無理にバッファー操作しない
- 文字列中の改行を考慮して必要高さを計算する
- 各行の最大長から必要幅を計算する
- 既存のカーソル退避・復元の設計はできるだけ維持する

#### できれば追加してほしいこと

- バッファーサイズ確保ロジックを共通 private メソッドに切り出す
- `FillRectAsync(...)` など他の座標系メソッドでも再利用できる形にする
- XML コメントや README に「安全化された挙動」を追記する

---

## 実装イメージ

厳密なコードではなく、考え方だけ示します。

1. `message` を改行で分割する
2. 行数を数える
3. 最長行の長さを調べる
4. 必要サイズを計算する
   - `requiredWidth = left + maxLineLength`
   - `requiredHeight = top + lineCount`
5. `Console.BufferWidth` / `Console.BufferHeight` が不足していれば拡張
6. `Console.SetCursorPosition(left, top)`
7. `WriteLineAsync(...)`

---

## 補足

このライブラリーは「行単位描画」と「軽量さ」を重視しているので、完全なクリッピングや仮想バッファーまでは不要だと思います。  
ただし、`WriteLineAtAsync(...)` の最低限の安全化は入れておくと、利用者体験がかなり良くなります。

特に、盤面表示・パネル表示・メッセージ欄のような「固定座標に複数行を書く」用途では、今回の改善効果が大きいです。
