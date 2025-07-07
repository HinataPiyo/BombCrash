# BombCrash スクリプト設計ドキュメント

## 1. 概要

このプロジェクトは、爆弾を駆使して敵のウェーブをクリアしていくアクションゲーム「BombCrash」のスクリプト群です。
設計の大きな特徴として、UnityのScriptableObjectを全面的に採用し、ゲーム内の様々な要素（キャラクターのステータス、敵のデータ、ボムの性能、ガチャの排出率など）をデータとして管理する**データ駆動型アーキテクチャ**を採用しています。これにより、プログラマー以外のプランナーやデザイナーも、ソースコードを触ることなくゲームバランスの調整やコンテンツの追加が可能になっています。

## 2. ディレクトリ構造

スクリプトは、その役割に応じて以下のディレクトリに機能分割されています。

- `/Boss`: ボスキャラクター固有のロジック
- `/CutIn`: カットイン演出の制御
- `/Define`: ゲーム内で使用する定数（enumや静的クラス）
- `/Interface`: オブジェクト間の連携を疎結合にするためのインターフェース
- `/SO`: ScriptableObjectのアセット定義と、それを管理するデータベース
- `/System`: ゲームのコアロジック
- `/UI`: ユーザーインターフェースの制御

---

## 3. 主要な機能モジュール

### 3.1. SO (ScriptableObject)

ゲームの静的なデータを管理する最も重要な部分です。

- **役割**: 敵、プレイヤー、ボム、スキル、ストーリー、ガチャなど、ゲームを構成するほぼ全ての要素のパラメータを保持します。
- **主なスクリプト**:
    - `PlayerStatusSO`, `EnemySO`, `BossSO`: キャラクターの基本ステータス。
    - `BombSO`, `SkillSO`, `AttachmentSO`: プレイヤーが使用する武器やスキルのデータ。
    - `ChapterStorySO`, `EndlessWaveRule`: ゲームモードやストーリーの進行に関するデータ。
    - `SkillDatabase`, `AttachmentDatabase`: 各データアセットを管理・検索するためのデータベース。

### 3.2. System

ゲームの動的な振る舞いを制御するコアロジックが集まっています。

- **役割**: ゲームの進行管理、プレイヤーや敵の制御、戦闘処理など、ゲームの「ルール」を実装します。
- **主なサブシステム**:
    - `GameSystem`: ゲーム全体の状態（プレイ中、ゲームオーバーなど）を管理する中心的な存在。
    - `Player`: プレイヤーの移動や、ボムを投げる(`ThrowBomb`)などのアクションを制御します。
    - `Bomb`: 投げられたボムの挙動や爆発処理を担います。
    - `EnemyBomb`: 敵キャラクターの生成、ステータス管理、死亡時の処理などを担当します。
    - `Wave`: `WaveGenerator`がSO（`EndlessWaveRule`など）を元に敵の出現パターンを生成し、`EnemySpawnController`が敵をシーンに登場させます。
    - `Otomo_Skill`: プレイヤーに追従する「オトモ」や、それが使用するスキルの管理を行います。
    - `Gacha`: `SkillGacha`がSOの定義に基づいてスキルの抽選処理を行います。
    - `Story`: `StoryController`がSOのストーリーデータを読み込み、ゲームの物語を進行させます。
    - `SoundManager`: BGMやSEの再生を管理します。

### 3.3. UI

プレイヤーが直接触れるユーザーインターフェースの表示と入力を担当します。

- **役割**: System層のデータや状態を画面に表示し、プレイヤーからの入力をSystem層に伝えます。
- **主なスクリプト**:
    - `HomeSceneController`: ホーム画面のボタン制御や画面遷移を担当。
    - `GameSceneMainCanvas`: ゲーム中のHPバー、スコア、スキルアイコンなどを管理。
    - `ResultCanvasController`: リザルト画面の表示を制御。
    - `GachaUIController`: ガチャ画面の演出や結果表示を制御。
    - `OtomoSkillDetailPanel`, `AttachmentEquipSlotController`: キャラクターの強化やカスタマイズ画面の制御。
    - `StatusTextController`: キャラクターのステータス表示を管理。

### 3.4. Define / Interface

コードの保守性や拡張性を高めるための補助的なモジュールです。

- **`Define`**: `SoundDefine`, `StoryDefine`のように、サウンド名やシーン名などの定数を一元管理し、ハードコーディングを防ぎます。
- **`Interface`**: `IEnemy`のように、異なる種類の敵に共通の操作（ダメージを受ける、死亡するなど）を定義することで、コードの再利用性を高めています。

## 4. 全体の連携フロー（例: ゲームプレイ）

1.  **UI** (`HomeSceneController`) が「ゲーム開始」ボタンの入力を検知する。
2.  **System** (`GameSystem`) がゲームシーンをロードする。
3.  **System** (`WaveManager` / `WaveGenerator`) が **SO** (`EndlessWaveRule`) を読み込み、そのデータに基づいて敵の出現を開始する。
4.  **UI** がプレイヤーの移動入力を受け取り、**System** (`PlayerMovement`) がキャラクターを動かす。
5.  **UI** がボム使用ボタンの入力を検知し、**System** (`ThrowBomb`) が **SO** (`BombSO`) のデータに基づいてボムを生成・投擲する。
6.  ボムが敵（`EnemyStatus`を持つオブジェクト）に命中すると、ダメージ計算が行われ、**UI** (`ApplyDamageText`) がダメージ数値を表示する。
7.  敵を倒すと **System** (`EnemyKillCountController`) が討伐数をカウントし、**UI** に反映する。
8.  ゲームオーバーまたはクリア条件を満たすと、**System** (`GameSystem`) がリザルトシーンに遷移させ、**UI** (`ResultCanvasController`) が結果を表示する。
