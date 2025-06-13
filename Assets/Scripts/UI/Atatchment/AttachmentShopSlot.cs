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
        effectName.text = SystemDefine.StatusNameToName(data.UseSutatusName);
        upstatusText.text = $"+{data.UpgreadeValue}";
        resourceValue.text = $"{data.ResourceValue}";

        CheckCanStat();
    }

    /// <summary>
    /// 開発できるかどうかを調べる
    /// </summary>
    public void CheckCanStat()
    {
        // 開発済みなら以降の処理は行わない
        if (AttachmentDataSO.IsDeveloped)
        {
            devOrEquipCanvasGroup.alpha = 1;
            devOrEquipCanvasGroup.interactable = true;
            devOrEquipButtonText.text = AttachmentDataSO.IsEquiped ? "装備中" : "装備";
            devOrEquipButtonText.color = new Color32(0, 107, 237, 255);
            return;
        }

        // スクラップの所持数がリソースを超えていたらボタンが押せるようにする
        if (AttachmentDataSO.ResourceValue <= playerStatusSO.ScrapHaveAmount)
        {
            devOrEquipCanvasGroup.alpha = 1;
            devOrEquipCanvasGroup.interactable = true;
            devOrEquipButtonText.text = "開発";
            devOrEquipButtonText.color = new Color32(237, 72, 0, 255);
        }
        else
        {
            devOrEquipCanvasGroup.alpha = 0.3f;
            devOrEquipCanvasGroup.interactable = false;
            devOrEquipButtonText.text = "不足";
            devOrEquipButtonText.color = new Color32(180, 150, 40, 255);
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
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        // 装備中だった場合以降の処理は行わない
        CheckCanStat();
        if (AttachmentDataSO.IsEquiped) return;

        // 開発済みだった場合
        if (AttachmentDataSO.IsDeveloped)
        {
            // 装備する処理
            assCtrl.EquipOnClick(AttachmentDataSO);
            CheckCanStat();
        }
        else        // 未開発だった場合
        {
            // 所持リソースを減少させる
            playerStatusSO.ScrapHaveAmount = -AttachmentDataSO.ResourceValue;
            // 開発完了フラグを立てる
            AttachmentDataSO.IsDeveloped = true;
            CheckCanStat();
        }
    }

    /// <summary>
    /// カーソルがSlotに入った時の処理
    /// </summary>
    public void OnCursorEnter()
    {
        if (IsPanelOpen) return;
        assCtrl.OtherPanelOnClick();        // 自身のパネルを開く前に他のパネルを閉じる
        CheckCanStat();      // Openするときに毎回　リソースの所持数を超えているか確認
        anim.SetTrigger("Open");
        IsPanelOpen = true;
    }


    /// <summary>
    /// カーソルがSlotから出たときの処理
    /// </summary>
    public void OnCursorExit()
    {
        anim.SetTrigger("Close");
        IsPanelOpen = false;
    }
}
