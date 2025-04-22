using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillStatusSlot : SkillSlotBase
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI effectText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI coolTimeText;

    public override void SetSkill(SkillSO skillSO)
    {
        base.SetSkill(skillSO);
        nameText.text = skillSO.Name;
        effectText.text = skillSO.Effect;
        levelText.text = "Lv " + skillSO.Level.ToString("F0");
        coolTimeText.text = "CT:" + skillSO.CoolTime.ToString("F0");
    }
}