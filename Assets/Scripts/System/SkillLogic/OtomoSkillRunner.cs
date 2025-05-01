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
            foreach (var skill in equippedSkill)
            {
                if (skill == null) continue;
                StartCoroutine(skill.SkillLogicBase.ExecuteFlow());         // スキルを発動
            }
        }
    }
}