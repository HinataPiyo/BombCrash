using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtomoSkillRunner : MonoBehaviour
{
    List<SkillSO> equippedSkill = new List<SkillSO>();
    [SerializeField] Transform skillSlotParent;
    [SerializeField] EquipmentNowSkilSlot[] equipmentNowSkilSlots;
    void Start()
    {
        if(OtomoSkillManager.Instance != null) 
        equippedSkill = OtomoSkillManager.Instance.EquippedSkill;
        equipmentNowSkilSlots = skillSlotParent.GetComponentsInChildren<EquipmentNowSkilSlot>();

        // スキルのスロットを更新する
        for (int ii = 0; ii < equipmentNowSkilSlots.Length; ii++)
        {
            // スキルのスロットを更新する
            equipmentNowSkilSlots[ii].SkillSO = ii < equippedSkill.Count ? equippedSkill[ii] : null;
            equipmentNowSkilSlots[ii].UpdateSlot(equipmentNowSkilSlots[ii].SkillSO);
            if(equippedSkill[ii] == null) continue;
            equippedSkill[ii].IsEndCoolTime = true; // クールタイムが終了している
        }
    }

    void Update()
    {
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
        if (equippedSkill[index].SkillLogicBase == null) return;
        if (equippedSkill[index].SkillLogicBase.ExecuteFlow() == null) return;

        // クールタイムが終了していない場合は何もしない
        if (equippedSkill[index].IsEndCoolTime == true)
        {
            StartCoroutine(equippedSkill[index].SkillLogicBase.ExecuteFlow());     // スキルを発動
        }
        else
        {
            return;
        }

        StartCoroutine(UpdateCoolTime(index));                                     // クールタイムを更新
    }

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
