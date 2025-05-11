using System.Collections.Generic;
using UnityEngine;

public class OtomoSkillRunner : MonoBehaviour
{
    List<SkillSO> equippedSkill = new List<SkillSO>();
    void Start()
    {
        if(OtomoSkillManager.Instance != null) 
        equippedSkill = OtomoSkillManager.Instance.EquippedSkill;
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

        StartCoroutine(equippedSkill[index].SkillLogicBase.ExecuteFlow());         // スキルを発動
    }
}
