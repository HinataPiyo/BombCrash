using System.Security.Cryptography;
using UnityEngine;

public class SkillSlotController : MonoBehaviour
{
    [SerializeField] Transform equipmentSkillSlot_Parent;
    [SerializeField] OtomoSkillStatusSlot[] equipmentSkillSlot;

    void Start()
    {
        equipmentSkillSlot = equipmentSkillSlot_Parent.GetComponentsInChildren<OtomoSkillStatusSlot>();
        TestSetSkill();
    }

    void TestSetSkill()
    {
        for (int ii = 0; ii < equipmentSkillSlot.Length; ii++)
        {
            if (equipmentSkillSlot[ii].SkillSO == null) continue;
            equipmentSkillSlot[ii].SetSkill(equipmentSkillSlot[ii].SkillSO);
            OtomoSkillManager.Instance.EquippedSkill.Add(equipmentSkillSlot[ii].SkillSO);
        }
    }
}