# MuzConsole メソッド名リファクタリング案

`MuzConsole` を基礎描画 API として整理していく前提で、自然な名前の対応案をまとめる。

## 命名の軸

- `Write`: 文字を書く
- `Draw`: 線や枠を描く
- `Fill`: 領域を塗る
- `Get` / `Parse`: 値を取得・変換する
- `RunWith` / `Preserve`: 一時状態を変えて処理し、元に戻す

## 対応表

| 現在 | 提案 | 理由 |
| --- | --- | --- |
| `SetColorAsync` | `RunWithColorAsync` | 色を設定するだけではなく、処理を実行して元に戻しているため |
| `GetColorByName` | `ParseColor` または `GetColorByName` | 現状維持でもよい。C# らしく寄せるなら `ParseColor` |
| `WriteLineAsync(fg, bg, message)` | `WriteLineAsync` | そのままで自然 |
| `WriteLineAsync(left, top, fg, bg, message)` | `WriteLineAtAsync` | 座標指定付きであることが分かりやすい |
| `ResetCursorLocationAfterExecute` | `PreserveCursorPositionAsync` | 処理後にカーソル位置を戻す意図が自然に伝わる |
| `PrintBoxAsync(left, top, width, height)` | `FillRectAsync` | 実態が矩形塗りつぶしだから |
| `PrintBoxAsync(..., fg, bg)` | `FillRectAsync(..., fg, bg)` | 上と揃える |
| `PrintDoubleBorderBoxAsync` | `DrawDoubleBorderRectAsync` | 枠線付き矩形であることが明確 |
| `BlinkAsync` | `RunWithBlinkColorsAsync` または `BlinkAsync` | 汎用処理として見せるなら前者、見た目機能として見せるなら後者 |

## 最小 API 一覧

### 色

- `RunWithColorAsync(...)`
- `ParseColor(...)`

### カーソル

- `PreserveCursorPositionAsync(...)`

### 文字表示

- `WriteLineAsync(...)`
- `WriteLineAtAsync(...)`

将来的に追加候補:

- `WriteAsync(...)`
- `WriteAtAsync(...)`

### 矩形

- `FillRectAsync(...)`
- `DrawDoubleBorderRectAsync(...)`

仕様メモ:

- `FillRectAsync(...)` は `width <= 0` または `height <= 0` のとき何も描画しない
- 負の幅や高さで反対方向に塗る仕様はサポートしない
- `DrawDoubleBorderRectAsync(...)` は `width < 2` または `height < 2` のとき何も描画しない
- 二重線の矩形が潰れるサイズはサポートしない

## 将来の追加候補

- `DrawSingleBorderRectAsync(...)`
- `ClearRectAsync(...)`
- `DrawHorizontalLineAsync(...)`
- `DrawVerticalLineAsync(...)`

## 命名ルールのメモ

### `Box` より `Rect`

`Box` は「箱」「UI 部品」「枠」など意味が広い。
`Rect` は「矩形領域」で意味が固定されるため、基礎描画 API には向いている。

### `Print` より `Write / Draw / Fill`

- `Print`: 曖昧
- `Write`: 文字を書く
- `Draw`: 線や枠を描く
- `Fill`: 塗りつぶす

## まず着手する順

1. `SetColorAsync` → `RunWithColorAsync`
2. `ResetCursorLocationAfterExecute` → `PreserveCursorPositionAsync`
3. `PrintBoxAsync` → `FillRectAsync`
4. `PrintDoubleBorderBoxAsync` → `DrawDoubleBorderRectAsync`
5. `WriteLineAsync(left, top, ...)` → `WriteLineAtAsync`
