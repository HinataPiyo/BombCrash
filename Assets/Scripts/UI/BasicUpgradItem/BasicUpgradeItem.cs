using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicUpgradeItem : MonoBehaviour
{
    StatusTextController statusTextCont;
    [SerializeField] BasicUpgradeData.Data myB_Data;
    [SerializeField] Button panelButton;
    [SerializeField] GameObject openPanel;
    [SerializeField] PlayerStatusSO playerSO;
    [Header("強化ボタンの設定")]
    [SerializeField] CanvasGroup upgradeButton_CGroup;
    [SerializeField] Button upgradeButton;
    [Header("強化ステータス")]
    [SerializeField] TextMeshProUGUI nowText;
    [SerializeField] TextMeshProUGUI nextText;
    const float CantButtonClick_Alpha = 0.3f;
    [Header("その他")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [Header("所持数")]
    [SerializeField] TextMeshProUGUI hasScrap;
    [SerializeField] TextMeshProUGUI hasInsight;
    [Header("必要個数")]
    [SerializeField] TextMeshProUGUI scrapText;
    [SerializeField] TextMeshProUGUI insightText;
    Animator anim;

    void Start()
    {
        statusTextCont = FindAnyObjectByType<StatusTextController>();
        anim = GetComponent<Animator>();

        panelButton.onClick.AddListener(OpenOrClosePanel);
        upgradeButton.onClick.AddListener(UpgradeButtonOnClick);
    }

    public void SetData(BasicUpgradeData.Data data)
    {
        myB_Data = data;
        UpdateTextValue();
    }

    /// <summary>
    /// テキストの更新
    /// </summary>
    public void UpdateTextValue()
    {
        hasScrap.text = playerSO.ScrapHaveAmount.ToString("F0");
        hasInsight.text = playerSO.InsightPointHaveAmount.ToString("F0");

        float now = myB_Data.UpgradeStatusValue(-1);
        float next = myB_Data.UpgradeStatusValue(0);

        // マッピングを定義(基礎値を返す)
        var statusValueMap = new Dictionary<StatusName, float>
        {
            { StatusName.BombAttackDamageUp, BombSO.Default_Damage },
            { StatusName.BombStockAmountUp, PlayerStatusSO.maxHaveBomb },
            { StatusName.BombCreateSpeedUp, PlayerStatusSO.createBombTime },
            { StatusName.ExplosionRadiusUp, BombSO.Default_ExplosionRadius }
        };

        // 対応する値を加算(基礎値を加算)
        if (statusValueMap.TryGetValue(myB_Data.statusName, out float additionalValue))
        {
            now += additionalValue;
            next += additionalValue;
        }

        if(myB_Data.statusName == StatusName.DropScrapUp)
        {
            nowText.text = (now * 100).ToString("F0") + "%";
            nextText.text = (next * 100).ToString("F0") + "%";
        }
        else
        {
            nowText.text = now.ToString("F1");
            nextText.text = next.ToString("F1");
        }
        

        nameText.text = myB_Data.name;
        levelText.text = myB_Data.level.ToString("F0");
        scrapText.text = myB_Data.currentScrap.ToString("F0");
        insightText.text = myB_Data.currentInsight.ToString("F0");

        IsPriceOverBudget();        // 現在の所持数が超えているか否か判別
    }

    /// <summary>
    /// 現在の所持数が超えているか否か判別
    /// </summary>
    bool IsPriceOverBudget()
    {
        // スクラップと知見ポイントが超えているか確認
        if(playerSO.ScrapHaveAmount >= myB_Data.currentScrap
        && playerSO.InsightPointHaveAmount >= myB_Data.currentInsight)
        {
            upgradeButton_CGroup.interactable = true;
            upgradeButton_CGroup.alpha = 1f;        // 透明度を最大にする
            return true;
        }

        upgradeButton_CGroup.interactable = false;      // 少し薄くする
        upgradeButton_CGroup.alpha = CantButtonClick_Alpha;
        return false;
    }

    /// <summary>
    /// 強化ボタンを押したときの処理
    /// </summary>
    void UpgradeButtonOnClick()
    {
        SoundManager.Instance.PlaySE(0);
        // 現在選択されているオブジェクトをnullに設定
        EventSystem.current.SetSelectedGameObject(null);
        // 所持数を減少
        playerSO.ScrapHaveAmount = -myB_Data.currentScrap;
        playerSO.InsightPointHaveAmount = -myB_Data.currentInsight;

        myB_Data.LevelUpProc();                 // レベルアップ処理
        UpdateTextValue();                      // テキストを更新
        statusTextCont?.SetStatusValue();        // ステータス表記のテキストを更新
        Debug.Log("レベルアップしました");
    }

    void OpenOrClosePanel()
    {
        SoundManager.Instance.PlaySE(1);
        UpdateTextValue();

        // まだOpenしていないとき
        if(openPanel.activeSelf == false)
        {
            anim.SetTrigger("Open");
        }
        else
        {
            anim.SetTrigger("Close");
        }
    }
}
