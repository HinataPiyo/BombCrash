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
        // UIを更新する & ガチャレベルが超えられるか確認
        CheckLevelUpGacha(1);

        // テスト
        Debug.Log(RandomSelectSkillSO(rarity)?.Name + "が選出された");
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

        // UIを更新する & ガチャレベルが超えられるか確認
        CheckLevelUpGacha(pullCount);
    }

    /// <summary>
    /// レアリティに応じたSkillSOをランダムに取得する
    /// </summary>
    public SkillSO RandomSelectSkillSO(Rarity rarity)
    {
        List<SkillSO> skills = new List<SkillSO>();

        // 引数のレアリティと一致しているSkillOSをDBから選出
        foreach (SkillSO skillSO in skillDB.SkillDB)
        {
            if (skillSO.Rarity != rarity) continue;
            skills.Add(skillSO);
        }

        // 該当スキルがなければnullを返す
        if (skills.Count == 0)
        {
            Debug.LogWarning($"レアリティ{rarity}のSkillSOが見つかりませんでした。");
            return null;
        }

        // 選出されたSkillSOのリストからランダムに抽選
        int rand = Random.Range(0, skills.Count);
        return skills[rand];
    }
}