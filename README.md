# BombCrash

**BombCrash** is a 2D action game built with Unity in which a player armed with powerful freeze bombs must destroy waves of countdown enemy bombs before they detonate. Defeat enemies to collect Scrap and InsightPoints, upgrade your skills and equipment, and see how far through the waves you can survive.

> For the full game manual (Japanese), see [`Assets/Docs/README.md`](Assets/Docs/README.md).  
> For the script architecture overview, see [`Assets/Docs/documentation.md`](Assets/Docs/documentation.md).  
> For the story background, see [`Assets/Docs/story.md`](Assets/Docs/story.md).

---

## Table of Contents

1. [Enemy Characters and Bosses](#enemy-characters-and-bosses)
2. [Bomb Mechanics](#bomb-mechanics)
3. [Ultimate Skills](#ultimate-skills)
4. [Gameplay System](#gameplay-system)
5. [User Interface](#user-interface)
6. [Credits](#credits)

---

## Enemy Characters and Bosses

Enemy bombs are defined in [`Assets/Scripts/SO/EnemySO.cs`](Assets/Scripts/SO/EnemySO.cs) and spawn according to wave rules.

| Property | Description |
| :--- | :--- |
| **HP** | Each enemy has a base max HP (`defaultMaxHp`) that scales up as waves progress (`upMaxHp`). |
| **Countdown** | A visible timer counts down to zero; reaching zero causes a game-ending explosion. |
| **Scrap drop** | Defeating an enemy drops a configurable amount of Scrap used for upgrades. |
| **Wave entry** | Every enemy type tracks which wave it first appeared in (`startWave`). |

**Bosses** extend `EnemySO` (see [`BossSO.cs`](Assets/Scripts/SO/BossSO.cs)) and come in three variants:

- `boss_Ver1_0`
- `boss_Ver1_5`
- `boss_Ver2_0`

A **Stampede** event (`Assets/Scripts/System/STANPEDE/`) spawns a surge of enemies every 10 waves.

---

## Bomb Mechanics

The player's primary weapon is the freeze bomb. Core logic lives in [`Assets/Scripts/System/Bomb/BombBase.cs`](Assets/Scripts/System/Bomb/BombBase.cs) and data is managed via [`Assets/Scripts/SO/BombSO.cs`](Assets/Scripts/SO/BombSO.cs).

| Property | Description |
| :--- | :--- |
| **Base attack damage** | Default value of `6.5`, scaled by a per-bomb coefficient. |
| **Explosion radius** | Controls the area-of-effect; also scaled by a coefficient. |
| **Critical hits** | On each throw, `BombSO` checks the player's `CriticalChance`. A successful roll multiplies damage by `CriticalDamage`. |
| **Player status modifiers** | Equipped attachments can raise `BombAttackDamageUp`, `ExplosionRadiusUp`, `BombStockAmountUp`, `BombCreateSpeedUp`, `CriticalDamageUp`, and `CriticalChanceUp`. |

After launch, the bomb travels toward the aim point, expands, contracts, then detonates — dealing damage to all enemies within its explosion radius. Explosions also trigger a camera shake (see [Gameplay System](#gameplay-system)).

The player starts with **3 bomb slots** (`defaultHaveBomb = 3`); bombs regenerate every **2 seconds** (`createBombTime = 2f`) by default. Both values are adjustable via attachments.

---

## Ultimate Skills

Ultimate skills are defined as abstract `ScriptableObject` assets (see [`Assets/Scripts/SO/UltimateSO.cs`](Assets/Scripts/SO/UltimateSO.cs)) and executed by [`Assets/Scripts/System/Ultimate/UltimateController.cs`](Assets/Scripts/System/Ultimate/UltimateController.cs).

| Property | Description |
| :--- | :--- |
| **Activation** | Press **V** when the ultimate is ready. |
| **Cooldown** | Defined per ultimate asset (`coolTime`); a UI slider tracks progress. |
| **Customization** | The active ultimate is set on `PlayerStatusSO.UltimateSO`, making it straightforward to swap in a different ultimate asset. |

While an ultimate is active, its bomb object persists on the field (only its `SpriteRenderer` is hidden after detonation), enabling sustained area effects.

---

## Gameplay System

### Otomo (Support AI)

[`Assets/Scripts/System/Otomo/OtomoMovement.cs`](Assets/Scripts/System/Otomo/OtomoMovement.cs) controls the companion character that follows the player. The Otomo is **unlocked automatically** once the player's highest-reached wave exceeds wave 10 (`maxWaveReleaseOtomo = 10` in `PlayerStatusSO`).

Skills assigned to the Otomo are managed by [`Assets/Scripts/System/Otomo_Skill/OtomoSkillManager.cs`](Assets/Scripts/System/Otomo_Skill/OtomoSkillManager.cs). Up to three skill slots (Z / X / C) can be configured; skills support both **auto** (default) and **manual** activation modes.

### Wave Progression

Wave logic is split across three files in [`Assets/Scripts/System/Wave/`](Assets/Scripts/System/Wave/):

| Script | Responsibility |
| :--- | :--- |
| `WaveManager.cs` | Tracks the current wave count and manages the enemy list. |
| `WaveGenerator.cs` | Reads `EndlessWaveRule` ScriptableObjects to determine which enemies appear and how many. |
| `EnemySpawnController.cs` | Instantiates enemies in the scene. |

Clearing all enemies on the field advances the wave. Wave data assets live in [`Assets/Scripts/SO/EndlessWaveRule.cs`](Assets/Scripts/SO/EndlessWaveRule.cs).

### Camera Controls

[`Assets/Scripts/System/GameSysytem/CameraShake.cs`](Assets/Scripts/System/GameSysytem/CameraShake.cs) uses **Unity Cinemachine** (`CinemachineBasicMultiChannelPerlin`) to shake the camera on events such as bomb explosions and the game-over sequence. The shake duration, amplitude, and frequency are all configurable per call.

---

## User Interface

### Attachment Management

[`Assets/Scripts/UI/Atatchment/`](Assets/Scripts/UI/Atatchment/) contains the attachment shop and equip-slot controllers:

| Script | Responsibility |
| :--- | :--- |
| `AttachmentEquipSlotController.cs` | Manages the player's currently equipped attachment slots. |
| `AttachmentShopSlotController.cs` | Displays purchasable attachments and handles Scrap transactions. |

Attachment data is defined in [`Assets/Scripts/SO/AttachmentDataSO.cs`](Assets/Scripts/SO/AttachmentDataSO.cs) and indexed by [`AttachmentDatabase.cs`](Assets/Scripts/SO/AttachmentDatabase.cs).

### Skill Customization Dropdowns

[`Assets/Scripts/UI/DropDown/`](Assets/Scripts/UI/DropDown/) provides genre-filter dropdowns for browsing and equipping skills:

| Script | Responsibility |
| :--- | :--- |
| `DropdownGenreSelectButton.cs` | Represents a single genre filter button. |
| `DropdownGenreSelectButtonController.cs` | Manages the full dropdown list and filters the skill view by category. |

Skill assets are defined in [`Assets/Scripts/SO/SkillSO.cs`](Assets/Scripts/SO/SkillSO.cs) and indexed by [`SkillDatabase.cs`](Assets/Scripts/SO/SkillDatabase.cs). Skills support up to **5 awakening levels** unlocked by spending InsightPoints (IP).

---

## Credits

| Asset | Source |
| :--- | :--- |
| BGM | https://bgmer.net/music/136 |
| SE (Explosion) | https://sounddictionary.info/battle-1/ |
| BGM (Game Over) | https://bgmer.net/music/259 |
