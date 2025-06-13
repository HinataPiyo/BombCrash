using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillInventorySlot : SkillSlotBase
{
    [SerializeField] Slider skillStockSlider;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        slotButton.onClick.AddListener(ButtonOnClick);
    }

    /// <summary>
    /// スロット生成時の初期化処理
    /// </summary>
    /// <param name="skillSO"></param>
    public override void SetSkill(SkillSO skillSO)
    {
        base.SetSkill(skillSO);

        // スキルストックスライダーを更新
        skillStockSlider.maxValue = skillSO.MaxAwaking() ? 1 : skillSO.GetNeedStockCount();
        skillStockSlider.value = skillSO.MaxAwaking() ? 1 : skillSO.SkillStock;
    }

    /// <summary>
    /// UIを更新する
    /// </summary>
    public void UpdateUI()
    {
        base.SetSkill(m_skillSO);

        // スキルストックスライダーを更新
        skillStockSlider.maxValue = m_skillSO.MaxAwaking() ? 1 : m_skillSO.GetNeedStockCount();
        skillStockSlider.value = m_skillSO.MaxAwaking() ? 1 : m_skillSO.SkillStock;
    }

    /// <summary>
    /// スロットが押された時の処理
    /// </summary>
    void ButtonOnClick()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.Slot_Click);
        anim.SetTrigger("Click");

        // スキル変更中だった場合
        if(OtomoSkillManager.Instance.isEquipmentChangeNow == true)
        {
            // スキルを変更
            OtomoSkillStatusSlotController skillSlotController = FindAnyObjectByType<OtomoSkillStatusSlotController>();
            skillSlotController.SkillChange_SetSkill(m_skillSO);
        }
        else
        {
            // 強化画面の詳細パネルにセット
            OtomoSkillDetailPanel.Instance.SetText(m_skillSO);
        }
    }
}
