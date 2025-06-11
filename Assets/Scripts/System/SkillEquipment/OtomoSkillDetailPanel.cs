using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtomoSkillDetailPanel : MonoBehaviour
{
    public static OtomoSkillDetailPanel Instance;
    [SerializeField] PlayerStatusSO playerSO;
    [Header("ステータス")]
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI coolTimeText;
    [Header("効果説明")]
    [SerializeField] TextMeshProUGUI currentEffectText;
    [Header("覚醒")]
    [SerializeField] TextMeshProUGUI awakingCountText;
    [SerializeField] TextMeshProUGUI nextAwakingVulueText;
    [SerializeField] Slider awakingSlider;
    [SerializeField] TextMeshProUGUI insightPointText;
    [Header("コンポーネント")]
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
        coolTimeText.text = "-";
        currentEffectText.text = "";
        awakingCountText.text = "";
        nextAwakingVulueText.text = "-/-";
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

        // ステータス
        nameText.text = skillSO.Name;
        rarityText.text = skillSO.Rarity.ToString();
        coolTimeText.text = skillSO.CoolTime.ToString("F0");
        currentEffectText.text = skillSO.Effect;

        // 覚醒
        awakingCountText.text = skillSO.AwakingCount.ToString();
        nextAwakingVulueText.text = $"{skillSO.SkillStock}/{skillSO.GetNeedStockCount()}";
        awakingSlider.maxValue = skillSO.GetNeedStockCount();
        awakingSlider.value = skillSO.SkillStock;

        // 知見ポイント
        insightPointText.text = skillSO.InsightPointFetchCost().ToString();
    }

    /// <summary>
    /// スキルのレベルアップ処理
    /// </summary>
    public bool CheckIPCostAndProficiency()
    {
        if (m_skillSO == null) return false;
        return playerSO.InsightPointHaveAmount >= m_skillSO.InsightPointFetchCost() && m_skillSO.CanAwaking();
    }
    
}
