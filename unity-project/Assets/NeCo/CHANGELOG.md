# ChangeLog

## [1.0.6] - 2022-05-09
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