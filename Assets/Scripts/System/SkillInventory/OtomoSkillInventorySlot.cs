using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillInventorySlot : SkillSlotBase
{
    [SerializeField] Slider proficiencySlider;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        slotButton.onClick.AddListener(ButtonOnClick);
    }
    
    public override void SetSkill(SkillSO skillSO)
    {
        base.SetSkill(skillSO);

        // 熟練度の設定
        proficiencySlider.maxValue = skillSO.MaxProficiency;
        proficiencySlider.value = skillSO.CurrentProficiency;
    }

    void ButtonOnClick()
    {
        anim.SetTrigger("Click");
        OtomoSkillDetailPanel.Instance.SetText(m_skillSO);
    }
}
