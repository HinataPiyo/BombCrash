using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillStatusSlot : SkillSlotBase
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI effectText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI coolTimeText;
    Animator anim;

    [Header("スロットの状態"), SerializeField] SkillEquipmentState slotState;
    public SkillSO SkillSO { get { return m_skillSO; } set { m_skillSO = value; } }
    public Button SkillChangeButton => slotButton;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void SetSkill(SkillSO skillSO)
    {
        base.SetSkill(skillSO);
        nameText.text = skillSO.Name;
        effectText.text = skillSO.Effect;
        levelText.text = "Lv " + skillSO.Level.ToString("F0");
        coolTimeText.text = "CT:" + skillSO.CoolTime.ToString("F0");
    }

    public void LockedSlot()
    {
        if(slotState == SkillEquipmentState.Locked)
        {
            slotButton.gameObject.SetActive(false);
            icon.enabled = false;
            rarityText.text = "-";
            nameText.text = "-";
            effectText.text = "-";
            levelText.text = "-";
            coolTimeText.text = "CT:-";
        }
    }

    public void SkillNullSlot()
    {
        if(m_skillSO == null)
        {
            slotButton.gameObject.SetActive(true);
            icon.enabled = false;
            rarityText.text = "-";
            nameText.text = "-";
            effectText.text = "-";
            levelText.text = "-";
            coolTimeText.text = "-";
        }
    }

    public void ClickAnimation() { anim.SetTrigger("Click"); }
}

public enum SkillEquipmentState
{
    Locked,    // ロックされている
    Unlocked,  // 解放されている
}