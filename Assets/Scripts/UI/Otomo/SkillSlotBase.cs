using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotBase : MonoBehaviour
{
    [SerializeField] protected SkillSO m_skillSO;
    [SerializeField] protected TextMeshProUGUI rarityText;
    [SerializeField] protected Button slotButton;
    [SerializeField] protected Image icon;

    public SkillSO SkillSO => m_skillSO;

    public virtual void SetSkill(SkillSO skillSO)
    {
        // 通常表示
        m_skillSO = skillSO;

        if (skillSO.Icon != null)
        {
            icon.sprite = skillSO.Icon;
            icon.enabled = true;
        }
        else
        {
            icon.enabled = false;
        }

        rarityText.text = m_skillSO.Rarity.ToString();
        rarityText.enabled = true;
    }
    
}