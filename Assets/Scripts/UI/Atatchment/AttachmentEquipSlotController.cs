using UnityEngine;

/// <summary>
/// AttachmentEquipSlot.csを管理するクラス
/// attachmentを装備する
/// </summary>
[RequireComponent(typeof(AttachmentShopSlotController))]
public class AttachmentEquipSlotController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerStatusSO;
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
            if (ii < playerStatusSO.EquipAttachments.Length)
            {
                slots[ii].SetInit(playerStatusSO.EquipAttachments[ii], assCtrl);
            }
            else
            {
                slots[ii].SetInit(null, assCtrl);
            }
        }
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
    }
}