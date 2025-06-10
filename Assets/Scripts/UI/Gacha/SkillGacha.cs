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
    public override void MultiPullOnClick()
    {

    }
}