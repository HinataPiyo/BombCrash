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
    [Header("スキルの自動か手動か")]
    [SerializeField] Button autoManualButton;
    [SerializeField] Animator autoManualAnim;
    Animator anim;

    [Header("スロットの状態"), SerializeField] SkillEquipmentState slotState;
    public SkillEquipmentState SlotState => slotState;
    public Button SkillChangeButton => slotButton;

    void Start()
    {
        autoManualButton.onClick.AddListener(ChangeAutoOrManual);
        anim = GetComponent<Animator>();
        autoManualButton.gameObject.SetActive(m_skillSO != null);
    }

    public override void SetSkill(SkillSO skillSO)
    {
        if (skillSO == null)
        {
            m_skillSO = null;
            // スロットの中身が空だった場合テキストの表示を簡素化する
            SkillNullSlot();
            return;
        }

        base.SetSkill(skillSO);
        nameText.text = skillSO.Name;
        effectText.text = skillSO.Effect;
        levelText.text = "Lv " + (skillSO.Level + 1).ToString("F0");
        coolTimeText.text = "CT:" + skillSO.CoolTime.ToString("F0");

        m_skillSO.IsAuto = false;       // 初期は必ず手動で初期化
        autoManualAnim.SetBool("ChnageAuto", skillSO.IsAuto);
        autoManualButton.gameObject.SetActive(m_skillSO != null);
    }

    public void LockedSlot()
    {
        if (slotState == SkillEquipmentState.Locked)
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
        if (m_skillSO == null)
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

    /// <summary>
    /// スキルを自動で使用するか手動で使用するかのボタンが押された時の処理
    /// </summary>
    void ChangeAutoOrManual()
    {
        // 手動、自動を反転させる
        m_skillSO.IsAuto = !m_skillSO.IsAuto;
        autoManualAnim.SetBool("ChangeAuto", m_skillSO.IsAuto);
    }
}

public enum SkillEquipmentState
{
    Locked,    // ロックされている
    Unlocked,  // 解放されている
}