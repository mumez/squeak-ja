<<フォントについて>>

[デフォルトTrueTypeフォント]

TrueTypeフォントとして「Komatunaフォント」を同梱しています。
https://packages.debian.org/bookworm/fonts-komatuna
ライセンスについてはアーカイブに同梱されるREADMEをご覧ください。

[外部TrueTypeフォントの利用]

外部のTrueTypeフォントを参照して利用することができます。
1. Squeakの実行ディレクトリ
2. 実行ディレクトリ下の'fonts'ディレクトリ
3. 各OSのフォントディレクトリ
の順でTrueTypeフォントを検索して使用します。

TTFileDescription installFamilyNamed: 'フォント名'.
とすると、Squeakから該当TrueTypeフォントを指定できるようになります。

"例: メイリオを指定可能にする"
TTFileDescription installFamilyNamed: 'Meiryo'.

※外部のTrueTypeフォントを使う場合、ファイルの移動などでフォントを参照できなくなると、イメージが起動不能になります。<イメージディレクトリ>/fonts/ にあるttfファイルを消さないようにしてください。
※以前はTTフォントデータをイメージ内に埋め込むことが可能でしたが、Squeak 6.0からは参照のみとなっています。Komatunaも参照形式に変わったのでご注意ください。

[フォント変更ユーティリティ]

Squeak日本語版は、JaExFontUtilForSqueakというフォント変更用のユーティリティを同梱しています。

fontsディレクトリ以下にTrueTypeフォントをダウンロードしコピーし、以下を"do it"でフォントをイメージに埋め込むことができます(例はKomatunaフォント)。

JaExFontUtilForSqueak embedTTFontFromFile: 'komatuna.ttf'.

fontsディレクトリ直下の外部TrueTypeフォントをまとめてSqueakから利用可能にするには以下を"do it"します。

JaExFontUtilForSqueak installLocalTTFonts.

※トップメニューから「外観...」「システム・フォント...」で個別にフォントを変更できます。

使用するフォントとサイズをまとめて設定するには以下のようにします(例はIPA P明朝)。

JaExFontUtilForSqueak setAllFontsNamed: 'IPAPMincho' size: 12.

このほか、より詳しくはJaExFontUtilForSqueakのソースを見てください。

---
[:masashi | ^umezawa] "Masashi Umezawa<ume@blueplane.jp>"