using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillDetailPanel : MonoBehaviour
{
    public static OtomoSkillDetailPanel Instance;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI coolTimeText;
    [SerializeField] TextMeshProUGUI currentEffectText;
    [SerializeField] TextMeshProUGUI nextEffectText;
    [SerializeField] TextMeshProUGUI proficiencyText;
    [SerializeField] Slider proficiencySlider;
    [SerializeField] TextMeshProUGUI insightPointText;
    [SerializeField] Animator iconAnim;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ResetTextFields();
    }

    public void ResetTextFields()
    {
        icon.sprite = null;
        icon.enabled = false;
        nameText.text = "-";
        rarityText.text = "-";
        levelText.text = "-";
        coolTimeText.text = "-";
        currentEffectText.text = "";
        nextEffectText.text = "";
        proficiencyText.text = "-/-";
        insightPointText.text = "-";
    }

    public void SetText(SkillSO skillSO)
    {
        if(skillSO.Icon != null)
        {
            icon.sprite = skillSO.Icon;
            icon.enabled = true;
            iconAnim.SetTrigger("Click");
        }
        nameText.text = $"{skillSO.Name}";
        rarityText.text = $"{skillSO.Rarity}";
        levelText.text = $"{skillSO.Level}";
        coolTimeText.text = $"{skillSO.CoolTime}";
        currentEffectText.text = $"{skillSO.Effect}";
        nextEffectText.text = "";
        proficiencyText.text = $"{skillSO.CurrentProficiency} / {skillSO.MaxProficiency}";
        insightPointText.text = "-";
    }
}
