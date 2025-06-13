using System.Collections.Generic;
using UnityEngine;

public class SkillGacha : GachaSystemController
{
    [SerializeField] SkillDatabase skillDB;

    void Start()
    {
        gpUICtrl.SetInit(GachaDefine.SkillGacha_SinglePullCost);
        gpUICtrl.CheckGachaButton();
    }

    /// <summary>
    /// 単発ガチャボタンが押された時の処理
    /// </summary>
    public override void SinglePullOnClick()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        Rarity rarity = Draw();     // レアリティを選出
        // UIを更新する & ガチャレベルが超えられるか確認
        CheckLevelUpGacha(1);
        ApplyGachaCost(GachaDefine.SkillGacha_SinglePullCost);      // リソースにコストを適応
        gpUICtrl.CheckGachaButton();                                // ボタンが押せるかの確認

        SkillSO skillSO = RandomSelectSkillSO(rarity);              // ランダムにレアリティに合ったSkillSOを取得
        SkillInventoryManager.Instance.SetSkillStock(skillSO);      // インベントリからDBにアクセスしてストックする
    }

    /// <summary>
    /// 一括ガチャボタンが押された時の処理
    /// </summary>
    public override void MultiPullOnClick(int pullCount)
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        List<Rarity> rarities = new List<Rarity>();
        for (int ii = 0; ii < pullCount; ii++)
        {
            Rarity rarity = Draw();
            SkillSO skillSO = RandomSelectSkillSO(rarity);

            SkillInventoryManager.Instance.SetSkillStock(skillSO);      // インベントリからDBにアクセスしてストックする
            // 選出したレアリティをリストに格納
            rarities.Add(rarity);
        }

        ApplyGachaCost(GachaDefine.SkillGacha_SinglePullCost * pullCount);      // リソースにコストを適応
        gpUICtrl.CheckGachaButton();        // ボタンが押せるかの確認
        CheckLevelUpGacha(pullCount);       // UIを更新する & ガチャレベルが超えられるか確認
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