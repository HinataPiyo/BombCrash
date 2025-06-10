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

    void Awake()
    {
        gpUICtrl = GetComponent<GachaPanelUIController>();

        nowGachaLevel = 3;
        gpUICtrl.SetInit(nowGachaLevel);
    }

    /// <summary>
    /// レアリティを選出する
    /// </summary>
    protected Rarity Draw()
    {
        // ガチャレベルに応じて排出確率を変化させた値を取得
        Dictionary<Rarity, float> rates = PlayerStatusSO.GachaProbabilityTable.GetRatesByLevel(nowGachaLevel);
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

    /// <summary>
    /// 単発ガチャボタンが押された時の処理
    /// </summary>
    public abstract void SinglePullOnClick();

    /// <summary>
    /// 一括ガチャボタンが押された時の処理
    /// </summary>
    public abstract void MultiPullOnClick(int pullCount);
}