using TMPro;
using UnityEngine;

public class OtomoSkillStatusSlot : SkillSlotBase
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI effectText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI coolTimeText;

    public SkillSO SkillSO => m_skillSO;

    public override void SetSkill(SkillSO skillSO)
    {
        base.SetSkill(skillSO);
        nameText.text = skillSO.Name;
        effectText.text = skillSO.Effect;
        levelText.text = "Lv " + skillSO.Level.ToString("F0");
        coolTimeText.text = "CT:" + skillSO.CoolTime.ToString("F0");
    }
}