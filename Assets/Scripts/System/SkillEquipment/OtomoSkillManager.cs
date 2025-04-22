using System.Collections.Generic;
using UnityEngine;


public class OtomoSkillManager : MonoBehaviour
{
    public static OtomoSkillManager Instance;
    [SerializeField] SkillSO[] skillSOs;    // 着脱テスト

    [SerializeField] Transform equipmentSkillSlot_Parent;
    [SerializeField] OtomoSkillStatusSlot[] equipmentSkillSlot;

    [SerializeField] List<SkillSO> equippedSkill = new List<SkillSO>();

    [SerializeField] bool test;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        equipmentSkillSlot = equipmentSkillSlot_Parent.GetComponentsInChildren<OtomoSkillStatusSlot>();
    }

    void Update()
    {
        if (test == true)
        {
            TestSetSkill();
            test = false;
        }
    }

    void TestSetSkill()
    {
        for (int ii = 0; ii < equipmentSkillSlot.Length; ii++)
        {
            equipmentSkillSlot[ii].SetSkill(skillSOs[ii]);
            equippedSkill.Add(skillSOs[ii]);
        }
    }

}