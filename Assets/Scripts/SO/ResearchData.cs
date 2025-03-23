using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchData", menuName = "SO/ResearchData")]
public class ResearchData : ScriptableObject
{
    [Header("表示名")] public string displayName;
    [Header("必要ウェーブ数")] public int requiredWave;
    [Header("必要スクラップ数")] public int scrapCost;
    [Header("必要WAVEポイント")] public int wavePointCost;
    [Header("前提研究")] public List<ResearchData> requiredResearchIds;   // 前提研究
}