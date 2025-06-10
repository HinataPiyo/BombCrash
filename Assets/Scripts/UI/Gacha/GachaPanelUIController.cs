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

    [SerializeField] ProbabilityElement[] proElems;
    [SerializeField] TextMeshProUGUI nowLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText;
    [SerializeField] Slider nextLevelSlider;

    [SerializeField] Button singlePullButton;
    [SerializeField] Button multiPullButton;
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
    ///  外部からの初期化処理
    /// </summary>
    public void SetInit(int nowGachaLevel, int nowPullCount)
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
}