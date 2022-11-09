# ChangeLog

## [1.2.2] - 2022-11-09
### Singletonで指定されたPrefaｂからのインスタンスをDestoryしたら、以降のResolveで再度Instantiateするように
- 上記の通り

## [1.2.1] - 2022-10-27
### Builderにid付きでRegistration可能に、injectionに失敗した際の例外メッセージ
- 一部クラス名変更
- サンプルを更新

## [1.2.0] - 2022-09-26
### Builderのメソッドのを整理、Dispose処理追加
- 上記の通り

## [1.1.2] - 2022-08-19
### Resolverを依存解決したFuncを登録できるように
- 上記の通り

## [1.1.1] - 2022-06-27
### Edior拡張が原因でビルド時にエラーが発生する問題の対応

## [1.0.0] - 2022-05-09
### RegistrationHelperのスクリプトとPrefabを自動生成するEditor拡張を追加
- RegistrationHelperのスクリプトとPrefabを自動生成するEditor拡張を追加
- BuilderへのRegistrationメソッドの引数をリファクタリング

## [1.0.6] - 2022-05-09
### RegistrationHelperの基底クラスをリファクタリング
- RegistrationHelperのinterfaceを定義し、GameObject用とScriptableObjectがそれを継承する形に

## [1.0.5] - 2022-05-07
### ServiceLocatorとして運用するケースのための改修
- ScriptableObjectからのRegistrationをする機能追加
- PrefabをDontDestoryOnLoad上に生成するオプション追加
- ServiceLocatorをするサンプル追加

## [1.0.1] - 2022-04-11
### RegistrationHelperに関する改修.
- RegistrationHelperを一つのGameObjectに複数アタッチできないように改修.

## [1.0.0] - 2022-04-07
### NeContainer公開.
- UPMからGitURLでのインポートを対応.