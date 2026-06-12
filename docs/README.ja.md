# True Clock

| 言語 |  |  |  |  |  |
|---|---|---|---|---|---|
| [English](../README.md) | [Português do Brasil](README.pt.md) | [Español](README.es.md) | **日本語** | [Français](README.fr.md) | [Italiano](README.it.md) |

True Clock は Stardew Valley 用の SMAPI mod です。ゲーム内 HUD に現実のローカル時刻を表示します。さらに、最大 5 個のリアルタイム通知を設定でき、プレイヤーの上に時計の吹き出し、小さな HUD メッセージ、短いアラーム音を表示します。

この時計は Stardew Valley のゲーム内時刻ではなく、コンピューターのローカル時刻を使います。

## 機能

- 左上のステータス表示の下に現実のローカル時刻を表示します。
- Stardew Valley 標準の UI スタイルを使います。
- 24 時間表示または AM/PM 表示に対応します。
- 最大 5 個の通知を設定できます。
- 各通知は個別に有効化または無効化できます。
- 各通知には時、分、任意のメッセージを設定できます。
- 通知は 1 日に最大 1 回だけ発生します。
- 通知は最大 5 秒間表示され、ゲームを一時停止したり入力をブロックしたりしません。
- Generic Mod Config Menu に任意で対応します。

## 必要なもの

- Stardew Valley
- SMAPI 4.0.0 以降
- ソースからビルドする場合のみ .NET 6 SDK
- Generic Mod Config Menu は任意です

## Nexus Mods からのインストール

mod ページが公開された後、True Clock は Nexus Mods からもインストールできます。

- Nexus Mods Stardew Valley: https://www.nexusmods.com/stardewvalley
- mod ページ: Nexus Mods の ID が利用可能になった後で追加します

Nexus Mods から mod アーカイブをダウンロードし、Stardew Valley の `Mods` フォルダーに展開してください。

## Release ZIP からのインストール

1. https://smapi.io/ から SMAPI をインストールします。
2. GitHub Releases ページから最新の `TrueClock.zip` をダウンロードします。
3. ZIP を Stardew Valley の `Mods` フォルダーに展開します。
4. 最終的なフォルダー構成が次のようになっていることを確認します。

```text
Stardew Valley/
  Mods/
    TrueClock/
      manifest.json
      TrueClock.dll
      i18n/
```

5. SMAPI からゲームを起動します。

初回起動時に、mod は `TrueClock` フォルダー内に `config.json` を作成します。

## 設定

Generic Mod Config Menu がインストールされている場合は、ゲーム内の mod 設定から時計と通知を設定できます。

Generic Mod Config Menu を使わない場合は、初回起動後に `config.json` を手動で編集します。mod は常に 5 個の通知スロットを保持します。

例:

```json
{
  "Use24HourClock": true,
  "Alerts": [
    {
      "Enabled": true,
      "Hour": 8,
      "Minute": 30,
      "Message": "農場を確認する時間"
    }
  ]
}
```

時は現実のローカル時刻を 24 時間形式で指定し、範囲は `0` から `23` です。分は `0` から `59` です。

## ソースからビルド

.NET 6 SDK をインストールし、次を実行します。

```bash
dotnet build -c Release
```

このリポジトリの release workflow は mod をビルドし、GitHub Releases に ZIP ファイルを公開します。

## GitHub Release ZIP

公式 release は GitHub Actions によって生成されます。`v1.0.0` のようなバージョンタグを push すると、workflow が mod をビルドし、`TrueClock.zip` を作成して GitHub Release に添付します。

## 作者

Regivaldo (Sun)  
Email: regivaldorfs@gmail.com  
Web サイト: https://regivaldo.com.br
