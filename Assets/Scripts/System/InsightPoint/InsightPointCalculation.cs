using UnityEngine;


/// <summary>
/// 知見ポイントの計算を行う
/// </summary>
public class InsightPointCalculation : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] PlayerStatusSO playerSO;
    int insightPoint;
    int bonusInsightPoint;

    /// <summary>
    /// 基礎-知見ポイントの計算
    /// </summary>
    /// <returns></returns>
    public int GetDefaultInsight()
    {
        int count = waveManager.WaveCount;              // wave到達地点を記録
        float rawPoint = Mathf.Pow(count, 1.15f);       // 小数になる
        insightPoint = Mathf.FloorToInt(rawPoint);      // 小数を切り捨てて整数に

        playerSO.InsightPointHaveAmount = insightPoint;

        return insightPoint;
    }

    /// <summary>
    /// ボーナス知見ポイントの計算
    /// </summary>
    /// <returns></returns>
    public int GetInsightBonus()
    {
        bonusInsightPoint = Mathf.CeilToInt(insightPoint);          // 修正する
        playerSO.InsightPointHaveAmount = bonusInsightPoint;

        return bonusInsightPoint;
    }
}