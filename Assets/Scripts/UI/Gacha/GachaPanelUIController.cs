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

    GachaSystemController gsCtrl;

    void Awake()
    {
        gsCtrl = GetComponent<GachaSystemController>();

        if (gsCtrl != null)
        {
            // ボタンをリスナー登録
            singlePullButton.onClick.AddListener(gsCtrl.SinglePullOnClick);
            multiPullButton.onClick.AddListener(gsCtrl.MultiPullOnClick);
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
    public void SetInit(int nowGachaLevel)
    {
        nowLevelText.text = $"{nowGachaLevel}";
        // 辞書を値だけ取得するリストに変換
        List<float> rates = PlayerStatusSO.GachaProbabilityTable.GetRatesByLevel(nowGachaLevel).Values.ToList();
        for (int ii = 0; ii < proElems.Length; ii++)
        {
            float foor = Mathf.Floor(rates[ii] * 10) / 10;
            // 排出確率反映
            proElems[ii].probabilityText.text = $"{foor}%";
        }
    }
}