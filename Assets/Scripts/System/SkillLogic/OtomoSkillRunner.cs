using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtomoSkillRunner : MonoBehaviour
{
    List<SkillSO> equippedSkill = new List<SkillSO>();
    [SerializeField] EnemySpawnController esCtrl;
    [SerializeField] Transform skillSlotParent;
    [SerializeField] EquipmentNowSkilSlot[] equipmentNowSkilSlots;
    float isAutoWaitTime = 1f; // 自動発動の待機時間

    void Start()
    {
        if(OtomoSkillManager.Instance != null)
        { equippedSkill = OtomoSkillManager.Instance.EquippedSkill; }
        equipmentNowSkilSlots = skillSlotParent.GetComponentsInChildren<EquipmentNowSkilSlot>();

        // スキルのスロットを更新する
        for (int ii = 0; ii < equipmentNowSkilSlots.Length; ii++)
        {
            if (ii < equippedSkill.Count)
            {
                // スキルのスロットを更新する
                equipmentNowSkilSlots[ii].SkillSO = ii < equippedSkill.Count ? equippedSkill[ii] : null;
                equipmentNowSkilSlots[ii].UpdateSlot(equipmentNowSkilSlots[ii].SkillSO);
                if (equippedSkill[ii] == null) continue;        // スキルが装備されていない場合は何もしない
                equippedSkill[ii].IsEndCoolTime = true;         // クールタイムが終了している
            }
            else
            {
                // スキルのスロットを空にする
                equipmentNowSkilSlots[ii].SkillSO = null;
                equipmentNowSkilSlots[ii].UpdateSlot(null);
            }
        }
    }

    void Update()
    {
        for(int ii = 0; ii < equipmentNowSkilSlots.Length; ii++)
        {
            if (equipmentNowSkilSlots[ii].SkillSO == null) continue;

            // 自動になっているかつクールタイムが終了していればスキルを発動する
            if (equipmentNowSkilSlots[ii].SkillSO.IsAuto
            && equipmentNowSkilSlots[ii].SkillSO.IsEndCoolTime)
            {
                if(esCtrl.EnemyList.Count > 0) StartCoroutine(AutoSkillExecute(ii)); // スキルを発動
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SkillExecute(0); // スキル1を発動
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            SkillExecute(1); // スキル2を発動
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            SkillExecute(2); // スキル3を発動
        }
    }

    /// <summary>
    /// スキルを発動する
    /// </summary>
    /// <param name="index">スキルのインデックス</param>
    void SkillExecute(int index)
    {
        if (equippedSkill[index] == null) return;

        // クールタイムが終了していない場合は何もしない
        if (equippedSkill[index].IsEndCoolTime == true)
        {
            equippedSkill[index].Execute();         // スキルを発動
            equippedSkill[index].AddProficiency();  // 熟練度を上昇させる
        }
        else
        {
            return;
        }

        StartCoroutine(UpdateCoolTime(index));                                     // クールタイムを更新
    }

    /// <summary>
    /// 自動発動のスキルを実行する
    /// </summary>
    IEnumerator AutoSkillExecute(int index)
    {
        if (equippedSkill[index] == null) yield break;

        // 自動発動の待機時間を待つ
        yield return new WaitForSeconds(isAutoWaitTime);

        // スキルを発動する
        SkillExecute(index);
    }

    /// <summary>
    /// クールタイムを更新する
    /// </summary>
    IEnumerator UpdateCoolTime(int index)
    {
        if (equippedSkill[index] == null) yield break;

        equippedSkill[index].IsEndCoolTime = false; // クールタイム中にする
        float coolTime = equippedSkill[index].CoolTime;
        while (coolTime > 0)
        {
            equipmentNowSkilSlots[index].UpdateCoolTimeField(coolTime);
            coolTime -= Time.deltaTime;
            yield return null;
        }

        equippedSkill[index].IsEndCoolTime = true;      // クールタイムが終了した
        equipmentNowSkilSlots[index].EndCoolTime();     // クールタイムが終了したときの処理
    }
}
