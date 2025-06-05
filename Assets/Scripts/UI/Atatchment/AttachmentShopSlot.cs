using TMPro;
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
    [SerializeField] Button openCloseButton;

    AttachmentDataSO m_attachmentDataSO;
    Animator anim;
    TextMeshProUGUI devOrEquipButtonText;
    bool isPanelOpen;

    void Awake()
    {
        openCloseButton.onClick.AddListener(OpenClosePanel);
        devOrEquipButton.onClick.AddListener(DevelopOrEquipOnClick);
        anim = GetComponent<Animator>();
        devOrEquipButtonText = devOrEquipButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetInit(AttachmentDataSO data)
    {
        m_attachmentDataSO = data;

        icon.sprite = data.Icon;
        attachmentName.text = data.Name;
        effectName.text = StatusNameToName(data.UseSutatusName);
        upstatusText.text = $"+{data.UpgreadeValue}";

        CheckDeveloped();
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
    /// パネルの開閉を管理するメソッド
    /// </summary>
    void OpenClosePanel()
    {
        if (!isPanelOpen)
        {
            anim.SetTrigger("Open");
            isPanelOpen = true;
        }
        else
        {
            anim.SetTrigger("Close");
            isPanelOpen = false;
        }
    }


    /// <summary>
    /// 他のパネルを開いたらこの処理を実行
    /// </summary>
    public void ClosePanel()
    {
        anim.SetTrigger("Close");
        isPanelOpen = false;
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
