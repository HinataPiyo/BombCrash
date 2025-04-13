using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchData", menuName = "SO/ResearchData")]
public class ResearchData : ScriptableObject
{
    // 拡張研究の上昇率
    // 爆弾系
    public const float explosionRadius = 0.1f;      // 爆発範囲
    public const float bombCreateSpeed = 0.05f;     // 場弾生成時間5%ずつ減少する( = 95%にする)
    public const int bombStockUp = 1;               // 爆弾ストック数(1個)
    public const float attackDamageUp = 0.1f;       // 攻撃力（10%）
    public const float criticalDamageUp = 0.1f;     // クリティカルダメージ(10%)
    public const float criticalChanceUp = 0.05f;    // クリティカル率(5%)
    public const int throwAmount = 1;               // 同時投擲数(実装未定)

    // 支援系
    public const float scrapBonusUp = 0.1f;
    public const float insightPointUp = 0.1f;

    [Header("ジャンル")] public ResearchesGenre genre;
    [Header("識別Enum")] public StatusName statusName;
    [Header("説明")] public string explanation;
    [Header("画像"), SerializeField] public Sprite icon;
    [Header("必要ウェーブ数 / 必要スクラップ数 / 必要知見ポイント")]
    [SerializeField] int requiredWave;
    [SerializeField] int scrapCost;
    [SerializeField] int insightPointCost;

    [Header("階層 (ツリーの深さ)")] public int tier;        // ツリーの階層

    public int RequiredWave { get { return requiredWave + (tier * 5); } }
    public int ScrapCost { get { return scrapCost + (tier * 100); } }
    public int InsightPointCost { get { return insightPointCost + (tier * 5); } }
}

public enum ResearchState
{
    Locked,    // ロックされている
    Unlocked,  // 解放されている
    Completed  // 研究が終了している
}
