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
    int multiPullCount = 10;    // テスト

    GachaSystemController gsCtrl;

    void Awake()
    {
        gsCtrl = GetComponent<GachaSystemController>();

        if (gsCtrl != null)
        {
            // ボタンをリスナー登録
            singlePullButton.onClick.AddListener(gsCtrl.SinglePullOnClick);
            multiPullButton.onClick.AddListener(() => gsCtrl.MultiPullOnClick(multiPullCount));
        }

        for (int ii = 0; ii < proElems.Length; ii++)
        {
            // レアリティを文字列にして取得し設定
            proElems[ii].rarityText.text = PlayerStatusSO.RarityToName(PlayerStatusSO.rarities[ii]);
            // 色を設定
            proElems[ii].rarityText.color = proElems[ii].rarityColor;
        }
    }

    /// <summary>
    /// 外部からのセットが必要な処理をここで行う
    /// </summary>
    /// <param name="singlePullCost">単発を引くのにかかるコスト（ガチャ種によって異なるかもしれないから）</param>
    public void SetInit(int singlePullCost)
    {
        singleCostText.text = singlePullCost.ToString();
        multiCostText.text = (singlePullCost * 10).ToString();
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
    /// コストを支払うことができるか確認する
    /// </summary>
    public void CanAfford(int singlePullCost)
    {
        Debug.Log("確認されました");

        singlePullButton.interactable = playerSO.InsightPointHaveAmount >= singlePullCost;
        multiPullButton.interactable = playerSO.InsightPointHaveAmount >= singlePullCost * 10;
    }
}