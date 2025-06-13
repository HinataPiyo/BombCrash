using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ガチャパネルの一つを管理するクラス
/// </summary>
[RequireComponent(typeof(GachaSystemController))]
public class GachaPanelUIController : MonoBehaviour
{
    [System.Serializable]
    public class ProbabilityElement
    {
        public TextMeshProUGUI rarityText;
        public Color32 rarityColor;
        public TextMeshProUGUI probabilityText;
    }

    [SerializeField] PlayerStatusSO playerSO;
    [SerializeField] ProbabilityElement[] proElems;
    [SerializeField] TextMeshProUGUI nowLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText;
    [SerializeField] Slider nextLevelSlider;

    [Header("Pull Button")]
    [SerializeField] Button singlePullButton;
    [SerializeField] Button multiPullButton;
    [SerializeField] TextMeshProUGUI singleCostText;
    [SerializeField] TextMeshProUGUI multiCostText;

    [Header("Multi Pull Count")]
    [SerializeField] MultiPullCountPanel multiPullCountPanel;
    int multiPullCount = 10;    // テスト

    GachaSystemController gsCtrl;
    public int SinglePullCount { get; private set; }

    void Awake()
    {
        gsCtrl = GetComponent<GachaSystemController>();
        multiPullButton.onClick.AddListener(() => multiPullCountPanel.OnMultiPullCountPanel(multiPullCount));

        if (gsCtrl != null)
        {
            singlePullButton.onClick.AddListener(gsCtrl.SinglePullOnClick);
        }

        for (int ii = 0; ii < proElems.Length; ii++)
        {
            // レアリティを文字列にして取得し設定
            proElems[ii].rarityText.text = SystemDefine.RarityToName(SystemDefine.rarities[ii]);
            // 色を設定
            proElems[ii].rarityText.color = proElems[ii].rarityColor;
        }
    }

    /// <summary>
    /// 外部からのセットが必要な処理をここで行う
    /// </summary>
    /// <param name="singlePullCost">単発を引くのにかかるコスト（ガチャ種によって異なるかもしれないから）</param>
    public void SetInit(int _singlePullCost)
    {
        SinglePullCount = _singlePullCost;
        singleCostText.text = _singlePullCost.ToString();
        multiCostText.text = (_singlePullCost * 10).ToString();
    }

    /// <summary>
    /// UIの更新
    /// </summary>
    public void SetUpdateUI(int nowGachaLevel, int nowPullCount)
    {
        // 現在のレベル
        nowLevelText.text = $"{nowGachaLevel}";

        int nextLevelPullCount = GachaDefine.GachaLevelProgression.GetRequiredPullsForNextLevel(nowGachaLevel);
        // 次のレベルまで
        nextLevelText.text = $"{nowPullCount} / {nextLevelPullCount}";
        nextLevelSlider.maxValue = nextLevelPullCount;
        nextLevelSlider.value = nowPullCount;

        // ガチャレベルに応じて確率を変動
        // 辞書を値だけ取得するリストに変換
        List<float> rates = GachaDefine.GachaProbabilityTable.GetRatesByLevel(nowGachaLevel).Values.ToList();
        for (int ii = 0; ii < proElems.Length; ii++)
        {
            float foor = Mathf.Floor(rates[ii] * 10) / 10;
            // 排出確率反映
            proElems[ii].probabilityText.text = $"{foor}%";
        }
    }

    /// <summary>
    /// コストを支払うことができればボタンを押せるようにし
    /// 出来なければ押せないようにする
    /// </summary>
    public void CheckGachaButton()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        singlePullButton.interactable = CanAffordMultiCount(SinglePullCount);
        multiPullButton.interactable = CanAffordMultiCount(SinglePullCount * 10);
    }

    /// <summary>
    /// コストが支払えるかどうか
    /// </summary>
    public bool CanAffordMultiCount(int count)
    {
        return playerSO.InsightPointHaveAmount >= count;
    }
}