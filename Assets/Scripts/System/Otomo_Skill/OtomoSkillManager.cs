using System.Collections.Generic;
using UnityEngine;


public class OtomoSkillManager : MonoBehaviour
{
    public static OtomoSkillManager Instance;

    [Header("装備中のスキル"), SerializeField] List<SkillSO> equippedSkill = new List<SkillSO>();
    public List<SkillSO> EquippedSkill { get { return equippedSkill; } }

    public bool isEquipmentChangeNow;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    /// <summary>
    /// 出撃するタイミングで装備を反映させる
    /// </summary>
    /// <param name="newSkills"></param>
    public void ReplaceEquippedSkills()
    {
        // 現在の装備スキルをクリア
        equippedSkill.Clear();
        OtomoSkillStatusSlotController slotsCont = FindAnyObjectByType<OtomoSkillStatusSlotController>();
        // 新しいスキルを追加
        foreach (var skill in slotsCont.EquipmentSkillSlot)
        {
            equippedSkill.Add(skill.SkillSO);
        }

        Debug.Log("装備スキルを新しいリストに入れ替えました。");
    }

}