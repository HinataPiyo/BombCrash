using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AttachmentShopSlot : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI attachmentName;
    [SerializeField] TextMeshProUGUI effectName;
    [SerializeField] TextMeshProUGUI upstatusText;
    [SerializeField] Button devOrEquipButton;
    [SerializeField] CanvasGroup devOrEquipCanvasGroup;
    [SerializeField] Button openCloseButton;
    [SerializeField] TextMeshProUGUI resourceValue;

    AttachmentDataSO m_attachmentDataSO;
    PlayerStatusSO playerStatusSO;
    AttachmentShopSlotController assCtrl;
    Animator anim;
    TextMeshProUGUI devOrEquipButtonText;
    public bool IsPanelOpen { get; private set; }

    void Awake()
    {
        openCloseButton.onClick.AddListener(OpenClosePanel);
        devOrEquipButton.onClick.AddListener(DevelopOrEquipOnClick);
        anim = GetComponent<Animator>();
        devOrEquipButtonText = devOrEquipButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetInit(AttachmentDataSO data, PlayerStatusSO playerstatus, AttachmentShopSlotController ctrl)
    {
        m_attachmentDataSO = data;
        playerStatusSO = playerstatus;
        assCtrl = ctrl;

        icon.sprite = data.Icon;
        attachmentName.text = data.Name;
        effectName.text = StatusNameToName(data.UseSutatusName);
        upstatusText.text = $"+{data.UpgreadeValue}";
        resourceValue.text = $"{data.ResourceValue}";

        CheckDeveloped();
        CheckCanDevelop();
    }

    /// <summary>
    /// 装備を購入したか否か
    /// </summary>
    void CheckDeveloped()
    {
        if (m_attachmentDataSO.IsDeveloped)
        {
            devOrEquipButtonText.text = "装備";
            devOrEquipButtonText.color = new Color32(0, 107, 237, 255);
        }
        else
        {
            devOrEquipButtonText.text = "開発";
            devOrEquipButtonText.color = new Color32(237, 72, 0, 255);
        }
    }

    /// <summary>
    /// 開発できるかどうかを調べる
    /// </summary>
    void CheckCanDevelop()
    {
        // 開発済みなら以降の処理は行わない
        if (m_attachmentDataSO.IsDeveloped) return;
        // スクラップの所持数がリソースを超えていたらボタンが押せるようにする
        if (m_attachmentDataSO.ResourceValue <= playerStatusSO.ScrapHaveAmount)
        {
            devOrEquipCanvasGroup.alpha = 1;
            devOrEquipCanvasGroup.interactable = true;
        }
        else
        {
            devOrEquipCanvasGroup.alpha = 0.5f;
            devOrEquipCanvasGroup.interactable = false;
        }
    }

    /// <summary>
    /// パネルの開閉を管理するメソッド
    /// </summary>
    void OpenClosePanel()
    {
        if (!IsPanelOpen)
        {
            assCtrl.OtherPanelOnClick();        // 自身のパネルを開く前に他のパネルを閉じる
            CheckCanDevelop();      // Openするときに毎回　リソースの所持数を超えているか確認
            anim.SetTrigger("Open");
            IsPanelOpen = true;
        }
        else
        {
            anim.SetTrigger("Close");
            IsPanelOpen = false;
        }
    }


    /// <summary>
    /// 他のパネルを開いたらこの処理を実行
    /// </summary>
    public void ClosePanel()
    {
        anim.SetTrigger("Close");
        IsPanelOpen = false;
    }


    /// <summary>
    /// 開発か装備というボタンをクリックしたときの処理
    /// </summary>
    public void DevelopOrEquipOnClick()
    {
        // 開発完了フラグを立てる
        m_attachmentDataSO.IsDeveloped = true;
        CheckDeveloped();       // ボタンテキストを更新する
    }


    /// <summary>
    /// StatusNameから日本語に変換した文字列を返す
    /// </summary>
    string StatusNameToName(StatusName statusName)
    {
        switch (statusName)
        {
            case StatusName.BombAttackDamageUp:
                return "冷却ダメージ";
            case StatusName.BombCreateSpeedUp:
                return "爆弾生成速度";
            case StatusName.BombStockAmountUp:
                return "最大爆弾所持数";
            case StatusName.CriticalDamageUp:
                return "クリティカルダメージ";
            case StatusName.CriticalChanceUp:
                return "クリティカル率";
            case StatusName.ExplosionRadiusUp:
                return "爆発範囲";
        }

        return "";
    }
}
