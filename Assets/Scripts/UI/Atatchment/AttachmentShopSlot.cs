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
    [SerializeField] CanvasGroup devOrEquipCanvasGroup;
    [SerializeField] Button openCloseButton;
    [SerializeField] TextMeshProUGUI resourceValue;

    public AttachmentDataSO AttachmentDataSO { get; private set; }
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
        AttachmentDataSO = data;
        playerStatusSO = playerstatus;
        assCtrl = ctrl;

        icon.sprite = data.Icon;
        attachmentName.text = data.Name;
        effectName.text = data.StatusNameToName(data.UseSutatusName);
        upstatusText.text = $"+{data.UpgreadeValue}";
        resourceValue.text = $"{data.ResourceValue}";

        CheckStat();
        CheckCanDevelop();
    }

    /// <summary>
    /// Attachmentの状態をテキストに反映
    /// </summary>
    public void CheckStat()
    {
        if (AttachmentDataSO.IsEquiped)
        {
            devOrEquipButtonText.text = "装備中";
            return;
        }

        if (AttachmentDataSO.IsDeveloped)
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
        if (AttachmentDataSO.IsDeveloped) return;
        // スクラップの所持数がリソースを超えていたらボタンが押せるようにする
        if (AttachmentDataSO.ResourceValue <= playerStatusSO.ScrapHaveAmount)
        {
            devOrEquipCanvasGroup.alpha = 1;
            devOrEquipCanvasGroup.interactable = true;
            devOrEquipButtonText.text = "開発";
        }
        else
        {
            devOrEquipCanvasGroup.alpha = 0.3f;
            devOrEquipCanvasGroup.interactable = false;
            devOrEquipButtonText.text = "不足";
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
        // 装備中だった場合以降の処理は行わない
        CheckStat();
        if (AttachmentDataSO.IsEquiped) return;

        // 開発済みだった場合
        if (AttachmentDataSO.IsDeveloped)
        {
            // 装備する処理
            assCtrl.EquipOnClick(AttachmentDataSO);
            CheckStat();
        }
        else        // 未開発だった場合
        {
            // 所持リソースを減少させる
            playerStatusSO.ScrapHaveAmount = -AttachmentDataSO.ResourceValue;
            // 開発完了フラグを立てる
            AttachmentDataSO.IsDeveloped = true;
            CheckStat();       // ボタンテキストを更新する
        }
    }

    /// <summary>
    /// カーソルがSlotに入った時の処理
    /// </summary>
    public void OnCursorEnter()
    {
        if (IsPanelOpen) return;
        anim.SetTrigger("Select");
    }


    /// <summary>
    /// カーソルがSlotから出たときの処理
    /// </summary>
    public void OnCursorExit()
    {
        if (IsPanelOpen) return;
        anim.SetTrigger("Exit");
    }
}
