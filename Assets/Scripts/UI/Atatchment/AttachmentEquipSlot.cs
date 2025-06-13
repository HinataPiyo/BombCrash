using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attachmentを装備するSlotUIの設定
/// </summary>
public class AttachmentEquipSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI effectName;
    [SerializeField] TextMeshProUGUI upstatusValue;
    [SerializeField] Button removeButton;       // 装備を外すボタン

    public AttachmentDataSO AttachmentDataSO { get; private set; }
    AttachmentShopSlotController assCtrl;
    Animator anim;

    void Awake()
    {
        removeButton.onClick.AddListener(RemoveAttachment);
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void SetInit(AttachmentDataSO data, AttachmentShopSlotController ctrl)
    {
        if (assCtrl == null && ctrl != null) assCtrl = ctrl;

        if (data == null)
        {
            AttachmentDataSO = null;
            icon.enabled = false;
            effectName.text = "";
            upstatusValue.text = "";

            return;
        }

        AttachmentDataSO = data;

        icon.enabled = true;
        icon.sprite = data.Icon;
        effectName.text = SystemDefine.StatusNameToName(data.UseSutatusName);
        upstatusValue.text = $"+{data.UpgreadeValue}";
    }

    /// <summary>
    /// attachmentを外す処理
    /// </summary>
    void RemoveAttachment()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.Slot_Click);
        if (AttachmentDataSO == null) return;
        // 装備中のフラグを折る
        AttachmentDataSO.IsEquiped = false;
        // 装備を外す処理（ボタンテキストの更新）
        // !必ずフラグがおられた時に処理を行う
        assCtrl.RemoveAttachment(AttachmentDataSO);
        SetInit(null, null);      // スロットを空にする
    }

    /// <summary>
    /// カーソルがSlotに入った時の処理
    /// </summary>
    public void OnCursorEnter()
    {
        anim.SetTrigger("Select");
    }


    /// <summary>
    /// カーソルがSlotから出たときの処理
    /// </summary>
    public void OnCursorExit()
    {
        anim.SetTrigger("Exit");
    }
}
