using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentNowSkilSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI coolTimeText;
    [SerializeField] Image coolTimeImage;
    SkillSO skillSO;

    public SkillSO SkillSO { get => skillSO; set => skillSO = value; }

    /// <summary>
    /// スキルのスロットを更新する
    /// </summary>
    public void UpdateSlot(SkillSO skill)
    {
        if(skill == null)
        {
            icon.enabled = false;
            coolTimeText.text = "";
            coolTimeImage.fillAmount = 0;
            return;
        }

        icon.enabled = true;
        icon.sprite = skill.Icon;
        coolTimeText.text = "";
        coolTimeImage.fillAmount = 0;
    }

    /// <summary>
    /// クールタイム用のUIを更新する
    /// </summary>
    public void UpdateCoolTimeField(float coolTime)
    {
        coolTimeText.text = coolTime.ToString("F0");
        coolTimeImage.fillAmount = coolTime / skillSO.CoolTime;
    }

    /// <summary>
    /// クールタイムが終了したときの処理
    /// </summary>
    public void EndCoolTime()
    {
        coolTimeText.text = "";
        coolTimeImage.fillAmount = 0;
    }
}