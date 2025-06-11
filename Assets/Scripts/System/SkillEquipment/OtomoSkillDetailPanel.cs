using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillDetailPanel : MonoBehaviour
{
    public static OtomoSkillDetailPanel Instance;
    [SerializeField] PlayerStatusSO playerSO;
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

    SkillSO m_skillSO;
    public SkillSO SkillSO => m_skillSO;

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
        m_skillSO = skillSO;
        if(skillSO.Icon != null)
        {
            icon.sprite = skillSO.Icon;
            icon.enabled = true;
            iconAnim.SetTrigger("Click");
        }
        nameText.text = $"{skillSO.Name}";
        rarityText.text = $"{skillSO.Rarity}";
        levelText.text = $"{skillSO.Level + 1}";
        coolTimeText.text = $"{skillSO.CoolTime}";
        currentEffectText.text = $"{skillSO.Effect}";
        nextEffectText.text = "";
        insightPointText.text = $"{skillSO.InsightPointFetchCost()}";
    }

    /// <summary>
    /// スキルのレベルアップ処理
    /// </summary>
    public bool CheckIPCostAndProficiency()
    {
        if (m_skillSO == null) return false;
        return playerSO.InsightPointHaveAmount >= m_skillSO.InsightPointFetchCost();
    }
    
}
