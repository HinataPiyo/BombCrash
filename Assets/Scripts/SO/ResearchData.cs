using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchData", menuName = "SO/ResearchData")]
public class ResearchData : ScriptableObject
{
    public const float explosionRadius = 0.1f;       // 10%
    public const float bombCreateSpeed = 0.08f;
    public const int bombStockAmountUp = 1;
    public const float attackDamageUp = 0.05f;        // 5%
    public const int throwAmount = 1;

    [Header("表示名")] public string displayName;
    [Header("画像"), SerializeField] public Sprite icon;
    [Header("必要ウェーブ数")] public int requiredWave;
    [Header("必要スクラップ数")] public int scrapCost;
    [Header("必要WAVEポイント")] public int wavePointCost;
    [Header("前提研究")] public List<ResearchData> requiredResearchIds; // 前提研究

    [Header("階層 (ツリーの深さ)")] public int tier;        // ツリーの階層
    [Header("研究の状態")] public ResearchState state;      // 研究の状態

    public int GetDynamicWaveCost()
    {
        return requiredWave + (tier * 5);   // 階層ごとに5増加
    }

    public int GetDynamicScrapCost()
    {
        return scrapCost + (tier * 10);     // 階層ごとに10増加
    }

    public int GetDynamicWavePointCost()
    {
        return wavePointCost + (tier * 3);  // 階層ごとに3増加
    }

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