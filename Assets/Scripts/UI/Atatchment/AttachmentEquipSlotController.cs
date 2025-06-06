using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// AttachmentEquipSlot.csを管理するクラス
/// attachmentを装備する
/// </summary>
[RequireComponent(typeof(AttachmentShopSlotController))]
public class AttachmentEquipSlotController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerStatusSO;
    [SerializeField] StatusTextController statusTextCtrl;
    [SerializeField] Transform slot_Parent;
    AttachmentEquipSlot[] slots;
    AttachmentShopSlotController assCtrl;
    void Awake()
    {
        slots = slot_Parent.GetComponentsInChildren<AttachmentEquipSlot>();
        assCtrl = GetComponent<AttachmentShopSlotController>();

        // 装備中のAttachmentを反映させる
        for (int ii = 0; ii < slots.Length; ii++)
        {
            if (ii < playerStatusSO.EquipAttachments.Count)
            {
                slots[ii].SetInit(playerStatusSO.EquipAttachments[ii], assCtrl);
            }
            else
            {
                slots[ii].SetInit(null, assCtrl);
            }
        }
    }

    void Start()
    {
        // !順番通りに処理を行っているのでStartで実行
        UpdateEquipAttachment();
    }

    /// <summary>
    /// 装備する処理
    /// </summary>
    public void EquipAttachment(AttachmentDataSO data)
    {
        for (int ii = 0; ii < slots.Length; ii++)
        {
            // 装備されていなければ
            if (slots[ii].AttachmentDataSO == null)
            {
                // 装備する
                data.IsEquiped = true;
                slots[ii].SetInit(data, assCtrl);
                break;
            }
        }

        UpdateEquipAttachment();
    }

    /// <summary>
    /// attachmentを外したときの処理
    /// ボタンテキストの更新
    /// </summary>
    /// <param name="removeData">外すattachment</param>
    public void RemoveAttachment(AttachmentDataSO removeData)
    {
        foreach (AttachmentEquipSlot slot in slots)
        {
            if (slot.AttachmentDataSO == removeData)
            {
                // スロットを空にする
                slot.SetInit(null, null);
                break;
            }
        }

        UpdateEquipAttachment();
    }

    /// <summary>
    /// 装備状態を更新する
    /// </summary>
    public void UpdateEquipAttachment()
    {
        playerStatusSO.EquipAttachments.Clear();
        foreach (AttachmentEquipSlot data in slots)
        {
            if (data.AttachmentDataSO == null) continue;
            playerStatusSO.EquipAttachments.Add(data.AttachmentDataSO);
        }

        UpdateStatusText();
    }

    public void UpdateStatusText()
    {
        statusTextCtrl.EquipUpdateText();
    }
}