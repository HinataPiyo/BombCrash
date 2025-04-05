using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchData", menuName = "SO/ResearchData")]
public class ResearchData : ScriptableObject
{
    public const float explosionRadius = 0.1f;       // 10%
    public const float bombCreateSpeed = 0.08f;
    public const int bombStockAmountUp = 1;
    public const float attackDamageUp = 0.1f;        // 5%
    public const int throwAmount = 1;

    public const float dropScrapAmountUp = 0.1f;
    public const float insightPointAmountUp = 0.1f;

    [Header("ジャンル")] public ResearchesGenre genre;
    [Header("識別Enum")] public ResearchName researchName;
    [Header("表示名")] public string displayName;
    [Header("説明")] public string explanation;
    [Header("画像"), SerializeField] public Sprite icon;
    [Header("必要ウェーブ数 / 必要スクラップ数 / 必要WAVEポイント")]
    [SerializeField] int requiredWave;
    [SerializeField] int scrapCost;
    [SerializeField] int insightPointCost;
    [Header("前提研究")] public List<ResearchData> requiredResearchIds; // 前提研究

    [Header("階層 (ツリーの深さ)")] public int tier;        // ツリーの階層
    [Header("研究の状態")] public ResearchState state;      // 研究の状態

    public int RequiredWave { get { return requiredWave + (tier * 5); } }
    public int ScrapCost { get { return scrapCost + (tier * 100); } }
    public int InsightPointCost { get { return insightPointCost + (tier * 5); } }
    
    /// <summary>
    /// 前提研究が全て完了しているか確認する
    /// </summary>
    public bool ClearPrerequisites()
    {
        foreach (var prerequisite in requiredResearchIds)
        {
            if (prerequisite.state != ResearchState.Completed)
            {
                return false; // 前提研究が完了していない
            }
        }
        return true; // すべての前提研究が完了している
    }

}

public enum ResearchState
{
    Locked,    // ロックされている
    Unlocked,  // 解放されている
    Completed  // 研究が終了している
}

public enum ResearchName
{
    ExplosionRadiusUp,
    BombCreateSpeedUp,
    BombStockAmountUp,
    AttackDamageUp,
    ThrowAmountUp,

    DropScrapUp,
    TakeWavePointUp,
}