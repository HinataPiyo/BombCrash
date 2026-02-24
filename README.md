# BombCrash

**BombCrash** は Unity で制作された 2D アクションゲームです。プレイヤーは強力な冷凍ボムを武器に、カウントダウン式の敵ボムが爆発する前に次々と撃破していきます。敵を倒してスクラップとインサイトポイントを集め、スキルや装備をアップグレードしながら、どこまでウェーブを生き延びられるか挑戦しましょう。

> ゲームの詳細なマニュアルは [`Assets/Docs/README.md`](Assets/Docs/README.md) を参照してください。  
> スクリプトのアーキテクチャ概要は [`Assets/Docs/documentation.md`](Assets/Docs/documentation.md) を参照してください。  
> ストーリーの背景については [`Assets/Docs/story.md`](Assets/Docs/story.md) を参照してください。

---

## 目次

1. [敵キャラクターとボス](#敵キャラクターとボス)
2. [ボムの仕様](#ボムの仕様)
3. [アルティメットスキル](#アルティメットスキル)
4. [ゲームプレイシステム](#ゲームプレイシステム)
5. [ユーザーインターフェース](#ユーザーインターフェース)
6. [クレジット](#クレジット)

---

## 敵キャラクターとボス

敵ボムは [`Assets/Scripts/SO/EnemySO.cs`](Assets/Scripts/SO/EnemySO.cs) で定義されており、ウェーブのルールに従ってスポーンします。

| プロパティ | 説明 |
| :--- | :--- |
| **HP** | 各敵には基本最大 HP（`defaultMaxHp`）があり、ウェーブが進むにつれて増加します（`upMaxHp`）。 |
| **カウントダウン** | 画面上のタイマーがゼロになると爆発し、ゲームオーバーとなります。 |
| **スクラップドロップ** | 敵を倒すと、アップグレードに使用するスクラップが一定量ドロップされます。 |
| **初出現ウェーブ** | 各敵タイプは初めて登場したウェーブ番号を保持しています（`startWave`）。 |

**ボス**は `EnemySO` を継承しており（[`BossSO.cs`](Assets/Scripts/SO/BossSO.cs) を参照）、以下の 3 種類が存在します。

- `boss_Ver1_0`
- `boss_Ver1_5`
- `boss_Ver2_0`

**スタンピード**イベント（`Assets/Scripts/System/STANPEDE/`）は 10 ウェーブごとに大量の敵を一斉スポーンさせます。

---

## ボムの仕様

プレイヤーの主要武器は冷凍ボムです。主要ロジックは [`Assets/Scripts/System/Bomb/BombBase.cs`](Assets/Scripts/System/Bomb/BombBase.cs) に、データは [`Assets/Scripts/SO/BombSO.cs`](Assets/Scripts/SO/BombSO.cs) で管理されています。

| プロパティ | 説明 |
| :--- | :--- |
| **基本攻撃ダメージ** | デフォルト値は `6.5` で、ボムごとの係数によってスケールされます。 |
| **爆発半径** | 範囲攻撃の広さを制御します。こちらも係数によってスケールされます。 |
| **クリティカルヒット** | 投擲のたびに `BombSO` がプレイヤーの `CriticalChance` を判定します。成功すると `CriticalDamage` の倍率でダメージが増加します。 |
| **プレイヤーステータス補正** | 装備したアタッチメントにより、`BombAttackDamageUp`・`ExplosionRadiusUp`・`BombStockAmountUp`・`BombCreateSpeedUp`・`CriticalDamageUp`・`CriticalChanceUp` を強化できます。 |

ボムは投擲後、照準地点へ向かって飛び、拡大・収縮してから爆発し、爆発半径内のすべての敵にダメージを与えます。爆発時にはカメラシェイクも発生します（[ゲームプレイシステム](#ゲームプレイシステム) を参照）。

プレイヤーは初期状態で **3 つのボムスロット**（`defaultHaveBomb = 3`）を持ち、デフォルトでは **2 秒ごと**（`createBombTime = 2f`）にボムが補充されます。どちらの値もアタッチメントで調整できます。

---

## アルティメットスキル

アルティメットスキルは抽象 `ScriptableObject` アセットとして定義されており（[`Assets/Scripts/SO/UltimateSO.cs`](Assets/Scripts/SO/UltimateSO.cs) を参照）、[`Assets/Scripts/System/Ultimate/UltimateController.cs`](Assets/Scripts/System/Ultimate/UltimateController.cs) によって実行されます。

| プロパティ | 説明 |
| :--- | :--- |
| **発動方法** | アルティメットが使用可能な状態で **V** キーを押します。 |
| **クールタイム** | 各アルティメットアセットで個別に定義（`coolTime`）され、UI スライダーで進行状況が確認できます。 |
| **カスタマイズ** | 使用するアルティメットは `PlayerStatusSO.UltimateSO` で設定するため、アセットを差し替えるだけで簡単に変更できます。 |

アルティメット発動中、ボムオブジェクトはフィールド上に残り続けます（爆発後は `SpriteRenderer` のみ非表示になります）。これにより持続的な範囲効果が実現されています。

---

## ゲームプレイシステム

### オトモ（サポート AI）

[`Assets/Scripts/System/Otomo/OtomoMovement.cs`](Assets/Scripts/System/Otomo/OtomoMovement.cs) は、プレイヤーに追従するコンパニオンキャラクターを制御します。オトモは、プレイヤーが到達した最高ウェーブ数がウェーブ 10 を超えると**自動的に解放**されます（`PlayerStatusSO` の `maxWaveReleaseOtomo = 10`）。

オトモに割り当てるスキルは [`Assets/Scripts/System/Otomo_Skill/OtomoSkillManager.cs`](Assets/Scripts/System/Otomo_Skill/OtomoSkillManager.cs) で管理されます。最大 3 つのスキルスロット（Z / X / C）を設定でき、スキルは**自動**（デフォルト）と**手動**の両方の発動モードに対応しています。

### ウェーブ進行

ウェーブのロジックは [`Assets/Scripts/System/Wave/`](Assets/Scripts/System/Wave/) 内の 3 つのファイルに分かれています。

| スクリプト | 役割 |
| :--- | :--- |
| `WaveManager.cs` | 現在のウェーブ数を管理し、敵リストを制御します。 |
| `WaveGenerator.cs` | `EndlessWaveRule` ScriptableObject を読み込み、出現する敵の種類と数を決定します。 |
| `EnemySpawnController.cs` | シーン内に敵をスポーンさせます。 |

フィールド上の敵をすべて倒すと次のウェーブへ進みます。ウェーブデータアセットは [`Assets/Scripts/SO/EndlessWaveRule.cs`](Assets/Scripts/SO/EndlessWaveRule.cs) にあります。

### カメラ制御

[`Assets/Scripts/System/GameSysytem/CameraShake.cs`](Assets/Scripts/System/GameSysytem/CameraShake.cs) は **Unity Cinemachine**（`CinemachineBasicMultiChannelPerlin`）を使用して、ボムの爆発やゲームオーバー時などにカメラを揺らします。シェイクの持続時間・振幅・周波数はすべて呼び出しごとに設定可能です。

---

## ユーザーインターフェース

### アタッチメント管理

[`Assets/Scripts/UI/Atatchment/`](Assets/Scripts/UI/Atatchment/) には、アタッチメントショップと装備スロットのコントローラーが含まれています。

| スクリプト | 役割 |
| :--- | :--- |
| `AttachmentEquipSlotController.cs` | プレイヤーの現在の装備スロットを管理します。 |
| `AttachmentShopSlotController.cs` | 購入可能なアタッチメントを表示し、スクラップによる取引を処理します。 |

アタッチメントデータは [`Assets/Scripts/SO/AttachmentDataSO.cs`](Assets/Scripts/SO/AttachmentDataSO.cs) で定義され、[`AttachmentDatabase.cs`](Assets/Scripts/SO/AttachmentDatabase.cs) でインデックス管理されています。

### スキルカスタマイズドロップダウン

[`Assets/Scripts/UI/DropDown/`](Assets/Scripts/UI/DropDown/) は、スキルの閲覧と装備のためのジャンルフィルタードロップダウンを提供します。

| スクリプト | 役割 |
| :--- | :--- |
| `DropdownGenreSelectButton.cs` | 単一のジャンルフィルターボタンを表します。 |
| `DropdownGenreSelectButtonController.cs` | ドロップダウンリスト全体を管理し、カテゴリ別にスキルビューをフィルタリングします。 |

スキルアセットは [`Assets/Scripts/SO/SkillSO.cs`](Assets/Scripts/SO/SkillSO.cs) で定義され、[`SkillDatabase.cs`](Assets/Scripts/SO/SkillDatabase.cs) でインデックス管理されています。スキルはインサイトポイント（IP）を消費することで、最大 **5 段階の覚醒レベル**を解放できます。

---

## クレジット

| アセット | 出典 |
| :--- | :--- |
| BGM | https://bgmer.net/music/136 |
| SE（爆発音） | https://sounddictionary.info/battle-1/ |
| BGM（ゲームオーバー） | https://bgmer.net/music/259 |
