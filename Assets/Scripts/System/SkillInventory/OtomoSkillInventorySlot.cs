using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillInventorySlot : SkillSlotBase
{
    [SerializeField] Slider proficiencySlider;

    void Start()
    {
        
    }
    
    public override void SetSkill(SkillSO skillSO)
    {
        base.SetSkill(skillSO);

        // 熟練度の設定
        proficiencySlider.maxValue = skillSO.MaxProficiency;
        proficiencySlider.value = skillSO.CurrentProficiency;
    }
}
