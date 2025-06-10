using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ガチャのシステムを管理するクラス
/// 基底クラス
/// </summary>
[RequireComponent(typeof(GachaPanelUIController))]
public abstract class GachaSystemController : MonoBehaviour
{
    GachaPanelUIController gpUICtrl;
    int nowGachaLevel;
    int nowPullCount;

    void Awake()
    {
        gpUICtrl = GetComponent<GachaPanelUIController>();

        nowGachaLevel = 3;      // テスト : 基本0
        gpUICtrl.SetInit(nowGachaLevel, nowPullCount);
    }

    /// <summary>
    /// レアリティを選出する
    /// </summary>
    protected Rarity Draw()
    {
        // ガチャレベルに応じて排出確率を変化させた値を取得
        Dictionary<Rarity, float> rates = GachaDefine.GachaProbabilityTable.GetRatesByLevel(nowGachaLevel);
        float rand = Random.Range(0f, 100f);
        float cumulative = 0f;

        foreach (var pair in rates)
        {
            cumulative += pair.Value;
            if (rand < cumulative)
            {
                return pair.Key;
            }
        }

        return Rarity.NON;
    }

    protected void CheckLevelUpGacha(int pullCount)
    {
        // 引いた回数を足していく
        nowPullCount += pullCount;
        int nextLevelPullCount = GachaDefine.GachaLevelProgression.GetRequiredPullsForNextLevel(nowGachaLevel);

        // 必要回数を超えていたら
        if (nowPullCount >= nextLevelPullCount)
        {
            nowGachaLevel++;        // ガチャレベルを上昇
            nowPullCount -= nextLevelPullCount;     // 差分を求める
        }

        // UIの更新
        gpUICtrl.SetInit(nowGachaLevel, nowPullCount);
    }

    /// <summary>
    /// 単発ガチャボタンが押された時の処理
    /// </summary>
    public abstract void SinglePullOnClick();

    /// <summary>
    /// 一括ガチャボタンが押された時の処理
    /// </summary>
    public abstract void MultiPullOnClick(int pullCount);
}