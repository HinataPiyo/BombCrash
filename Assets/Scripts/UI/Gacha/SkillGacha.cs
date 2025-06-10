using System.Collections.Generic;
using UnityEngine;

public class SkillGacha : GachaSystemController
{
    [SerializeField] SkillDatabase skillDB;


    /// <summary>
    /// 単発ガチャボタンが押された時の処理
    /// </summary>
    public override void SinglePullOnClick()
    {
        Rarity rarity = Draw();     // レアリティを選出

        Debug.Log(PlayerStatusSO.RarityToName(rarity) + "が選出された");
    }

    /// <summary>
    /// 一括ガチャボタンが押された時の処理
    /// </summary>
    public override void MultiPullOnClick(int pullCount)
    {
        List<Rarity> rarities = new List<Rarity>();
        for (int ii = 0; ii < pullCount; ii++)
        {
            Rarity rarity = Draw();
            Debug.Log(PlayerStatusSO.RarityToName(rarity) + "が選出された");
            // 選出したレアリティをリストに格納
            rarities.Add(rarity);
        }
    }
}