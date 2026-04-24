# Ｃ＃コンソール・アプリ作成サンプル



## 第１章：インフラストラクチャーの作成

* 📄 [はじめに](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O1_はじめに.md)
* 📄 [ソリューション一式の作成](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O2_ソリューション一式の作成.md)
* 📄 [コンソールアプリケーションに汎用ホストの機能を追加](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O3_コンソールアプリケーションに汎用ホストの機能を追加.md)
* 📄 [プログラム・サービスの作成](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O3A2_プログラム・サービスの作成.md)
* 📄 [アプリケーション設定ファイルの準備](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O4_アプリケーション設定ファイルの準備.md)
* 📄 [ロギングの準備＜その１＞](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O5_ロギングの準備＜その１＞.md)
* 📄 [ロギングの準備＜その２＞（複数のロガーの使い分け）](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O6_ロギングの準備＜その２＞（複数のロガーの使い分け）.md)
* 📄 [アサートの準備](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O7_アサートの準備.md)
* 📄 [ユニットテストの準備](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャーの作成/O8_ユニットテストの準備.md)


```shell
hello
```

👆 📄 [リプルの作成](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャー/O9_リプルの作成.md)  


### ファイルからメッセージを読み取ろう

```shell
read-messages-warmup
```

👆 📄 [ファイルからメッセージを読み取ろう＜その１＞ウォーミングアップ](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャー/OO10A1_ファイルからメッセージを読み取ろう＜その１＞ウォーミングアップ.md)


```shell
read-messages-2-warmup
```

👆 📄 [ファイルからメッセージを読み取ろう＜その２＞複数行の書式](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャー/OO10A2_ファイルからメッセージを読み取ろう＜その２＞複数行の書式.md)  


```shell
load-messages
get-message-warmup Msg_1
get-message-warmup ErrorMsg_1
get-message-warmup WaHaHa!
```

👆 📄 [ファイルからメッセージを読み取ろう＜その３＞コマンドでの動作確認](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャー/OO10A3_ファイルからメッセージを読み取ろう＜その３＞コマンドでの動作確認.md)  


```shell
get-message Msg_1
get-message ErrorMsg_1
```

👆 📄 [ファイルからメッセージを読み取ろう＜その４＞メソッドにしよう](./HelloConsoleAppCSharp/Docs/第１章：インフラストラクチャー/OO10A4_ファイルからメッセージを読み取ろう＜その４＞メソッドにしよう.md)📄 [


## 第２章：プログレスバーの作成

この章では、コンソールの操作を覚えながら、プログレスバーを作ってみましょう（＾▽＾）  


```shell
command hello
command command hello
```

👆 📄 [コマンドの間接的実行](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O1_コマンドの間接的実行.md)  


```shell
change-color-warmup
```

👆 📄 [文字色の変更＜その１＞ウォーミングアップ](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O2A1_文字色の変更＜その１＞ウォーミングアップ.md)  


```shell
print-message-with-color Red Blue Hello, World!!

# 入力エラーのときの動作確認（＾～＾）
print-message-with-color Banana
```

👆 📄 [文字色の変更＜その２＞色を指定してメッセージ表示](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O2A2_文字色の変更＜その２＞色を指定してメッセージ表示.md)  


```shell
move-cursor-warmup
```

👆 📄 [カーソルの移動](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O3_カーソルの移動.md)  


```shell
clear
```

👆 📄 [コンソールのクリアー](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O4_コンソールのクリアー.md)


```shell
wait-for-3-seconds-warmup
```

👆 📄 [３秒待つ](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O5_３秒待つ.md)


```shell
get-console-size
```

👆 📄 [コンソールのサイズ取得](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O6_コンソールのサイズ取得.md)  


```shell
show-progress-bar-warmup
```

👆 📄 [プログレスバーの作成と動作確認](./HelloConsoleAppCSharp/Docs/第２章：プログレスバーの作成/O7_プログレスバーの作成と動作確認.md)  


## 第３章：タイトル画面の作成


```shell
key-input-warmup
```

👆 📄 [キー入力](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O1_キー入力.md)  


```shell
show-erapsed-time
```

👆 📄 [アプリケーション起動からの経過時間を表示する](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O2_アプリケーション起動からの経過時間を表示する.md)  


```shell
hide-message-box
hide-start-box
```

👆 📄 [フローティングボックスの作成](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O3_フローティングボックスの作成.md)  


```shell
show-message-box
show-start-box
```

👆 📄 [枠付きのフローティングボックスの作成](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O4_枠付きのフローティングボックスの作成.md)  


```shell
show-wall
```

👆 📄 [壁面の描画.md](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O5_壁面の描画.md)


```shell
floating-label-warmup
show-title
show-credit
```

👆 📄 [フローティングラベルの作成](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O6_フローティングラベルの作成.md)  


```shell
show-start-vertical-list
```

👆 📄 [箇条書きの作成](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O7_箇条書きの作成.md)  


```shell
select-start-warmup
```

👆 📄 [点滅ラベルの作成](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O8_点滅ラベルの作成.md)  


```shell
cursor-increment-warmup
```

👆 📄[カーソルキーとインクリメント](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/O9_カーソルキーとインクリメント.md)  


```shell
show-start-menu
```

👆 📄 [メニューの作成](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/OO10_メニューの作成.md)


```shell
title-page-warmup
```

👆 📄 [タイトル画面風の描画](./HelloConsoleAppCSharp/Docs/第３章：タイトル画面の作成/OO11_タイトル画面風の描画.md)


## 第４章：図鑑の作成

メニューから項目を選ぶと、メッセージが表示される、という図鑑のようなものを作ってみましょう（＾▽＾）！


```shell
typewriter-effect-warmup
```

👆 📄 [タイプライター効果の作成](./HelloConsoleAppCSharp/Docs/第４章：図鑑の作成/O1_タイプライター効果の作成.md)  


```shell
continue-prompt-warmup
```

👆 📄 [コンティニュープロンプトの作成](./HelloConsoleAppCSharp/Docs/第４章：図鑑の作成/O2_コンティニュープロンプトの作成.md)  


```shell
message-box-warmup
```

👆 📄 [メッセージボックスの作成](./HelloConsoleAppCSharp/Docs/第４章：図鑑の作成/O3_メッセージボックスの作成.md)  


## 作りかけ

TODO: 以降の予定： メッセージ・リソース読取（JSONファイルを使ったメッセージ管理）、 古いタイマーの見直し、垂直メニューのサンプルの作り直し、古いブリンカーの見直し、画面遷移、  
など。  

TODO: メッセージファイルの読込は１回だけにして、プログラム・サービスに常時格納したい。（ホットリロードは作るか？）  
