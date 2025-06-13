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
    [SerializeField] TextMeshProUGUI nextEffectText;
    [Header("覚醒")]
    [SerializeField] TextMeshProUGUI awakeningCountText;
    [SerializeField] TextMeshProUGUI nextAwakingVulueText;
    [SerializeField] Slider awakingSlider;
    [SerializeField] TextMeshProUGUI insightPointText;
    [Header("コンポーネント")]
    [SerializeField] Animator iconAnim;

    SkillSO m_skillSO;
    public SkillSO SkillSO => m_skillSO;

    void Awake()
    {
        if (Instance == null) Instance = this;
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
        nextEffectText.text = "";
        awakeningCountText.text = "-";
        nextAwakingVulueText.text = "-/-";
        insightPointText.text = "-";
    }

    public void SetText(SkillSO skillSO)
    {
        m_skillSO = skillSO;
        if (skillSO.Icon != null)
        {
            icon.sprite = skillSO.Icon;
            icon.enabled = true;
            iconAnim.SetTrigger("Click");
        }

        // ステータス
        nameText.text = skillSO.Name;
        rarityText.text = skillSO.Rarity.ToString();
        coolTimeText.text = skillSO.CoolTime.ToString("F0");

        // 覚醒
        if (!skillSO.MaxAwaking())      // 覚醒回数がMaxに達していないとき
        {

            currentEffectText.text = skillSO.GetEffectDiscription(skillSO.AwakeningCount);
            nextEffectText.text = skillSO.GetEffectDiscription(skillSO.AwakeningCount + 1);

            awakeningCountText.text = skillSO.AwakeningCount.ToString();
            nextAwakingVulueText.text = $"{skillSO.SkillStock}/{skillSO.GetNeedStockCount()}";
            insightPointText.text = skillSO.InsightPointFetchCost().ToString();

            awakingSlider.maxValue = skillSO.GetNeedStockCount();
            awakingSlider.value = skillSO.SkillStock;
        }
        else
        {

            currentEffectText.text = skillSO.GetEffectDiscription(skillSO.AwakeningCount);
            nextEffectText.text = "";

            awakeningCountText.text = "MAX";
            nextAwakingVulueText.text = $"{skillSO.SkillStock}/MAX";
            insightPointText.text = "-";
            
            awakingSlider.maxValue = 1;
            awakingSlider.value = 1;
        }
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
